namespace TheFreezeTeam.Com.Commands;

using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;
using static TheFreezeTeam.Com.Commands.SampleCommand;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statiq.Common;

[Description("This is just a sample command")]
internal class SampleCommand : EngineCommand<SampleCommandSettings>
{
  public SampleCommand
  (
    IConfiguratorCollection configurators,
    Settings settings,
    IServiceCollection serviceCollection,
    IFileSystem fileSystem,
    Bootstrapper bootstrapper
  ) : base(configurators, settings, serviceCollection, fileSystem, bootstrapper) { }

  protected override Task<int> ExecuteEngineAsync(CommandContext commandContext, SampleCommandSettings commandSettings, IEngineManager engineManager) => throw new NotImplementedException();

  internal class SampleCommandSettings : BaseCommandSettings
  {
    [CommandOption("--some-option <SOMEOPTION>")]
    [Description("Pause execution at the start of the program until a debugger is attached.")]
    public string? SomeOption { get; set; }
  }
}


