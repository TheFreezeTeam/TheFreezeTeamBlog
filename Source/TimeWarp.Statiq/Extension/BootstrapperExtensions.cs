namespace Statiq.App;

using Statiq.Common;
using Statiq.TimeWarp.Extension;
using Statiq.Web.Pipelines;
using System.Text;
using System.Text.Json;

public static class BootstrapperExtensions
{

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bootstrapper"></param>
  /// <returns></returns>
  /// <remarks>https://www.statiq.dev/guide/configuration/bootstrapper/adding-pipelines</remarks>
  public static Bootstrapper AddMyPipelines(this Bootstrapper bootstrapper)
  {
    bootstrapper
      //.AddPipelines();
      .AddPipeline<AnalyzeContent>()
      .AddPipeline<Archives>()
      .AddPipeline<Assets>()
      .AddPipeline<Content>()
      .AddPipeline<Data>()
      .AddPipeline<DirectoryMetadata>()
      .AddPipeline<Feeds>()
      .AddPipeline<Inputs>()
      .AddPipeline<Redirects>()
      .AddPipeline<SearchIndex>()
      .AddPipeline<Sitemap>();

    return bootstrapper;
  }

  public static Bootstrapper AddServices(this Bootstrapper bootstrapper)
  {
    bootstrapper
      .ConfigureServices
      (
        services =>
        {
          //services.AddTransient<IXyzService, XyzService>();
          //services.AddTransient<IReadingTimeService, ReadingTimeService>();
          //services.AddSingleton(new PostListOptions(document => document.GetTitle()));
        }
      );

     return bootstrapper;
  }

  public static Bootstrapper ModifyPipelines(this Bootstrapper bootstrapper)
  {
    //bootstrapper.ModifyPipeline(nameof(somepipline), (ipipeline => ipipeline.blah));
    return bootstrapper; ;
  }
  public static Bootstrapper RemovePipelines(this Bootstrapper bootstrapper)
  {
    bootstrapper
      .ConfigureEngine
      (
        engine =>
        {
          engine.Pipelines.Clear();
          //engine.Pipelines.Remove(nameof(AnalyzeContent));
          //engine.Pipelines.Remove(nameof(Archives));
          //engine.Pipelines.Remove(nameof(Assets));
          //engine.Pipelines.Remove(nameof(Content));
          //engine.Pipelines.Remove(nameof(Data));
          //engine.Pipelines.Remove(nameof(DirectoryMetadata));
          //engine.Pipelines.Remove(nameof(Feeds));
          //engine.Pipelines.Remove(nameof(Inputs));
          //engine.Pipelines.Remove(nameof(Redirects));
          //engine.Pipelines.Remove(nameof(SearchIndex));
          //engine.Pipelines.Remove(nameof(Sitemap));
        }
      );

    return bootstrapper;
  }

  public static Bootstrapper DisplayEngine(this Bootstrapper bootstrapper)
  {
    _ = bootstrapper
      .ConfigureEngine
      (
        engine =>
        {
          string json = JsonSerializer.Serialize(engine, new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true});
          Console.WriteLine(json);
          string indentation = "  ";
          var sb = new StringBuilder();
          sb.AppendLine();
          sb.AppendLine("Engine:");
          sb.AppendLine($"{indentation}ApplicationState({engine.ApplicationState})");
          sb.AppendLine($"{indentation}SerialExecution({engine.SerialExecution})");
          sb.AppendLine($"{indentation}Analyzers({engine.Analyzers.Count})");
          sb.AppendLine($"{indentation}ClassCatalog({engine.ClassCatalog.Count})");
          sb.AppendLine($"{indentation}Shortcodes({engine.Shortcodes.Count})");
          foreach (string? shortcode in engine.Shortcodes)
          {
            sb.AppendLine($"{shortcode}");
          }
          foreach (KeyValuePair<string, IPipeline> item in engine.Pipelines)
          {
            sb.AppendLine("----------");
            sb.AppendLine($"Pipeline ({item.Key}):{item.Value.Dump()}");
          }
          Console.WriteLine(sb.ToString());
        }
      );

    return bootstrapper;
  }
}
