using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;

public class Services
{
        public static void Initialize(DalamudPluginInterface pluginInterface)
            => pluginInterface.Create<Services>();

        // @formatter:off
        [PluginService][RequiredVersion("1.0")] public static IClientState ClientState { get; private set; } = null!;
        [PluginService][RequiredVersion("1.0")] public static IGameInteropProvider GameInteropProvider { get; private set; } = null!;
        [PluginService][RequiredVersion("1.0")] public static IPluginLog PluginLog { get; private set; } = null!;
        // @formatter:on

}
