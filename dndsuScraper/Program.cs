using dndsuScraper;
using Microsoft.EntityFrameworkCore;
using TableTopBattleTracker.Data;


DbContextOptionsBuilder optionsBuilder = new();
var opt = optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BattleTrackerDb;UserId=user;Password=1;").Options;

using (var dbContext = new AppDbContext(opt))
{
    var spellParser = new SpellParser(dbContext);
    //SeedBaseTables(dbContext);
    await spellParser.CollectSpells();
}
