using HtmlAgilityPack;
using TableTopBattleTracker.Data;

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

}

