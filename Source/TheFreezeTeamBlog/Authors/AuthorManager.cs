namespace TheFreezeTeamBlog.Authors;

using Statiq.Common;
using Statiq.Web;
using System.Collections.Generic;

public class AuthorManager
{
  public const string NoAvatar = "/author-avatars/noavatar.jpg";
  public IReadOnlyDictionary<string, Author> Authors { get; set; }
  public AuthorManager()
  {
    Authors = new Dictionary<string, Author>()
    {
      {
        "Steven T. Cramer",
        new Author
        (
          Name: "Steven T. Cramer",
          AvatarPath: "/author-avatars/steven-t-cramer.jpeg",
          Bio: "Get smarter every day.",
          CodingGame: "729d2f5ebf2a12fc3bbefc3c13fa4f463434423",
          Discord: "957829331474849802",
          Facebook: null,
          LinkedIn: "steventcramer",
          Twitter: "steventcramer"
        )
      },
      {
        "Mike Yoshino",
        new Author
        (
          Name: "Mike Yoshino",
          Bio: ":) Welcome to my profile!",
          CodingGame: "ede69031e8671a592c04562a69d92a250402493",
          Facebook:"yoshinoookami",
          LinkedIn: "tsubagi-yoshino-503441171",
          Twitter: "YoshinoSupakorn",
          AvatarPath: "/author-avatars/mike-yoshino.jpg",
          Discord: "629559177190309910"
        )
      },
      {
        "Stefan Bemelmans",
        new Author
        (
          Name: "Stefan Bemelmans",
          AvatarPath: "/author-avatars/stefan-bemelmans.jpg",
          Bio: "Computers and Food",
          Discord: "386790410460332032",
          Twitter: "Twitter"
        )
      },
      {
        "Kevin Dietz",
        new Author
        (
          Name: "Kevin Dietz",
          AvatarPath: "/author-avatars/no-avatar.jpg",
          Twitter: "kevinknowscs",
          Discord: "497944319227985920"
        )
      },
      {
        "Ilyana Smith",
        new Author
        (
          Name: "Ilyana Smith",
          Twitter: "ilyanaDev",
          AvatarPath: "/author-avatars/ilyana-smith.jpg"
        )
      }
    };
  }

  public string AddImagePath(IDocument document)
  {
    string authorKey = document.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey))
    {
      return Authors[authorKey].AvatarPath;
    }else
    {
      return NoAvatar;
    }
  }
  public string? AddAuthorFacebook(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(Authors[authorKey].Facebook))
    {
      return Authors[authorKey].Facebook;
    }
    else
    {
      return string.Empty;
    }
  }
  public string? AddAuthorCodingGame(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(Authors[authorKey].CodingGame))
    {
      return Authors[authorKey].CodingGame;
    }
    else
    {
      return string.Empty;
    }
  }

  public string? AddAuthorTwitter(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(Authors[authorKey].Twitter))
    {
      return Authors[authorKey].Twitter;
    }
    else
    {
      return string.Empty;
    }
  }

  public string? AddAuthorBio(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) &&!string.IsNullOrEmpty(Authors[authorKey].Bio))
    {
      return Authors[authorKey].Bio;
    }
    else
    {
      return string.Empty;
    }
  }
  public string? AddAuthorLinkedIn(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(Authors[authorKey].LinkedIn))
    {
      return Authors[authorKey].LinkedIn;
    }
    else
    {
      return string.Empty;
    }
  }

  public string? AddAuthorDiscord(IDocument doc)
  {
    string authorKey = doc.GetString(WebKeys.Author);
    if (authorKey != null && Authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(Authors[authorKey].Discord))
    {
      return Authors[authorKey].Discord;
    }
    else
    {
      return string.Empty;
    }
  }
}
