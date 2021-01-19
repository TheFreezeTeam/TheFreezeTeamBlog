using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using TheFreezeTeamBlog.ShortCodes;

namespace MyFirstStaticQ
{
    class Program
    {
        public static async Task<int> Main(string [] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .AddShortcode<GitShortCode>()
                .RunAsync();

    }
}
