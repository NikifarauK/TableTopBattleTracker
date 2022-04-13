using dndsuScraper;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TableTopBattleTracker.Data;
using TableTopBattleTracker.Model;

internal class MonsterParser : BaseParser
{
    public MonsterParser(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CollectCharacters()
    {
        var charDoc = await GetDocumentAsync(new Uri(_baseLink + @"bestiary/"));
        var lisWithLinks = charDoc.DocumentNode.SelectNodes("//li[@class='for_filter']");

        int i = 0;
        foreach (var li in lisWithLinks)
        {
            var ch = await GetCharacterFromUri(new Uri(_baseLink + GetLinkFromNode(li)));

            if (ch == null) continue; //start point

            Console.WriteLine($"[{i++}]\tcr: {ch.ChallengeRating}\tstr: {ch.Strength}\t{ch.Allignment}\t{ch.MonsterSize}");
            Console.WriteLine($"\t{ch.MonsterType}\tsense count:{ch.Senses?.Count}\tdam res: {ch.DamageResistances?.Count ?? 0}");
            Console.WriteLine($"\tdam immun: {ch.DamageImmunities?.Count}\tdam vuln: {ch.DamageVulnerabilities?.Count}\tcond imm: {ch.ConditionImmunities?.Count ?? 0}");
            Console.WriteLine($"\tspec abilities: {ch.SpecialAbilities?.Count}");
            _ = ch.SpecialAbilities?
                .Count(sa =>
                {
                    Console.WriteLine($"\t\t{sa.Name}-> {sa.Usage};");
                    return true;
                });
            var ts = ch.SpecialAbilities?.Where(c => c.Spellcasting != null)?.Select(s => (s.Spellcasting, s.Name));
            if (ts != null)
            {
                foreach (var (t, name) in ts)
                {
                    Console.WriteLine($"\t{name}:");
                    Console.WriteLine($"\t  spelcast: {t?.Ability?.ToString() ?? "----no spellcast"}\tlevel: {t?.Level}\tmod: {t?.Modifier}");
                    Console.WriteLine($"\t  dc: {t?.DifficultyClass}\tslots: {t?.Slots?.Count}");
                    _ = (t?.Slots?.Count(s =>
                      {
                          Console.WriteLine($"\t\t{s.SlotId}->{s.Times}");
                          return true;
                      }));
                    Console.WriteLine($"\t  spells: {t?.Spells?.Count}");
                    _ = t?.Spells?.Count(s =>
                    {
                        Console.WriteLine($"\t\t{s.SpellId}:{s.Spell}->{s.Usage}");
                        return true;
                    });
                }
            }
            Console.WriteLine();
        }
    }

    bool start = true;
    private async Task<Character> GetCharacterFromUri(Uri uri)
    {
        if (uri.AbsoluteUri.Contains(@"4842-drake_companion")) //start point
            start = false;                             //start point
        if (start)                                     //start point
            return null;

        var doc = await GetDocumentAsync(uri);
        //var doc = await GetDocumentAsync(new Uri(@"https://dnd.su/bestiary/5012-arrant_quill/"));
        var character = new Character();

        SetCharName(ref character, doc);
        Console.WriteLine(character.Name);

        var parNodes = doc.DocumentNode.SelectNodes("//ul[@class='params']")[0].SelectNodes("./li").ToList();

        SetCharTypeAndSize(ref character, parNodes[0].InnerText);
        SetCharAllingthement(ref character, parNodes[0].InnerText);

        //bool flag = true;
        for (var i = 1; i < parNodes.Count; i++)
        {
            var innerText = parNodes[i].InnerText;
            if (innerText.StartsWith("Класс Доспеха"))
            {
                SetArmorClass(ref character, innerText);
            }
            else if (innerText.StartsWith("Хиты"))
            {
                SetHitPointsAndDices(ref character, innerText);
            }
            else if (innerText.StartsWith("Скорость"))
            {
                SetSpeeds(ref character, innerText);
            }
            else if (innerText.StartsWith("СИЛ"))
            {
                SetCharacteristics(ref character, innerText);
            }
            else if (innerText.StartsWith("Навыки") || innerText.StartsWith("Спасброски"))
            {
                SetProficiencies(ref character, innerText);
            }
            else if (innerText.StartsWith("Опасность"))
            {
                SetChallengeRatingAndExp(ref character, innerText);
            }
            else if (innerText.StartsWith("Бонус мастерства"))
            {
                SetProfBonus(ref character, innerText);
            }
            else if (innerText.StartsWith("Язык"))
            {
                SetLanguages(ref character, innerText);
            }
            else if (innerText.StartsWith("Чувства"))
            {
                SetSenses(ref character, innerText);
            }
            else if (innerText.StartsWith("Сопротивление урону"))
            {
                SetDamageResistances(ref character, innerText);
            }
            else if (innerText.StartsWith("Иммунитет к урону"))
            {
                SetDamageImunities(ref character, innerText);
            }
            else if (innerText.StartsWith("Уязвимость к урону"))
            {
                SetDamageVulnerabilities(ref character, innerText);
            }
            else if (innerText.StartsWith("Иммунитет к состоянию"))
            {
                SetConditionImmunities(ref character, innerText);
            }
            if (parNodes[i].Attributes.Count(c => c.Value == "subsection desc") == 1 && parNodes[i].SelectNodes(@".//h3") == null)
            {
                SetSpecialAbilities(ref character, parNodes[i]);
            }
        }

        return character;
    }

    private void SetSpecialAbilities(ref Character character, HtmlNode abilitiesNode)
    {
        if (character.SpecialAbilities != null)
            return;
        var specAbilitiesSec = abilitiesNode.SelectNodes(@"./div/p");
        if (specAbilitiesSec == null)
            return;

        if (character.Name == null)
            throw new ArgumentException("Character name cant be null");

        character.SpecialAbilities = new List<SpecialAbility>();
        foreach (var sa in specAbilitiesSec)
        {
            var specialAbility = new SpecialAbility();
            var innerText = sa.InnerText.Trim();
            if (innerText.StartsWith("Язык"))
                continue;
            var saName = Regex.Match(innerText, @"^[А-Яа-яЁё ()//\d]+\.").Value.Trim();
            var nmLen = saName.Length;

            if (saName.Contains('('))
            {
                Usage usage = GetUsage(saName);
                specialAbility.Usage = usage;
                saName = saName[..saName.IndexOf('(')];
            }
            specialAbility.Name = saName;
            specialAbility.Desc = innerText[nmLen..].Trim();

            if (saName.StartsWith("Использование заклинаний") || saName.StartsWith("Врождённое колдовство"))
            {
                SetSpellcasting(ref specialAbility, sa);
            }
            /*
            if(Regex.IsMatch(specialAbility.Desc, @"\d+")){
                specialAbility.Value = int.Parse(Regex.Match(specialAbility.Desc, @"\d+").ValueSpan);
            }
            */
            character.SpecialAbilities.Add(specialAbility);
        }
    }

    private Usage GetUsage(Usage usage, string namePostfix)
    {
        return _dbContext?.Usages?.FirstOrDefault(u => u.Times == usage.Times && u.Type == (usage.Type + namePostfix)) ??
            new Usage() { Times = usage.Times, Type = (usage.Type + namePostfix) };
    }

    private Usage GetUsage(string str)
    {
        Usage usage = new();
        str = Regex.Replace(str, @"&nbsp;", "");
        var end = str.IndexOf(')');
        if (end == -1 || end > 76) end = Math.Min(str.IndexOf(' '), str.IndexOf(':'));
        if (end == -1) end = str.Length;
        var start = str.IndexOf('/');
        if (start == -1) start = str.IndexOf('(') + 1;
        if (start > end) start = 0;
        usage.Type = str[start..end];
        try
        {
            usage.Times = int.Parse(Regex.Match(str[..Math.Min(str.Length, 76)], @"\d+").ValueSpan);
        }
        catch (Exception)
        {
            usage.Times = null;
        }
        return _dbContext.Usages?.FirstOrDefault(u => u.Times == usage.Times && u.Type == usage.Type) ?? usage;
    }

    private void SetSpellcasting(ref SpecialAbility specialAbility, HtmlNode sa)
    {
        Spellcasting sc;
        specialAbility.Spellcasting = sc = new();

        sc.Ability = FindCharacteristic(sa.InnerText);
        sc.DifficultyClass = FindSpellcastingDC(sa.InnerText);
        sc.Level = FindSpellcastersLevel(sa.InnerText);
        sc.Modifier = FindSpellcastersModifier(sa.InnerText);
        SetSpellsWithSlots(ref sc, sa);
    }

    private void SetSpellsWithSlots(ref Spellcasting sc, HtmlNode sa)
    {
        if (sc.Slots != null || sc.Spells != null)
            return;

        List<SpellcastingSlot> sl = new();
        List<SpellcastingSpell> scs = new();

        var spellsLinks = sa.SelectNodes("./a");
        if (spellsLinks != null)
        {
            foreach (var spell in spellsLinks)
            {
                var s = @"[Нн]еограниченно[А-Яа-яЁё ,\w(&nbsp;)[\]]+"
                    + spell.InnerText[..spell.InnerText.IndexOf("[")];
                if (Regex.IsMatch(sa.InnerText, s))
                    AddSpell(ref scs, GetUsage("Неограниченно"), spell);
                else
                    throw new Exception("Wrong Usage in ");
            }
        }

        HtmlNode spellsUl = sa.NextSibling;
        IEnumerable<HtmlNode>? slotes;
        if (spellsUl == null)
        {
            slotes = sa.SelectNodes(@".//li");
        }
        else try
            {
                while (spellsUl.Name != "ul")
                {
                    spellsUl = spellsUl.NextSibling;
                }
                slotes = spellsUl.SelectNodes("./li");
            }
            catch (NullReferenceException)
            {
                slotes = sa.ParentNode.ChildNodes.Where(c => c != sa && c.SelectNodes(@".//a")?.Count > 0);
            }

        foreach (var li in slotes)
        {
            AddSpellcastingSpellsAndSlots(ref scs, ref sl, li);
        }

        sc.Slots = sl;
        sc.Spells = scs;
    }

    private void AddSpellcastingSpellsAndSlots(ref List<SpellcastingSpell> sspells, ref List<SpellcastingSlot> sslots, HtmlNode li)
    {
        bool isSlotChosen = false;
        foreach (ESpellLelel sl in Enum.GetValues(typeof(ESpellLelel)))
        {
            var slotName = Slot.GetNameById(sl) ?? throw new Exception();
            if (Regex.IsMatch(li.InnerText.Trim(), $"^{slotName}") && !Regex.IsMatch(li.InnerText, @"\d ?/"))
            {
                if (!int.TryParse(Regex
                    .Match(Regex.Match(li.InnerText, @"\(\d+").Value, @"\d+").ValueSpan, out int times))
                    times = int.MaxValue;

                sslots.Add(new SpellcastingSlot()
                {
                    SlotId = sl,
                    Slot = _dbContext.Slots?.Find(sl) ?? throw new Exception("Wrond spellslot"),
                    Times = times,
                });
                isSlotChosen = true;
                break;
            }
        }
        Usage usage = isSlotChosen
            ? GetUsage(sslots[sslots.Count - 1].SlotId == ESpellLelel.Cantrip
            ? "Неограниченно" : "За ячейку")
            : GetUsage(li.InnerText);

        var spellsLis = li.SelectNodes(@".//a");

        foreach (var spellLi in spellsLis)
        {
            AddSpell(ref sspells, usage, spellLi);
        }
    }

    private void AddSpell(ref List<SpellcastingSpell> sspells, Usage usage, HtmlNode spellLi)
    {
        if (spellLi.InnerText.Length < 2)
            return;

        var spell = _dbContext?.Spells?
                        .Where(s => s.Name.ToLower().StartsWith(spellLi.InnerText.ToLower()))
                        .FirstOrDefault();

        if (spell == null)
        {
            var link = spellLi.Attributes?.FirstOrDefault(a => a.Name == "href")?.Value;

            if (!link!.Contains("spells")) return;

            var parsedSpell = new SpellParser(_dbContext)
                .GetSpellFromUri(new Uri(_baseLink + link))
                .Result;
            var spellName = parsedSpell?.Name?[..parsedSpell.Name.IndexOf('[')];
            spell = _dbContext?.Spells?
            .Where(s => s.Name.StartsWith(spellName))
            .FirstOrDefault();
        }

        var parent = spellLi.ParentNode;
        var endPos = spellLi.InnerText.Contains('[') ? spellLi.InnerText.IndexOf('[') : spellLi.InnerText.Length;
        var s = @$"{Regex.Replace(spellLi.InnerText[..endPos], @"&nbsp;", "") }[]\[\w ]+?\([А-Яа-яЁё \d]+\)";
        var match = Regex.Match(Regex.Replace(parent.InnerText, @"&nbsp;", ""), s);
        if (match.Success)
        {
            usage = GetUsage(usage, Regex.Match(match.Value, @"\([А-Яа-яЁё \d+]+\)").Value);
        }

        sspells.Add(new SpellcastingSpell()
        {
            Spell = spell ?? throw new Exception("Wrong spell in spellcasting"),
            SpellId = spell.SpellId,
            Usage = usage,
        });
    }

    private int FindSpellcastersModifier(string innerText)
    {
        _ = int.TryParse(Regex.Match(
            Regex.Match(innerText, @"\+?\d+ к [паз]").Value,
            @"\d+").ValueSpan, out int res);
        return res;
    }

    private int FindSpellcastersLevel(string innerText)
    {
        _ = int.TryParse(Regex.Match(
            Regex.Match(innerText, @"\d+(-го)? уровн").Value,
            @"\d+").ValueSpan, out int res);
        return res;
    }

    private int? FindSpellcastingDC(string innerText)
    {
        var t = Regex.Match(innerText, @"[Сс]пасброска ?[а-яё (&nbsp;),]+?\d+").Value;
        var success = int.TryParse(Regex
            .Match(t, @"\d+")
            .ValueSpan, out int res);
        return success ? res : null;
    }

    private Characteristic FindCharacteristic(string innerText)
    {
        Characteristic? characteristic = null;
        var baseReg = @"[А-Яа-яЁё’ ,(&nbsp;)]+[Бб]азов[А-Яа-яЁё ’,(&nbsp;)]+";
        foreach (ECharacteristic ch in Enum.GetValues(typeof(ECharacteristic)))
        {
            switch (ch)
            {
                case ECharacteristic.Cha:
                    if (Regex.IsMatch(innerText, @"[Хх]аризм"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
                case ECharacteristic.Con:
                    if (Regex.IsMatch(innerText, @"[Тт]елосло"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
                case ECharacteristic.Dex:
                    if (Regex.IsMatch(innerText, @"[Лл]овкост"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
                case ECharacteristic.Int:
                    if (Regex.IsMatch(innerText, @"[Ии]нтеллект"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
                case ECharacteristic.Str:
                    if (Regex.IsMatch(innerText, @"[Сс]ил[еыа]"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
                case ECharacteristic.Wis:
                    if (Regex.IsMatch(innerText, @"[Мм]удрост"))
                        characteristic = _dbContext.Characteristics?.Find(ch) ?? throw new Exception("Wrong Characteristic");
                    break;
            }
        }
        if (characteristic == null)
            Console.WriteLine("!!!!!!!!-> characteristic in spellcasting not found");

        return characteristic ?? _dbContext.Characteristics?.Find(ECharacteristic.Int) ?? throw new Exception("Wrong Characteristic");//throw new Exception("No characteristic found");
    }

    private void SetConditionImmunities(ref Character character, string innerText)
    {
        if (character.ConditionImmunities != null)
            return;

        character.ConditionImmunities = new List<Condition>();
        var reg = new Regex(@"[А-Яа-я]+ ?с? ?[А-Яа-я]+,?");
        var matches = reg.Matches(innerText[21..]);

        foreach (Match match in matches)
        {
            var name = match.Value.Replace(",", "").Trim();
            var cName = Condition.Names.FirstOrDefault(n => Regex
                        .IsMatch(n, $"[{name.ToUpper()[0]}{name.ToLower()[0]}]{name[1..Math.Min(4, name.Length)]}"));
            ECondition condition;
            if (cName != null)
            {
                condition = (ECondition)Condition.Names.IndexOf(cName) + 1;
            }
            else
            {
                if (Regex.IsMatch(name, "^[Гг]лух")) condition = ECondition.Deafened;
                else if (Regex.IsMatch(name, "^[Сс]би[вт]")) condition = ECondition.Prone;
                else if (Regex.IsMatch(name, "^[Зз]ахв")) condition = ECondition.Grappled;
                else if (Regex.IsMatch(name, "^[Оо]шел")) condition = ECondition.Stunned;
                else if (Regex.IsMatch(name, @"[\w\W]"))
                    condition = ECondition.Stunned;
                else
                    throw new Exception($"Wrong condition: {name}");
            }
            character.ConditionImmunities.Add(_dbContext.Conditions?.Find(condition) ?? throw new Exception($"Wrong condition: {name}"));
        }

        var num = innerText.Count(c => c == ',') + 1;
        if (num != character.ConditionImmunities?.Count)
            throw new Exception($"Wrong dam resistnce number: {character.ConditionImmunities?.Count}" +
                $"commas number: {num - 1}");
    }

    private void SetDamageVulnerabilities(ref Character character, string innerText)
    {
        if (character.DamageVulnerabilities != null)
            return;

        character.DamageVulnerabilities = new List<DamageVulnerabilitie>();

        var damages = FindDamageTypes(innerText, 18);
        foreach (var damage in damages)
        {
            character.DamageVulnerabilities.Add(new DamageVulnerabilitie()
            {
                DamageType = damage
            });
        }
    }

    private void SetDamageImunities(ref Character character, string innerText)
    {
        if (character.DamageImmunities != null)
            return;

        character.DamageImmunities = new List<DamageImmunitie>();

        var damages = FindDamageTypes(innerText, 17);
        foreach (var damage in damages)
        {
            character.DamageImmunities.Add(new DamageImmunitie()
            {
                DamageType = damage
            });
        }
    }

    private void SetDamageResistances(ref Character character, string innerText)
    {
        if (character.DamageResistances != null)
            return;

        character.DamageResistances = new List<DamageResistance>();
        var damages = FindDamageTypes(innerText, 19);
        foreach (var damage in damages)
        {
            character.DamageResistances.Add(new DamageResistance()
            {
                DamageType = damage
            });
        }
    }

    private List<DamageType> FindDamageTypes(string innerText, int startChar)
    {
        List<DamageType> damageTypes = new();
        var reg = new Regex(@"[А-Яа-я]+ ?[А-Яа-я]+,?");
        var matches = reg.Matches(innerText[startChar..]);
        foreach (Match match in matches)
        {
            DamageType damageType;
            var name = match.Value.Replace(",", "").Trim();
            if (name.Contains(' ') && !Regex.IsMatch(name, " энерги"))
            {
                damageType = _dbContext.DamageTypes?.FirstOrDefault(dt => dt.Name == name)
                            ?? new DamageType() { Name = name };
                damageTypes.Add(damageType);
            }
            else
            {
                var dName = DamageType.Names.FirstOrDefault(n => Regex
                        .IsMatch(n, $"[{name.ToUpper()[0]}{name.ToLower()[0]}]{name[1..Math.Min(3, name.Length)]}"));
                EDamageType type;
                if (dName == null)
                {
                    if (Regex.IsMatch(match.Value, "[Ээ]лектр")) type = EDamageType.Ligthning;
                    else if (Regex.IsMatch(match.Value, "[Оо]го")) type = EDamageType.Fire;
                    else if (Regex.IsMatch(match.Value, "[Яя]д")) type = EDamageType.Poison;
                    else
                        throw new Exception("Wrong Damage Type: " + match.Value);
                }
                else
                {
                    type = (EDamageType)(DamageType.Names.IndexOf(dName) + 1);
                }
                damageType = _dbContext.DamageTypes?.Find(type)
                            ?? throw new Exception($"Wrong type: {type}");
                damageTypes.Add(damageType);
            }
        }

        var num = innerText.Count(c => c == ',') + 1;
        if (num != damageTypes.Count)
            throw new Exception($"Wrong dam resistnce number: {damageTypes.Count}" +
                $"commas number: {num - 1}");

        return damageTypes;
    }

    private void SetSenses(ref Character character, string innerText)
    {
        if (character.Senses != null)
            return;

        var senseStr = Regex.Replace(innerText, @"Чувств[ао]?.? ?", "");

        character.Senses = new List<MonsterSense>();
        var reg = new Regex(@"[А-Яа-яё]+ ?[А-Яа-яё]+ \d+");
        var matches = reg.Matches(senseStr);
        foreach (Match match in matches)
        {
            var name = Regex.Match(match.Value, @"[А-Яа-яё]+ ?[А-Яа-яё]+").Value.ToLower();
            var value = int.Parse(Regex.Match(match.Value, @"\d+").ValueSpan);
            if (name == "или")
            {
                name = Regex.Match(innerText, $"{match.Value} ?[А-Яа-яё]+ ?[А-Яа-яё]+").Value
                    .Replace(match.Value, "").Trim().ToLower();
            }
            var sense = _dbContext.Senses?.FirstOrDefault(l => l.Name == name)
                ?? new Sense() { Name = name };
            character.Senses?.Add(new MonsterSense() { Sense = sense, Distance = value });
        }

        if (character.Senses?.Count != Regex.Matches(innerText, @"\d+").Count)
            throw new Exception($"Some sense was missed in '{innerText}'");
    }

    private void SetLanguages(ref Character character, string innerText)
    {
        var langReg = new Regex(@"Языки?.? ?[А-Яа-я ,()]+");
        var langSpan = langReg.Match(innerText).Value;
        var langStr = Regex.Replace(langSpan, @"Языки?.? ?", "");
        var langLst = langStr.Split(',');
        character.Languages = new List<Language>();
        foreach (var langName in langLst)
        {
            var langNameTrimed = langName.Trim().ToLower();
            var lang = _dbContext.Languages?.FirstOrDefault(l => l.Name == langNameTrimed);
            character.Languages.Add(lang ?? new Language() { Name = langNameTrimed });
        }
    }

    private void SetProfBonus(ref Character character, string innerText)
    {
        if (int.TryParse(Regex.Match(innerText, @"\d+").ValueSpan, out int res))
            character.ProficiencyBonus = res;
        else
            character.ProficiencyBonus = int.MaxValue;
    }

    private void SetChallengeRatingAndExp(ref Character character, string innerText)
    {
        Regex lesserThenOneCRReg = new(@"/\d");
        Regex numReg = new(@"\d+");
        var lst = innerText.Split('-');

        if (lst.Length < 2)
            return;

        var crMathch = lesserThenOneCRReg.Match(lst[0]);
        if (crMathch.Success)
        {
            character.ChallengeRating = 1.0f / float.Parse(numReg.Match(crMathch.Value).ValueSpan);
        }
        else
        {
            character.ChallengeRating = float.Parse(numReg.Match(lst[0]).ValueSpan);
        }

        character.Experience = int.Parse(numReg.Match(lst[1]).ValueSpan);
    }

    private void SetProficiencies(ref Character character, string innerText)
    {
        Regex profReg = new(@"[А-Я]{1,3}[ а-я]+ ?\+?\d+");
        var matches = profReg.Matches(innerText);
        if (character.Proficiencies == null) character.Proficiencies = new List<MonsterProficiency>();
        Regex nameReg = new(@"[А-яа-я ]+");
        Regex modifierReg = new(@"\d+");
        foreach (var matchO in matches)
        {
            var match = matchO as Match;
            if (match == null) continue;
            string name = nameReg.Match(match.Value).Value;
            int modifier = int.Parse(modifierReg.Match(match.Value).ValueSpan);
            var prof = _dbContext.Proficiencies?.FirstOrDefault(p => p.Name.Equals(name))
                ?? new Proficiency() { Name = name };
            character.Proficiencies.Add(new() { Proficiency = prof, Modifier = modifier });
        }
    }

    private void SetCharacteristics(ref Character character, string innerText)
    {
        Regex num = new(@"\d+");
        Regex chReg;
        int i = 0;
        foreach (var ch in Enum.GetValues(typeof(ECharacteristic)))
        {
            var chEn = (ECharacteristic)ch;
            var chStr = Characteristic.GetNameById(chEn);
            chReg = new Regex(@$"[{chStr.ToUpper()}{chStr.ToLower()}]{"{3}"} ?\d+");
            var match = chReg.Match(innerText);
            if (match.Success)
            {
                switch (chEn)
                {
                    case ECharacteristic.Cha:
                        i++;
                        character.Charisma = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    case ECharacteristic.Con:
                        i++;
                        character.Constitution = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    case ECharacteristic.Dex:
                        i++;
                        character.Dexterity = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    case ECharacteristic.Int:
                        i++;
                        character.Intelligence = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    case ECharacteristic.Str:
                        i++;
                        character.Strength = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    case ECharacteristic.Wis:
                        i++;
                        character.Wisdom = int.Parse(num.Match(match.Value).ValueSpan);
                        break;
                    default:
                        throw new Exception($"Wrong ECharacteristic: {chEn}");
                }
            }
        }
        if (i != 6) throw new Exception($"Not all Characteristic has been set: {i}");
    }

    private void SetSpeeds(ref Character character, string innerText)
    {
        var speeds = new List<MonsterSpeed>();
        Regex re;
        var num = new Regex(@"\d+");
        foreach (var speed in Enum.GetValues(typeof(ESpeedType)))
        {
            var spType = (ESpeedType)speed;
            switch (spType)
            {
                case ESpeedType.Walk:
                    re = new Regex(@"корость \d+");
                    break;
                case ESpeedType.Fly:
                case ESpeedType.Swim:
                case ESpeedType.Climb:
                case ESpeedType.Burrow:
                    re = new Regex($@"{SpeedType.GetNameById(spType).ToLower()} \d+");
                    break;
                default:
                    throw new Exception("Wrong speed type");
            }
            var match = re.Match(innerText);
            if (match.Success)
            {
                var mst = _dbContext.SpeedTypes?.Find(speed);
                if (mst == null) throw new Exception("No such speed type in DB");
                int val = int.Parse(num.Match(match.Value).ValueSpan);
                speeds.Add(new()
                {
                    SpeedType = mst,
                    Value = val,
                });
            }
        }
        character.Speeds = speeds;
    }

    private void SetHitPointsAndDices(ref Character character, string innerText)
    {
        character.InitialHitPoints = int.Parse(new Regex(@"\d+").Match(innerText).Value);
        character.CurrentHitPoints = character.InitialHitPoints;
        character.HitDice = new Regex(@"\d+к\d+").Match(innerText).Value;
    }

    private void SetArmorClass(ref Character character, string str)
    {
        var acRe = new Regex(@"\d+").Match(str);
        if (acRe.Success)
        {
            var ac = acRe.ValueSpan;
            character.ArmorClass = int.Parse(ac);
        }
    }

    private void SetCharAllingthement(ref Character character, string innerText)
    {
        var lit = innerText.Split(",");
        if (lit.Length < 2) return;
        var allName = lit?[^1]?.Trim();
        var allign = _dbContext.Allignment?.Where(x => x.Name == allName).FirstOrDefault();
        character.Allignment = allign ?? new() { Name = allName };
    }

    private void SetCharTypeAndSize(ref Character character, string innerText)
    {
        var typeLst = innerText.Split(",");
        string typeName = typeLst.Length > 2
            ? string.Join(',', typeLst[..^1]).Trim()
            : typeLst[0].Trim();

        var type = _dbContext.MonsterTypes?.Where(t => t.Name == typeName).FirstOrDefault();
        character.MonsterType = type ?? new() { Name = typeName };

        string sizeName = typeName[..1];
        var size = _dbContext.MonsterSizes?.Where(t => t.Name.StartsWith(sizeName)).FirstOrDefault();
        character.MonsterSize = size ?? throw new Exception($" Wrong size: {sizeName}");
    }

    private void SetCharName(ref Character character, HtmlDocument doc)
    {
        character.Name = doc.DocumentNode.SelectNodes("//h2[@class='card-title ']")[0].InnerText;
    }
}