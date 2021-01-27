namespace MyFirstStaticQ
{
  using TheFreezeTeamBlog.ShortCodes;
  using Statiq.Web;
  using System.Threading.Tasks;
  using Statiq.App;
  using Statiq.Core;
  using Statiq.Web.Pipelines;
  using Statiq.Common;
  using System.Collections.Generic;
  using TheFreezeTeamBlog.Authors;

  class Program
  {
    public static async Task<int> Main(string[] aArgumentArray)
    {
      var authorManager = new AuthorManager();
      return await Bootstrapper
        .Factory
        .CreateWeb(aArgumentArray)
        .AddShortcode<GitShortCode>()
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorImage", Config.FromDocument((doc, ctx) => authorManager.AddImagePath(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorFacebook", Config.FromDocument((doc, ctx) => authorManager.AddAuthorFacebook(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorCodingGame", Config.FromDocument((doc, ctx) => authorManager.AddAuthorCodingGame(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorTwitter", Config.FromDocument((doc, ctx) => authorManager.AddAuthorTwitter(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorBio", Config.FromDocument((doc, ctx) => authorManager.AddAuthorBio(doc)))))
        .ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata("AuthorLinkedIn", Config.FromDocument((doc, ctx) => authorManager.AddAuthorLinkedIn(doc)))))
        .RunAsync();
    }

  }
}
