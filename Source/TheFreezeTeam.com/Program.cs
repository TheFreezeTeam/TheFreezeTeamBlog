namespace TheFreezeTeam.Com;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;
using TimeWarp.Statiq.ShortCodes;

internal class Program
{
  public static async Task<int> Main(string[] aArgumentArray) =>
    await Bootstrapper
      .Factory
      .CreateWeb(aArgumentArray)
      .AddReadingTimeMeta()
      .AddShortcode<GitShortCode>()
      .RunAsync();
}
