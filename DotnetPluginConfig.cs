using System;
using SemanticRelease.Abstractions;

namespace SemanticRelease.DotNet
{

    public class DotnetPluginConfig : IPluginConfig
    {
        public bool Restore { get; set; } = true;
        public bool Build { get; set; } = true;
        public bool Pack { get; set; } = false;
        public bool Publish { get; set; } = false;
        public bool UseSkipFlags { get; set; } = true;
        public string Configuration { get; set; } = string.Empty;
        public string? OutputDir { get; set; }
        public string? SlnOrProject { get; set; }

        public void Validate()
        {
            if (Configuration.IsNullOrEmpty())
                throw new ArgumentException("Configuration must not be null.");
            if (!SlnOrProject.IsNullOrEmpty() && SlnOrProject!.Contains("*"))
                throw new ArgumentException("Solution or Project file name must not contain '*'");
        }
    }
}