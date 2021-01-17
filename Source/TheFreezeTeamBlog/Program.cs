namespace MyFirstStaticQ
{
  using Statiq.App;
  using Statiq.Web;
  using System.Threading.Tasks;

  class Program
  {
    public static async Task<int> Main(string[] aArgumentArray) =>
      await Bootstrapper
      .Factory
      .CreateWeb(aArgumentArray)
      .RunAsync();
  }
}
