namespace TheFreezeTeam.Com;

using Statiq.Razor;

public abstract class TftStatiqRazorPage<TModel> : StatiqRazorPage<TModel>
  where TModel : IDocument
{
  public string? FavIconLink => Context.GetLink("favicon.ico");
  public string PageTitle => $"{SiteTitle} - {Title}".Trim(new[] { ' ', '-' });
  public string? Description => Document.GetString(WebKeys.Description);
  public string? Title => Document.GetString(WebKeys.Title);
  public string? SiteTitle => Context.GetString(MetaDataKeys.SiteTitle);
  public bool IsPost => Outputs.FilterSources(Context.GetString(MetaDataKeys.PostSources)).ContainsById(Document);
  public DateTime Published => Document.GetDateTime(WebKeys.Published);
  public string FullLink => Document.GetLink(true);
  public FilteredDocumentList<IDocument> RssFeeds => Outputs["**/*.rss"];
  public FilteredDocumentList<IDocument> AtomFeeds => Outputs["**/*.atom"];
}
