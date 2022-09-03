namespace TheFreezeTeamBlog.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IDocumentExtensions
{
  public static string Author(this IDocument) => Document.GetString(WebKeys.Author);
  public static string AuthorImage(this IDocument) => Document.GetString(WebKeys.AuthorImage);
  public string AuthorTwitter(this IDocument) => Document.GetString(TftKeys.AuthorTwitter);
  public string AuthorFaceboook(this IDocument) => Document.GetString(TftKeys.AuthorFacebook);
  public string AuthorLinkedIn(this IDocument) => Document.GetString(TftKeys.AuthorLinkedIn);
  public string AuthorCodingGame(this IDocument) => Document.GetString(TftKeys.AuthorCodingGame);
  public string AuthorDiscord(this IDocument) => Document.GetString(TftKeys.AuthorDiscord);
  public string AuthorBio(this IDocument) => Document.GetString(TftKeys.AuthorBio);
  public string CoverImageUrl(this IDocument) => Document.GetString(TftKeys.CoverImageUrl);
  public string Description(this IDocument) => Document.GetString(WebKeys.Description);
  public string Excerpt => Document.GetString(Keys.Excerpt);
}
