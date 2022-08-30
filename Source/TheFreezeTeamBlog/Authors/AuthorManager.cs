namespace TheFreezeTeamBlog.Authors
{
  using Statiq.Common;
  using Statiq.Web;
  using System.Collections.Generic;

  public class AuthorManager
  {
    public const string NoAvatar = "/authorAvatars/noavatar.jpg";
    public Dictionary<string, Author> authors { get; set; }
    public AuthorManager()
    {
      authors = new Dictionary<string, Author>();
      authors.Add("Steven T. Cramer", new Author { Name = "Steven T. Cramer", CodingGame = "729d2f5ebf2a12fc3bbefc3c13fa4f463434423", Twitter = "Twitter", LinkedIn = "steventcramer", AvatarPath = "/authorAvatars/steve.jpeg", Bio = "Get smarter every day.", Discord = "957829331474849802" });
      authors.Add("Mike Yoshino", new Author { Name = "Mike Yoshino", CodingGame = "ede69031e8671a592c04562a69d92a250402493", Facebook = "yoshinoookami", LinkedIn = "tsubagi-yoshino-503441171", Bio = ":) Welcome to my profile!", Twitter = "Twitter", AvatarPath = "/authorAvatars/MikeYoshino.jpg", Discord = "629559177190309910" });
      authors.Add("Stefan Bemelmans", new Author { Name = "Stefan Bemelmans", Twitter = "Twitter", AvatarPath = "/authorAvatars/Stefan.jpg", Bio = "Computers and Food", Discord = "386790410460332032" });
      authors.Add("Kevin Dietz", new Author { Name = "Kevin Dietz", CodingGame = "CodingGameId", Facebook = "FacebookId", Twitter = "Twitter", AvatarPath = "/authorAvatars/noavatar.jpg", Discord= "497944319227985920" });
      authors.Add("Ilyana Smith", new Author { Name = "Ilyana Smith", Twitter = "ilyanaDev", AvatarPath = "/authorAvatars/ilyanaSmith.jpg"});
    }

    public string AddImagePath(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey))
      {
        return authors[authorKey].AvatarPath;
      }else
      {
        return NoAvatar;
      }
    }
    public string AddAuthorFacebook(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(authors[authorKey].Facebook))
      {
        return authors[authorKey].Facebook;
      }
      else
      {
        return string.Empty;
      }
    }
    public string AddAuthorCodingGame(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(authors[authorKey].CodingGame))
      {
        return authors[authorKey].CodingGame;
      }
      else
      {
        return string.Empty;
      }
    }

    public string AddAuthorTwitter(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(authors[authorKey].Twitter))
      {
        return authors[authorKey].Twitter;
      }
      else
      {
        return string.Empty;
      }
    }

    public string AddAuthorBio(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) &&!string.IsNullOrEmpty(authors[authorKey].Bio))
      {
        return authors[authorKey].Bio;
      }
      else
      {
        return string.Empty;
      }
    }
    public string AddAuthorLinkedIn(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(authors[authorKey].LinkedIn))
      {
        return authors[authorKey].LinkedIn;
      }
      else
      {
        return string.Empty;
      }
    }

    public string AddAuthorDiscord(IDocument doc)
    {
      string authorKey = doc.GetString(WebKeys.Author);
      if (authorKey != null && authors.ContainsKey(authorKey) && !string.IsNullOrEmpty(authors[authorKey].Discord))
      {
        return authors[authorKey].Discord;
      }
      else
      {
        return string.Empty;
      }
    }
  }
}
