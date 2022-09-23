namespace TheFreezeTeam.Com.Models;

public record Author
(
  string Name,
  string AvatarPath,
  string? Bio = null,
  string? CodinGame = null,
  string? Discord = null,
  string? Facebook = null,
  string? GitHub = null,
  string? LinkedIn = null,
  string? Twitter = null,
  string? Twitch = null,
  string? YouTube = null
)
{
  public string GetUrl()
  {
    var cleanPath = PathUtilities.CleanPath(Name);
    return $"/authors/{cleanPath}";
  }
};
