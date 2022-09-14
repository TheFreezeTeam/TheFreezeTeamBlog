namespace TheFreezeTeam.Com.Models;

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
) {
  public string RelativeUrl
  {
    get
    {
      var result = new string(Name.Where(c => !char.IsPunctuation(c)).ToArray());
      var x = result.Replace(" ","-").ToLower();
      return $"authors/{x}";
    }
  }
};
