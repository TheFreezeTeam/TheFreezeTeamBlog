namespace MyFirstStaticQ
{
  using TheFreezeTeamBlog.ShortCodes;
  using Statiq.Web;
  using System.Threading.Tasks;
  using Statiq.App;
  using Statiq.Core;
  using Statiq.Web.Pipelines;
  using Statiq.Common;
  using TheFreezeTeamBlog.Authors;
  using TheFreezeTeamBlog.ModuleDelegate;
  using TheFreezeTeamBlog;

  class Program
  {
    public static async Task<int> Main(string[] aArgumentArray)
    {
      var authorManager = new AuthorManager();
      return await Bootstrapper
        .Factory
        .CreateWeb(aArgumentArray)
        .AddShortcode<GitShortCode>()
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorImage,      Config.FromDocument((doc, ctx) => authorManager.AddImagePath(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorFacebook,   Config.FromDocument((doc, ctx) => authorManager.AddAuthorFacebook(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorCodingGame, Config.FromDocument((doc, ctx) => authorManager.AddAuthorCodingGame(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorTwitter,    Config.FromDocument((doc, ctx) => authorManager.AddAuthorTwitter(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorBio,        Config.FromDocument((doc, ctx) => authorManager.AddAuthorBio(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorLinkedIn,   Config.FromDocument((doc, ctx) => authorManager.AddAuthorLinkedIn(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorDiscord,    Config.FromDocument((doc, ctx) => authorManager.AddAuthorDiscord(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.ReadTime,         Config.FromDocument((doc, ctx) => ReadTimeCalculator.CalculateReadingTime(doc)))))
        .RunAsync();
    }
  }
}
