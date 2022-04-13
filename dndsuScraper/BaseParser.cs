using HtmlAgilityPack;
using TableTopBattleTracker.Data;
using TableTopBattleTracker.Model;

namespace dndsuScraper;

public class BaseParser
{
    protected readonly AppDbContext _dbContext;
    protected string _baseLink { get; } = "https://dnd.su/";

    public BaseParser(AppDbContext context)
    {
        _dbContext = context;
    }

    public static async Task<HtmlDocument> GetDocumentAsync(Uri uri)
    {
        HttpClient httpClient = new();
        var page = await httpClient.GetStringAsync(uri);
        HtmlDocument htmlDocument = new();
        htmlDocument.LoadHtml(page);
        return htmlDocument;
    }

    public static string? GetLinkFromNode(HtmlNode node)
    {
        var link = node.SelectNodes("./a")[0];
        return link.GetAttributeValue("href", null);
    }

    public void SeedBaseTables()
    {

        var ae = new List<AreaOfEffect>();
        foreach (var id in Enum.GetValues(typeof(EAreaType)))
        {
            ae.Add(new AreaOfEffect()
            {
                AreaOfEffectId = (EAreaType)id,
                Name = AreaOfEffect.GetNameById((EAreaType)id),
            });
        }
        _dbContext.AreaOfEffects?.AddRange(ae);
        var cc = new List<CastingComponent>();
        foreach (var id in Enum.GetValues(typeof(ECastingComponent)))
        {
            cc.Add(new CastingComponent()
            {
                CastingComponentId = (ECastingComponent)id,
                Name = CastingComponent.GetNameById((ECastingComponent)id)
            });
        }
        _dbContext.CastingComponents?.AddRange(cc);

        var ch = new List<Characteristic>();
        foreach (var id in Enum.GetValues(typeof(ECharacteristic)))
        {
            ch.Add(new Characteristic()
            {
                CharacteristicId = (ECharacteristic)id,
                Name = Characteristic.GetNameById((ECharacteristic)id),
            });
        }
        _dbContext.Characteristics?.AddRange(ch);

        var co = new List<Condition>();
        foreach (var id in Enum.GetValues(typeof(ECondition)))
        {
            co.Add(new Condition()
            {
                ConditionId = (ECondition)id,
                Name = Condition.GetNameById((ECondition)id),
            });
        }
        _dbContext.Conditions?.AddRange(co);

        var dt = new List<DamageType>();
        foreach (var id in Enum.GetValues(typeof(EDamageType)))
        {
            dt.Add(new DamageType()
            {
                DamageTypeId = (EDamageType)id,
                Name = DamageType.GetNameById((EDamageType)id),
            });
        }
        _dbContext.DamageTypes?.AddRange(dt);

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
        _dbContext.MonsterSizes?.AddRange(mt);

        var sl = new List<Slot>();
        foreach (var id in Enum.GetValues(typeof(ESpellLelel)))
        {
            sl.Add(new Slot()
            {
                SlotId = (ESpellLelel)id,
                Name = Slot.GetNameById((ESpellLelel)id),
            });
        }
        _dbContext.Slots?.AddRange(sl);

        var st = new List<SpeedType>();
        foreach (var id in Enum.GetValues(typeof(ESpeedType)))
        {
            st.Add(new SpeedType()
            {
                SpeedTypeId = (ESpeedType)id,
                Name = SpeedType.GetNameById((ESpeedType)id),
            });
        }
        _dbContext.SpeedTypes?.AddRange(st);

        _dbContext.SaveChanges();
    }

}

