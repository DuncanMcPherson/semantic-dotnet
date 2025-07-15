using System;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using JetBrains.Annotations;
using SemanticRelease.Abstractions;

namespace SemanticRelease.DotNet
{
    [UsedImplicitly]
    public class DotnetPlugin : ISemanticPlugin
    {
        public void Register(SemanticLifecycle lifecycle)
        {
            lifecycle.OnPrepare(RunPlugin);
        }

        private static async Task RunPlugin(ReleaseContext context)
        {
            Console.WriteLine("Beginning step 'prepare' for 'DotNet'...");
            var config = context.Config.PluginConfigs?["SemanticRelease.DotNet"] as DotnetPluginConfig ?? new DotnetPluginConfig
            {
                Configuration = "Release"
            };

            if (config.Restore)
            {
                await RunDotnetCommand(config, "restore");
            }

            if (config.Build)
            {
                await RunDotnetCommand(config, "build");
            }

            if (config.Pack)
            {
                await RunDotnetCommand(config, "pack");
            }

            if (config.Publish)
            {
                await RunDotnetCommand(config, "publish");
            }
        }

        private static async Task RunDotnetCommand(DotnetPluginConfig config, string command)
        {
            Console.WriteLine($"Beginning {command}...");
            var cliArgs = $"{command}";

            if (!config.SlnOrProject.IsNullOrEmpty())
                cliArgs += $" {config.SlnOrProject}";
            if (command != "restore" && config.UseSkipFlags)
                cliArgs += $" --no-{(command == "build" ? "restore" : "build")}";
            if (command == "pack" && !config.OutputDir.IsNullOrEmpty())
                cliArgs += $" -o \"{config.OutputDir}\"";
            cliArgs += $" -c {config.Configuration}";
                
            Console.WriteLine($"{(command == "restore" ? "Restor".ToUpper() : command.ToUpper())}ing with: '{cliArgs}'");
                
            var result = await Cli.Wrap("dotnet")
                .WithArguments(cliArgs)
                .ExecuteBufferedAsync();
                
            Console.WriteLine(result.StandardOutput);
                

            if (result.ExitCode != 0)
            {
                await Console.Error.WriteLineAsync(result.StandardError);
                throw new Exception(result.StandardError);
            }
            Console.WriteLine($"{command.ToUpper()} succeeded");
        }
    }
}