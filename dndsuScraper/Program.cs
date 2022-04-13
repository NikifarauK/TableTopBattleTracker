using dndsuScraper;
using Microsoft.EntityFrameworkCore;
using TableTopBattleTracker.Data;

DbContextOptionsBuilder optionsBuilder = new();
var opt = optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BattleTrackerDb;UserId=user;Password=1;").Options;

using (var dbContext = new AppDbContext(opt))
{
    dbContext.Database.EnsureCreated();
    /*
    dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    var sp = dbContext.Spells?.Include(s => s.SpellDamages)
        .Include(s => s.SpellSchool).Include(s => s.CastingComponents)
        .Include(s => s.AreaOfEffect)?
        .Include(s => s.CastRange)?.Include(s => s.CastTime)?
        .Include(s => s.SpellDamages).ThenInclude(sd => sd.SpellDamageValues)
        .Where(s => s.Name.StartsWith("Да")).Take(8);

    var sql = sp?.ToQueryString();
    Console.WriteLine(sql);
    foreach(var t in sp)
    {
        Console.WriteLine(t.Name);
    }
    var spellParser = new SpellParser(dbContext);
    spellParser.SeedBaseTables();
    await spellParser.CollectSpells();
    */
    var monsterParser = new MonsterParser(dbContext);
    await monsterParser.CollectCharacters();
}
