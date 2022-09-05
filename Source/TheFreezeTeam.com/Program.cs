namespace TheFreezeTeam.Com;

internal class Program
{
  public static async Task<int> Main(string[] args)
  {
    Bootstrapper bootstrapper =
      Bootstrapper
      .Factory
      .InitStatiq(args);

    return await bootstrapper.RunAsync().ConfigureAwait(false);
  }
}
