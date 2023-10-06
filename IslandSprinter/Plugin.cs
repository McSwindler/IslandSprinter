using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace IslandSprinter
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {

        public string Name => "Island Sprinter";

        private readonly Sprinter sprinter;

        public Plugin(DalamudPluginInterface pluginInterface, IClientState clientState, IGameInteropProvider gameInteropProvider) {
            sprinter = new Sprinter(clientState, gameInteropProvider);
            sprinter.Initialize();
        }

        public void Dispose()
        {
            sprinter.Dispose();
        }
    }
}
