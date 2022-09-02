namespace TheFreezeTeamBlog.Models;

using Statiq.Common;

public record BaseModel(IDocument Document, IExecutionContext Context)
{
  const string GitHubRepoUrl = "https://github.com/TheFreezeTeam/TheFreezeTeamBlog";
  /// <summary>
  /// Author of the post.
  /// </summary>
  /// <remarks>Source: Front matter of the post markdown file</remarks>
  /// <example>blazor-state-tutorial.md</example>
  public string Author => Document.GetString(WebKeys.Author);

  /// <summary>
  /// The Description of the post.
  /// </summary>
  /// <remarks>Source: Front matter of the post markdown file</remarks>
  /// <example>blazor-state-tutorial.md</example>
  public string Description => Document.GetString(WebKeys.Description);
  public string FullLink => Document.GetLink(true);
  public string PageTitle => Document.GetString(TftKeys.PageTitle);
  public string PostImageUrl => Document.GetString(TftKeys.PostImageUrl);
  public string AuthorTwitter => Document.GetString(TftKeys.AuthorTwitter);

  private string markdownPath => Document.Source.GetRelativeInputPath().ChangeExtension("md").ToString();
  public string GitHubEditUrl => $"{GitHubRepoUrl}/edit/master/Source/TheFreezeTeamBlog/input/{markdownPath}";

  #region Context Based
  public string FavIconLink => Context.GetLink("/favicon.ico");
  public string PostImageFullLink => Context.GetLink(PostImageUrl,true);
  #endregion
};

public record MetaTags(IDocument Document, IExecutionContext Context) :
  BaseModel(Document, Context);
