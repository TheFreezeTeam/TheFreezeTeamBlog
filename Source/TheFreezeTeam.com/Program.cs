namespace TheFreezeTeam.Com;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

internal class Program
{
  public static async Task<int> Main(string[] aArgumentArray) =>
    await Bootstrapper
      .Factory
      .CreateWeb(aArgumentArray)
      .AddReadingTimeMeta()
      .RunAsync();
}
