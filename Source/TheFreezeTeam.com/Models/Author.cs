namespace TheFreezeTeam.Com.Models;

public record Author
(
  string Name,
  string AvatarPath,
  string? Bio = null,
  string? CodinGame = null,
  string? Discord = null,
  string? GitHub = null,
  string? Facebook = null,
  string? LinkedIn = null,
  string? Twitter = null,
  string? Twitch = null,
  string? YouTube = null
) {
  public string Url
  {
    get
    {
      var result = new string(Name.Where(c => !char.IsPunctuation(c)).ToArray());
      var x = result.Replace(" ","-").ToLower();
      return $"/authors/{x}";
    }
  }
};
