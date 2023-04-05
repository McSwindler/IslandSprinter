using Dalamud.Plugin;
using Dalamud.Game.ClientState;

namespace IslandSprinter
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {

        public string Name => "Island Sprinter";

        private readonly Sprinter sprinter;

        public Plugin(DalamudPluginInterface pluginInterface, ClientState clientState) {
            sprinter = new Sprinter(clientState);
            sprinter.Initialize();
        }

        public void Dispose()
        {
            sprinter.Dispose();
        }
    }
}
