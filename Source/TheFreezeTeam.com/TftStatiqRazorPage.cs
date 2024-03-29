﻿namespace TheFreezeTeam.Com;

using Statiq.Razor;
using System.Text.Json;
using TheFreezeTeam.Com.Models;

public abstract class TftStatiqRazorPage<TModel> : StatiqRazorPage<TModel>
  where TModel : IDocument
{

  #region Document
  public string? Description => Document.GetDescription();
  public string? Title => Document.GetTitle();
  public DateTime Published => Document.GetPublished();
  public string PublishedDate => Document.GetPublished().ToLongDateString();
  public string FullLink => Document.GetFullLink();
  public DocumentList<IDocument> Posts => Document.GetChildren();
  public IDocument? FeaturedPost => Posts.Count > 0 ? Posts[0]: null;
  public IEnumerable<IDocument> NonFeaturedPosts => Posts.Skip(1);
  public int PostCount => Posts.Count;
  public string CoverImageFullLink => Context.GetLink(Document.GetImageUrl(), true);
  #endregion

  #region Context
  public string? FavIconLink => Context.GetLink("favicon.ico");
  public string? SiteTitle => Context.GetString(MetaDataKeys.SiteTitle);
  #endregion

  #region  Outputs
  public bool IsPost => Outputs.FilterSources(Context.GetString(MetaDataKeys.PostSources)).ContainsById(Document);
  public FilteredDocumentList<IDocument> AtomFeeds => Outputs["**/*.atom"];
  public FilteredDocumentList<IDocument> RssFeeds => Outputs["**/*.rss"];

  public IOrderedEnumerable<IDocument> NavBarPages =>
    OutputPages.GetChildrenOf("index.html")
    .Where(x => x.GetBool(MetaDataKeys.ShowInNavbar, true))
    .OrderBy(x => x.GetInt(Keys.Order));


  private readonly Lazy<IReadOnlyDictionary<string, Author>> _Authors;

  public IReadOnlyDictionary<string, Author> Authors => _Authors.Value;

  #endregion
  protected TftStatiqRazorPage()
  {
    _Authors = new Lazy<IReadOnlyDictionary<string, Author>>(() => GetAuthors());
  }

  public string PageTitle => $"{SiteTitle} - {Title}".Trim(new[] { ' ', '-' });

  private Dictionary<string, Author> GetAuthors()
  {
    const string AuthorsPath = "data/authors.json";
    IDocument? authorsDocument =
    Outputs
      .FromPipeline("Data")
      .FilterSources(AuthorsPath)
      .FirstOrDefault();

    if (authorsDocument == null) return new Dictionary<string, Author>();

    var authorsStream = authorsDocument.GetContentStream();
    var authors = JsonSerializer.Deserialize<Dictionary<string, Author>>(authorsStream);

    if (authors == null) throw new ArgumentNullException($"{AuthorsPath} is a required file!");

    return authors;
  }
}
