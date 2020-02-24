using System;
using Adventure.Misc.ChatCheat.Configuration;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks;
using Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Adventure.Misc.ChatCheat.ReloadedII
{
    public class Program : IMod
    {
        // Twitch Variables/Constants
        private static readonly Client _twitchBot = new Client();
        private static OnFrameHook _onFrame;
        private static PlaceCharacterInCartHook _placeCharacterInCart;

        private const string ModId = "adventure.misc.chatcheat";
        private IModLoader _modLoader;
        public static ILoggerV2 Logger;
        private Configurator _configurator;
        public static Config Configuration;

        public void Start(IModLoaderV1 loader)
        {
            _modLoader = (IModLoader)loader;
            Logger = (ILogger)_modLoader.GetLogger();

            // Your config file is in Config.json.
            // Need more configurations? See `_configurator.MakeConfigurations`
            _configurator = new Configurator(_modLoader.GetDirectoryForModId(ModId));
            Configuration = _configurator.GetConfiguration<Config>(0);
            Configuration.ConfigurationUpdated += OnConfigurationUpdated;

            /* Your mod code starts here. */
            _twitchBot.Connect();
            EnableHooks();
        }
        private void OnConfigurationUpdated(IConfigurable obj)
        {
            /*
                This is executed when the configuration file gets updated by the user
                at runtime. This allows for mods to be configured in real time.

                Note: Events are also copied, you do not need to re-subscribe.
            */

            // Replace configuration with new.
            Configuration = (Config)obj;
            Logger.WriteLine($"[{ModId}] Config Updated: Applying");

            // Apply settings from configuration.
            // ... your code here.

        }

        /* Mod loader actions. */
        public void Suspend()
        {
            _onFrame.IsEnabled = false;
            _placeCharacterInCart.IsEnabled = false;
            _twitchBot.Disconnect();
        }

        public void Resume()
        {
            _onFrame.IsEnabled = true;
            _placeCharacterInCart.IsEnabled = true;
            _twitchBot.Connect();
        }

        public void Unload() { }

        /*  If CanSuspend == false, suspend and resume button are disabled in Launcher and Suspend()/Resume() will never be called.
            If CanUnload == false, unload button is disabled in Launcher and Unload() will never be called.
        */
        public bool CanUnload() => false;
        public bool CanSuspend() => true;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }

        public void EnableHooks()
        {
            _onFrame = new OnFrameHook();
            _placeCharacterInCart = new PlaceCharacterInCartHook();
        }
    }
}
