using HtmlAgilityPack;
using System.Text.RegularExpressions;
using TableTopBattleTracker.Data;
using TableTopBattleTracker.Model;

namespace dndsuScraper;

public class SpellParser : BaseParser
{

    public SpellParser(AppDbContext context) : base(context)
    {
    }


    public async Task CollectSpells()
    {
        var spellDoc = await GetDocumentAsync(new Uri(_baseLink + @"spells/"));
        var lisWithLinks = spellDoc.DocumentNode.SelectNodes("//li[@class='for_filter']");
        int i = 0;
        foreach (var liWithLink in lisWithLinks)
        {
            var spell = await GetSpellFromUri(new Uri(_baseLink + GetLinkFromNode(liWithLink)));
            Console.Write($"[{i++}]lvl:\t{spell.Level},\t{spell.AreaOfEffect?.Name}:\t{spell.AreasSize},\t");
            Console.WriteLine($"ct:\t{spell.CastTime?.Name},\tdist:\t{spell.CastRange?.Name}");
            Console.WriteLine($"DC:\t{spell.DC},\t{spell.SpellDamage?.FirstOrDefault()?.DamageType?.Name},\t{spell.SpellDamage?.FirstOrDefault()?.SpellDamageValues?.FirstOrDefault()?.Value}");
            Console.WriteLine();
            _dbContext.Spells?.Add(spell);
        }
        await _dbContext.SaveChangesAsync();

    }

    async Task<Spell> GetSpellFromUri(Uri uri)
    {
        await Task.Delay(50);
        var doc = await GetDocumentAsync(uri);
        var spell = new Spell();

        SetSpellName(ref spell, doc);
        Console.WriteLine(spell.Name);

        var parNodes = doc.DocumentNode.SelectNodes("//ul[@class='params']")[0].SelectNodes("./li").ToList();

        SetSpellIsRitualAndLevel(ref spell, parNodes[0].InnerText);

        SetSpellSchoool(ref spell, parNodes[0].InnerText);


        for (var i = 1; i < parNodes.Count; i++)
        {
            var innerText = parNodes[i].InnerText.Trim();
            if (innerText.StartsWith("Время накладывания"))
            {
                SetSpellCastTime(ref spell, innerText);
            }
            else if (innerText.StartsWith("Дистанция"))
            {
                SetSpellCastRange(ref spell, innerText);
            }
            else if (innerText.StartsWith("Компоненты"))
            {
                try
                {
                    SetSpellCastComponentsAndMaterial(ref spell, innerText);
                }
                catch (Exception ex) { Console.WriteLine($"---------{ex.Message}!!!!!!"); }
            }
            else if (innerText.StartsWith("Длительность"))
            {
                SetSpellDurationAndIsConcentration(ref spell, innerText);
            }
        }


        var desc = doc.DocumentNode.SelectSingleNode("//div[@itemprop='description']");
        spell.Desc = desc.InnerText;

        SetSpellAreaOfEffectAndSize(ref spell, desc.InnerText);

        SetSpellDC(ref spell, desc.InnerText);

        SetSpellDamage(ref spell, desc.InnerText);

        return spell;
    }

    private void SetSpellDamage(ref Spell spell, string str)
    {
        var spellDamages = new List<SpellDamage>();
        int lvl = (int)spell.Level;
        var diceReg = new Regex(@"\d+к\d+");
        foreach (var damType in Enum.GetValues(typeof(EDamageType)))
        {
            var dt = (EDamageType)damType;

            ChooseRegexForDamageType(str, dt, out string dtStr, out Match matched);
            if (matched.Success)
            {
                var sd = new SpellDamage()
                {
                    DamageType = _dbContext.DamageTypes?.Find(dt),
                    Spell = spell,
                    SpellDamageValues = new List<SpellDamageValue>(),
                };
                sd.SpellDamageValues.Add(new SpellDamageValue()
                {
                    DamageTypeId = dt,
                    Level = lvl > 0 ? lvl : 1,
                    Value = diceReg.Match(matched.Value).Value,
                });
                spellDamages.Add(sd);
                Console.WriteLine($"-------------------\t{matched.Value}\n" +
                                  $"+++++++++++++++++++\t{dt}:{dtStr}\t++++: {sd.SpellDamageValues?.FirstOrDefault()?.Value}");
            }
        }
        if (spellDamages.Count > 0)
            SetBiggerLevelDamages(ref spellDamages, lvl, str);
        spell.SpellDamage = spellDamages;
    }

    private static void ChooseRegexForDamageType(string str, EDamageType dt, out string dtStr, out Match matched)
    {
        dtStr = DamageType.GetNameById(dt);
        Regex damReg;
        Regex damRegRev;
        switch (dt)
        {
            case EDamageType.Force:
                damReg = new(@"урона? силовым полем.{0,} \d+к\d+");
                damRegRev = new($@"\d+к\d+ урона? силовым полем.+");
                break;
            case EDamageType.Ligthning:
                damReg = new(@"урона? электрич.{0,} \d+к\d+");
                damRegRev = new(@"\d+к\d+ урона? электрич");
                break;
            case EDamageType.Fire:
            case EDamageType.Cold:
            case EDamageType.Thunder:
            case EDamageType.Poison:
            case EDamageType.Acid:
            case EDamageType.Radiant:
            case EDamageType.Psichic:
                damReg = new(@$"урона? {dtStr.ToLower()[..^2]}.{"{0,}"} \d+к\d+");
                damRegRev = new(@$"\d+к\d+ урона? {dtStr.ToLower()[..^2]}");
                break;
            case EDamageType.Slashing:
            case EDamageType.Bludgeoning:
            case EDamageType.Piercing:
            case EDamageType.Necrotic:
                damReg = new(@$"{dtStr.ToLower()[..^2]}[а-я][а-я].? урон.+ \d+к\d+");
                damRegRev = new(@$"\d+к\d+ {dtStr.ToLower()[..^2]}[а-я][а-я].? урон.+");
                break;
            case EDamageType.Healing:
                damReg = new($@"восстан.+ \d+к\d+");
                damRegRev = new($@"\d+к\d+ восстан.+");
                break;
            default:
                throw new Exception("Wrong damage type");
        }
        matched = damReg.Match(str);
        var matchedRev = damRegRev.Match(str);
        matched = matched.Success ? matched : matchedRev;
    }

    private void SetBiggerLevelDamages(ref List<SpellDamage> spellDamages, int spellLvl, string str)
    {
        if (spellLvl > 0)
        {
            var bigLvlStr = new Regex(@"На больших уровнях.+").Match(str).Value;
            if (spellDamages.Count == 1)
            {
                SetBiggerSpellSlotsDamages(spellDamages[0], spellLvl, bigLvlStr);
            }
            else
            {
                foreach (SpellDamage spellDamage in spellDamages)
                {
                    var tn = spellDamage.DamageType?.DamageTypeId;
                    if (tn == null) continue;
                    var t = tn ?? EDamageType.Slashing;
                    ChooseRegexForDamageType(bigLvlStr, t, out string _, out Match match);
                    if (match.Success)
                    {
                        SetBiggerSpellSlotsDamages(spellDamage, spellLvl, bigLvlStr);
                    }
                    else
                    {
                        spellDamage.IncreaseType = EIncreaseType.None;
                    }
                }
            }
        }
        else if (spellLvl == 0)
        {
            var bigLvlStr = new Regex(@"[Уу]рон.+увелич.+").Match(str).Value;
            if (spellDamages.Count == 1)
            {
                SetBiggerLevelCantripDamages(spellDamages[0], bigLvlStr);
            }
            else
            {
                foreach (SpellDamage spellDamage in spellDamages)
                {
                    var tn = spellDamage.DamageType?.DamageTypeId;
                    if (tn == null) continue;
                    var t = tn ?? EDamageType.Slashing;
                    ChooseRegexForDamageType(bigLvlStr, t, out string _, out Match match);
                    if (match.Success)
                    {
                        SetBiggerLevelCantripDamages(spellDamage, bigLvlStr);
                    }
                    else
                    {
                        spellDamage.IncreaseType = EIncreaseType.None;
                    }
                }

            }
        }
    }

    private static void SetBiggerLevelCantripDamages(SpellDamage spellDamage, string str)
    {
        spellDamage.IncreaseType = EIncreaseType.OnCharacterLevel;
        if (spellDamage.SpellDamageValues == null)
            spellDamage.SpellDamageValues = new List<SpellDamageValue>();
        foreach (var i in new List<int> { 5, 11, 17 })
        {
            var reg = new Regex(@$"{i}[ -гом]{"{0,4}"}(уровн[яе] ?)?\(( ?\d+к\d+[ и,]{"{0,2}"}){"{1,3}"}\)");
            var match = reg.Match(str);
            if (match.Success)
            {
                var valReg = new Regex(@"\d+к\d+");
                spellDamage.SpellDamageValues?.Add(new()
                {
                    DamageTypeId = spellDamage.DamageTypeId,
                    Level = i,
                    Value = valReg.Match(match.Value).Value,
                });
            }
            Console.WriteLine($"************\t{spellDamage.SpellDamageValues?.LastOrDefault()?.Level}" +
                    $"\t{spellDamage.SpellDamageValues?.LastOrDefault()?.Value}");
        }
    }

    private static void SetBiggerSpellSlotsDamages(SpellDamage spellDamage, int spellLvl, string bigLvlStr)
    {
        spellDamage.IncreaseType = EIncreaseType.OnSpellSlot;
        var damageIncrementMatch = new Regex(@"\d+к\d+").Match(bigLvlStr);
        var step = 10;
        if (damageIncrementMatch.Success)
        {
            if (new Regex(@"кажд[ыйог]{2,3} уров[енья]{2,3} ячейки").IsMatch(bigLvlStr))
                step = 1;
            else if (new Regex(@"кажд[ыеую]{2} [дв][вт][ео]").IsMatch(bigLvlStr))
                step = 2;

            var dice = damageIncrementMatch.Value.Split("к");
            var baseDice = spellDamage.SpellDamageValues?
                .FirstOrDefault()?.Value?.Split("к") ?? dice;
            var baseCount = int.Parse(baseDice[0]);
            var baseSides = int.Parse(baseDice[1]);
            var diceCount = int.Parse(dice[0]);
            var diceSides = int.Parse(dice[1]);
            if (diceSides != baseSides) throw new Exception("Wrong dice");
            for (var i = spellLvl + step; i <= 9; i += step)
            {
                baseCount += diceCount;
                spellDamage.SpellDamageValues?.Add(new SpellDamageValue()
                {
                    Level = i,
                    Value = $"{baseCount}к{baseSides}"
                });
                Console.WriteLine($"************\t{spellDamage.SpellDamageValues?.LastOrDefault()?.Level}" +
                    $"\t{spellDamage.SpellDamageValues?.LastOrDefault()?.Value}");
            }
        }
    }

    private void SetSpellDC(ref Spell spell, string innerText)
    {
        List<ECharacteristic> lst = new();
        foreach (var ch in Enum.GetValues(typeof(ECharacteristic)))
        {
            var characteristic = (ECharacteristic)ch;
            var str = Characteristic.GetNameById(characteristic);
            var reg = new Regex(
                @"[Сс]пасброс[оке]{2}[а-я]{0,3} [" + str.ToUpper() + str.ToLower() + @"]{3}");
            if (reg.IsMatch(innerText))
                lst.Add(characteristic);
        }
        spell.DC = lst.Count > 0 ? lst[0] : null;
    }

    private void SetSpellAreaOfEffectAndSize(ref Spell spell, string str)
    {
        foreach (var oaoe in Enum.GetValues(typeof(EAreaType)))
        {
            try
            {

                var aoe = (EAreaType)oaoe;
                var regStr = AreaOfEffect.GetNameById(aoe);
                if (regStr.StartsWith('Л'))
                    regStr = regStr[..^1];
                var reg = aoe != EAreaType.Sphere
                    ? new Regex($"[{regStr[0]}{regStr.ToLower()[0]}]{regStr[1..]}")
                    : new Regex($"[Рр]адиус");
                if (reg.IsMatch(str))
                {
                    spell.AreaOfEffect = _dbContext.AreaOfEffects?.Find(aoe) ?? throw new Exception("No AOE in db");
                    spell.AreasSize = AreaSize(aoe, str);
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-----------{ex.Message}!!!!");
                spell.AreaOfEffect = null;
            }
        }
    }

    private int? AreaSize(EAreaType aoe, string innerText)
    {
        int size = 0;
        bool correct = false;
        var digit = new Regex(@"\d+");
        string? regVal;
        switch (aoe)
        {
            case EAreaType.Cone:
                break;
            case EAreaType.Cube:
            case EAreaType.Line:
                regVal = new Regex(@"линой .+?\d+").Match(innerText).Value ?? new Regex(@"тороной .+?\d+").Match(innerText).Value;
                correct = int.TryParse(digit.Match(regVal).Value, out size);
                break;
            case EAreaType.Cilinder:
            case EAreaType.Sphere:
                regVal = new Regex(@"адиусом .+?\d+").Match(innerText).Value;
                correct = int.TryParse(digit.Match(regVal).Value, out size);
                break;
            default:
                throw new Exception("Wrong AOE");
        }
        if (!correct)
        {
            regVal = new Regex(@" \d+[- ]фут").Match(innerText).Value;
            var corr = int.TryParse(digit.Match(regVal).Value, out size);
            if (!corr) throw new Exception("No size founded");
        }
        return size;
    }

    private static void SetSpellName(ref Spell spell, HtmlDocument doc)
    {
        spell.Name = doc.DocumentNode.SelectNodes("//h2[@class='card-title ']")[0].InnerText;
    }

    private static void SetSpellDurationAndIsConcentration(ref Spell spell, string str)
    {
        Regex concentr = new(".?[Кк]онцентрация.?");
        spell.IsConcetration = concentr.IsMatch(str);
        var duration = str.Split(':')[1].Trim();
        spell.Duration = duration.Length < 64 ? duration : duration[..64];
    }

    private void SetSpellCastComponentsAndMaterial(ref Spell spell, string str)
    {
        var strComp = str.Split(":")[1].Trim();
        if (strComp.Contains('('))
        {
            var materials = strComp.Split('(')[1].Trim()[..^1];
            spell.Materials = materials.Length < 96 ? materials : materials[..96];
            strComp = strComp.Split('(')[0].Trim();
        }
        var componentNames = strComp.Split(",");
        spell.CastingComponents = new List<CastingComponent>();
        foreach (var componentName in componentNames)
        {
            if (componentName.Length < 1)
                continue;
            var comp = componentName.TrimStart()[0] switch
            {
                'В' => _dbContext.CastingComponents?.Find(ECastingComponent.V),
                'С' => _dbContext.CastingComponents?.Find(ECastingComponent.S),
                'М' => _dbContext.CastingComponents?.Find(ECastingComponent.M),
                _ => throw new Exception("wrong component"),
            };
            spell.CastingComponents.Add(comp ?? throw new Exception());
        }
    }

    private void SetSpellCastRange(ref Spell spell, string str)
    {
        var castRangeName = str.Split(":")[1].Trim();
        castRangeName = castRangeName.Length < 64 ? castRangeName : castRangeName[0..64];
        var castRange = _dbContext.CastRanges?.FirstOrDefault(cr => cr.Name == castRangeName);
        spell.CastRange = castRange ?? new CastRange() { Name = castRangeName };
    }

    private void SetSpellCastTime(ref Spell spell, string parNode)
    {
        var castTimeName = parNode.Split(":")[1].Trim();
        castTimeName = castTimeName.Length < 64 ? castTimeName : castTimeName[0..64];
        var castTime = _dbContext.CastTimes?.FirstOrDefault(castTime => castTime.Name == castTimeName);
        spell.CastTime = castTime ?? new CastTime() { Name = castTimeName };
    }

    private static void SetSpellIsRitualAndLevel(ref Spell spell, string str)
    {
        Regex regex = new(".?[Рр]итуал.?");
        spell.IsRitual = regex.IsMatch(str);
        if (!int.TryParse(str.Split(",")[0].Split(" ")[0].Trim(), out int level))
            level = 0;
        spell.Level = (ESpellLelel)level;
    }

    private void SetSpellSchoool(ref Spell spell, string str)
    {
        var schoolName = str.Split(",")[1];
        var school = _dbContext.SpellSchools?.FirstOrDefault(school => school.Name == school.Name);
        spell.SpellSchool = school ?? new SpellSchool() { Name = schoolName };
    }

    void SeedBaseTables()
    {
        /*
        var ae = new List<AreaOfEffect>();
        foreach(var id in Enum.GetValues(typeof(EAreaType)))
        {
            ae.Add(new AreaOfEffect()
            {
                AreaOfEffectId = (EAreaType)id,
                Name = AreaOfEffect.GetNameById((EAreaType) id),
            });
        }
        dbContext.AreaOfEffects.AddRange(ae);
        var cc = new List<CastingComponent>();
        foreach (var id in Enum.GetValues(typeof(ECastingComponent)))
        {
            cc.Add(new CastingComponent()
            {
                CastingComponentId = (ECastingComponent)id,
                Name = CastingComponent.GetNameById((ECastingComponent)id)
            });
        }
        dbContext.CastingComponents.AddRange(cc);

        var ch = new List<Characteristic>();
        foreach (var id in Enum.GetValues(typeof(ECharacteristic)))
        {
            ch.Add(new Characteristic()
            {
                CharacteristicId = (ECharacteristic)id,
                Name = Characteristic.GetNameById((ECharacteristic)id),
            });
        }
        dbContext.Characteristics.AddRange(ch);

        var co = new List<Condition>();
        foreach (var id in Enum.GetValues(typeof(ECondition)))
        {
            co.Add(new Condition()
            {
                ConditionId = (ECondition)id,
                Name = Condition.GetNameById((ECondition)id),
            });
        }
        dbContext.Conditions.AddRange(co);

        var dt = new List<DamageType>();
        foreach (var id in Enum.GetValues(typeof(EDamageType)))
        {
            dt.Add(new DamageType()
            {
                DamageTypeId = (EDamageType)id,
                Name = DamageType.GetNameById((EDamageType)id),
            });
        }
        dbContext.DamageTypes.AddRange(dt);

        var mt = new List<MonsterSize>();
        foreach (var id in Enum.GetValues(typeof(EMonsterSize)))
        {
            var (name, mod) = MonsterSize.GetParamsById((EMonsterSize)id);
            mt.Add(new MonsterSize()
            {
                MonsterSizeId = (EMonsterSize)id,
                Name = name,
                SpaceModifier = mod,
            });
        }
        dbContext.MonsterSizes.AddRange(mt);

        var sl = new List<Slot>();
        foreach (var id in Enum.GetValues(typeof(ESpellLelel)))
        {
            sl.Add(new Slot()
            {
                SlotId = (ESpellLelel)id,
                Name = Slot.GetNameById((ESpellLelel)id),
            });
        }
        dbContext.Slots.AddRange(sl);

        var st = new List<SpeedType>();
        foreach (var id in Enum.GetValues(typeof(ESpeedType)))
        {
            st.Add(new SpeedType()
            {
                SpeedTypeId = (ESpeedType)id,
                Name = SpeedType.GetNameById((ESpeedType)id),
            });
        }
        dbContext.SpeedTypes.AddRange(st);



        dbContext.SaveChanges();
        */
    }

}
