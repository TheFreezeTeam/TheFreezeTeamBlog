using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace MyFirstStaticQ
{
    class Program
    {
        public static async Task<int> Main(string [] args) => 
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .RunAsync();
    
    }
}
