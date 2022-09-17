namespace TheFreezeTeamBlog.Authors
{

  public record Author
  (
    string Name,
    string AvatarPath,
    string? Bio = null,
    string? CodingGame = null,
    string? Discord = null,
    string? Facebook = null,
    string? LinkedIn = null,
    string? Twitter = null
  );
  //{
  //  public Author(string aName, string? aFacebook, string? aTwitter, string? aCodingGame, string aAvatarPath, string? aBio, string? aLinkedIn, string? aDiscord)
  //  {
  //    Name = aName;
  //    Facebook = aFacebook;
  //    Twitter = aTwitter;
  //    CodingGame = aCodingGame;
  //    AvatarPath = aAvatarPath;
  //    Bio = aBio;
  //    LinkedIn = aLinkedIn;
  //    Discord = aDiscord;
  //  }

  //  public string Name { get; set; }
  //  public string? Facebook { get; set; }
  //  public string? Twitter { get; set; }
  //  public string? CodingGame { get; set; }
  //  public string AvatarPath { get; set; }
  //  public string? Bio { get; set; }
  //  public string? LinkedIn { get; set; }
  //  public string? Discord { get; set; }

  //}
}
