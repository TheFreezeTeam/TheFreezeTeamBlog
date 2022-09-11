namespace Statiq.App;

using Statiq.Common;
using Statiq.TimeWarp.Commands;
using Statiq.Web.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class BootstrapperFactoryExtensions
{
  public static Bootstrapper InitStatiq(this BootstrapperFactory factory, string[] args) =>
    factory
    .CreateWeb(args)
    .AddCommand<SampleCommand>();
    // .DisplayEngine();
    //.ModifyPipelines()
    //.RemovePipelines()
    //.AddMyPipelines()
    //.AddServices();
    //.AddShortcode<GitShortCode>()
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorImage, Config.FromDocument((doc, ctx) => authorManager.AddImagePath(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorFacebook, Config.FromDocument((doc, ctx) => authorManager.AddAuthorFacebook(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorCodingGame, Config.FromDocument((doc, ctx) => authorManager.AddAuthorCodingGame(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorTwitter, Config.FromDocument((doc, ctx) => authorManager.AddAuthorTwitter(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorBio, Config.FromDocument((doc, ctx) => authorManager.AddAuthorBio(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorLinkedIn, Config.FromDocument((doc, ctx) => authorManager.AddAuthorLinkedIn(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.AuthorDiscord, Config.FromDocument((doc, ctx) => authorManager.AddAuthorDiscord(doc)))))
    //.ModifyPipeline(nameof(Inputs), aX => aX.ProcessModules.Add(new SetMetadata(TftKeys.ReadTime, Config.FromDocument((doc, ctx) => ReadTimeCalculator.CalculateReadingTime(doc)))))
}
