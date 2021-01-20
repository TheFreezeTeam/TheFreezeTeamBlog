using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using TheFreezeTeamBlog.ShortCodes;

namespace MyFirstStaticQ
{
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
using Statiq.Web;
using System.Threading.Tasks;
}
