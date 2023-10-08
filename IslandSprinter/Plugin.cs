using Dalamud.Plugin;

namespace IslandSprinter
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {

        public string Name => "Island Sprinter";

        private readonly Sprinter sprinter;

        public Plugin(DalamudPluginInterface pluginInterface) {
            Services.Initialize(pluginInterface);

            sprinter = new Sprinter();
            sprinter.Initialize();
        }

        public void Dispose()
        {
            sprinter.Dispose();
        }
    }
}
