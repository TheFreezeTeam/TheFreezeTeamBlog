namespace Statiq.App;

using Statiq.TimeWarp.Commands;

public static class BootstrapperFactoryExtensions
{
  public static Bootstrapper InitStatiq(this BootstrapperFactory factory, string[] args) =>
    factory
    .CreateWeb(args)
    .AddCommand<SampleCommand>()
    .AddReadingTimeMeta();
}
