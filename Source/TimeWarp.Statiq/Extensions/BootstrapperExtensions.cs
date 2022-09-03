namespace TheFreezeTeamBlog.extensions;

public static class BootstrapperExtensions(this Bootstrapper bootstrapper)
{
  private static TBootstrapper AddPipelines<TBootstrapper>(this TBootstrapper bootstrapper)
    where TBootstrapper : IBootstrapper
  {
    bootstrapper
      .AddPipeline<Archives>()
      .AddPipeline<Assets>()
      .AddPipeline<Content>()
      .AddPipeline<DirectoryMetadata>()
      .AddPipeline<Feeds>()
      .AddPipeline<Inputs>()
      .AddPipeline<Redirects>()
      .AddPipeline<SearchIndex>()
      .AddPipeline<Sitemap>()

    return bootstrapper;
  }

  private static TBootstrapper RemovePipelines<TBootstrapper>(this TBootstrapper bootstrapper)
    where TBootstrapper : IBootstrapper
  {
    bootstrapper
      .ConfigureEngine
      (
        engine =>
        {
          engine.Pipelines.Remove(nameof(Inputs));
          engine.Pipelines.Remove(nameof(Assets));
          engine.Pipelines.Remove(nameof(Content));
          engine.Pipelines.Remove(nameof(Sitemap));
          engine.Pipelines.Remove(nameof(Archives));
          engine.Pipelines.Remove(nameof(Feeds));
          engine.Pipelines.Remove(nameof(Data));
          engine.Pipelines.Remove(nameof(Redirects));
          engine.Pipelines.Remove(nameof(SearchIndex));
          engine.Pipelines.Remove(nameof(AnalyzeContent));
        }
      );

    return bootstrapper;
  }
}
