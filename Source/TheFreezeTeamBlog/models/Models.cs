namespace TheFreezeTeamBlog.Models;

using Statiq.Common;

public record BaseModel(IDocument Document, IExecutionContext Context)
{
  const string GitHubRepoUrl = "https://github.com/TheFreezeTeam/TheFreezeTeamBlog";
  private string MarkdownPath => Document.Source.GetRelativeInputPath().ChangeExtension("md").ToString();

  /// <summary>
  /// Author of the post.
  /// </summary>
  /// <remarks>Source: Front matter of the post markdown file</remarks>
  /// <example>blazor-state-tutorial.md</example>
  public string Author => Document.GetString(WebKeys.Author);
  public string AuthorImage => Document.GetString(TftKeys.AuthorImage);  
  public string AuthorTwitter => Document.GetString(TftKeys.AuthorTwitter);
  public string AuthorFaceboook => Document.GetString(TftKeys.AuthorFacebook);
  public string AuthorLinkedIn => Document.GetString(TftKeys.AuthorLinkedIn);
  public string AuthorCodingGame => Document.GetString(TftKeys.AuthorCodingGame);
  public string AuthorDiscord => Document.GetString(TftKeys.AuthorDiscord);
  public string AuthorBio => Document.GetString(TftKeys.AuthorBio);
  
  public string CoverImageUrl => Document.GetString(TftKeys.CoverImageUrl);
  /// <summary>
  /// The Description of the post.
  /// </summary>
  /// <remarks>Source: Front matter of the post markdown file</remarks>
  /// <example>blazor-state-tutorial.md</example>
  public string Description => Document.GetString(WebKeys.Description);
  public string Excerpt => Document.GetString(Keys.Excerpt);
  public string ReadTime => Document.GetString(TftKeys.ReadTime);
  public string FullLink => Document.GetLink(true);
  public string GitHubEditUrl => $"{GitHubRepoUrl}/edit/master/Source/TheFreezeTeamBlog/input/{MarkdownPath}";
  public bool IsPost => Document.GetBool(TftKeys.IsPost);
  public bool HasCoverImage => !string.IsNullOrEmpty(CoverImageUrl);
  public string PageTitle => Document.GetString(TftKeys.PageTitle);
  public string Tags => Document.GetString(TftKeys.Tags);
  public FilteredDocumentList<IDocument> TagsOutput  => Context.Outputs["tags.html"];
  public FilteredDocumentList<IDocument> RssFeeds = Context.Outputs["**/*.rss"];
  public FilteredDocumentList<IDocument> AtomFeeds = Context.Outputs["**/*.atom"];

  public string Published=> Document.GetDateTime(WebKeys.Published).ToLongDateString();

  #region Context Based
  public string FavIconLink => Context.GetLink("/favicon.ico");
  public string CoverImageFullLink => Context.GetLink(CoverImageUrl,true);
  #endregion
};

public record MetaTags(IDocument Document, IExecutionContext Context) :
  BaseModel(Document, Context);
