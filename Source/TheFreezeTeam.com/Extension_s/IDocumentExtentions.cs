namespace TheFreezeTeam.Com;

using Statiq.Razor;

// public static class IContextExtentions
// {
//   public static string? FavIconLink(this IContext context) => context.GetLink("favicon.ico");
// }

public static class IDocumentExtentions
{
  // public string PageTitle => $"{SiteTitle} - {Title}".Trim(new[] { ' ', '-' });
  public static string? GetDescription(this IDocument document) => document.GetString(WebKeys.Description);

  // public string? SiteTitle(this IDocument document) => document.Context.GetString(MetaDataKeys.SiteTitle);
  // public bool IsPost => Outputs.FilterSources(Context.GetString(MetaDataKeys.PostSources)).ContainsById(Document);
  public static DateTime GetPublished(this IDocument document) => document.GetDateTime(WebKeys.Published);
  public static string GetFullLink(this IDocument document) => document.GetLink(true);
  // public FilteredDocumentList<IDocument> RssFeeds => Outputs["**/*.rss"];
  // public FilteredDocumentList<IDocument> AtomFeeds => Outputs["**/*.atom"];
  // public int PostCount => Posts.Count;
  // public FilteredDocumentList<IDocument> Posts => Outputs["posts/**/*.md"];
  // public IDocument? FeaturedPost => Posts.FirstOrDefault();
  // public IEnumerable<IDocument> NonFeaturedPosts => Posts.Skip(1);

  public static bool HasImage(this IDocument document) => document.ContainsKey(WebKeys.Image);
  public static string GetImageUrl(this IDocument document) => document.GetString(MetaDataKeys.ImageUrl);
  public static IReadOnlyList<string>? GetTags(this IDocument document) =>
    document.GetList<string>(MetaDataKeys.Tags);
}
