using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using TableTopBattleTracker.Data;
using TableTopBattleTracker.Model;



var bestiaryDocument = await GetDocumentAsync(new Uri("https://dnd.su/bestiary/"));
var monsterLinkedLis = bestiaryDocument.DocumentNode.SelectNodes("//li[@class='for_filter']");

DbContextOptionsBuilder optionsBuilder = new();
var opt = optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BattleTrackerDb;UserId=user;Password=1;").Options;

using(var dbContext = new AppDbContext(opt))
{

}


static async Task<HtmlDocument> GetDocumentAsync(Uri uri)
{
    HttpClient httpClient = new();
    var page = await httpClient.GetStringAsync(uri);
    HtmlDocument htmlDocument = new();
    htmlDocument.LoadHtml(page);
    return htmlDocument;
}
