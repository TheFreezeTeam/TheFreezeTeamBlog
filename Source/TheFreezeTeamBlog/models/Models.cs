namespace TheFreezeTeamBlog.Models;

using Statiq.Common;

public record BaseModel(IDocument Document, IExecutionContext Context)
{
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
  public string PostImageFullLink => Document.GetLink(PostImageUrl,true);
  public string AuthorTwitter => Document.GetString(TftKeys.AuthorTwitter);
  public string FavIconLink => Context.GetLink("/favicon.ico");

  const string gitHubRepoUrl = "https://github.com/TheFreezeTeam/TheFreezeTeamBlog";
  private string markdownPath => Document.Source.GetRelativeInputPath().ChangeExtension("md").ToString();
  public string GitHubEditUrl => $"{gitHubRepoUrl}/edit/master/Source/TheFreezeTeamBlog/input/{markdownPath}";
};

public record MetaTags(IDocument Document, IExecutionContext Context) :
  BaseModel(Document, Context);
