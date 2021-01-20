namespace MyFirstStaticQ
{
  using TheFreezeTeamBlog.ShortCodes;
  using Statiq.Web;
  using System.Threading.Tasks;
  using Statiq.App;

    class Program
    {
    public static async Task<int> Main(string[] aArgumentArray) =>
      await Bootstrapper
      .Factory
      .CreateWeb(aArgumentArray)
	    .AddShortcode<GitShortCode>()
      .RunAsync();

    }

}
