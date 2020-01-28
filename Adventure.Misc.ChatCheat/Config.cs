using System.Collections.Generic;
using System.ComponentModel;
using Adventure.Misc.ChatCheat.Configuration;

namespace Adventure.Misc.ChatCheat
{
    public struct CommandConfig
    {
        [Description("Name of the command.")]
        public string Name { get; set; }

        [Description("Cooldown applied to the command, in seconds.")]
        public int Cooldown { get; set; }

        public override string ToString()
        {
            return "Chat Command";
        }
    }
    public struct MultiCommandConfig
    {
        [Description("Name of the command.")]
        public string Name { get; set; }

        [Description("Cooldown applied to the command, in seconds.")]
        public int Cooldown { get; set; }

        [Description("List of arguments that can be used with the command.")]
        public List<string> Arguments { get; set; }

        public override string ToString()
        {
            return "Chat Command with Arguments";
        }
    }
    public class Config : Configurable<Config>
    {
        private const string CharacterCommands = "Swap Character:";

        [Category("Twitch")]
        [DisplayName("Chat Prefix")]
        [Description("Prefix used by Twitch Chat")]
        public char TwitchPrefix { get; set; } = '!';
        [Category("Twitch")]
        [DisplayName("Channel Name")]
        [Description("Sets the channel the bot listens to.")]
        public string TwitchChannelName { get; set; } = "";
        [Category("Twitch")]
        [DisplayName("Bot Username")]
        [Description("The name of the bot used on the channel.")]
        public string TwitchBotUsername { get; set; } = "";
        [Category("Twitch")]
        [DisplayName("Bot Token")]
        [Description("")]
        public string TwitchBotToken { get; set; } = "";
        [Category("Twitch")]
        [DisplayName("Client ID")]
        [Description("")]
        public string TwitchClientID { get; set; } = "";

        [Category(CharacterCommands)]
        [DisplayName("Sonic")]
        [Description("Turns the player character into Sonic.")]
        public CommandConfig SwapSonic { get; set; } = new CommandConfig { Name = "gottagofast", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Super")]
        [Description("Changes the player into Super state.")]
        public CommandConfig SwapSuper { get; set; } = new CommandConfig { Name = "nowillshowyou", Cooldown = 120 };

        [Category(CharacterCommands)]
        [DisplayName("Metal Sonic")]
        [Description("Turns the player character into Metal Sonic.")]
        public CommandConfig SwapMetalSonic { get; set; } = new CommandConfig { Name = "strangeisntit", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Eggman")]
        [Description("Turns the player character into Eggman.")]
        public CommandConfig SwapEggman { get; set; } = new CommandConfig { Name = "gianttalkingegg", Cooldown = 120 };

        [Category(CharacterCommands)]
        [DisplayName("Tails")]
        [Description("Turns the player character into Tails.")]
        public CommandConfig SwapTails { get; set; } = new CommandConfig { Name = "flyhigh", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Knuckles")]
        [Description("Turns the player character into Knuckles.")]
        public CommandConfig SwapKnuckles { get; set; } = new CommandConfig { Name = "rougherthantherestofthem", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Tikal")]
        [Description("Turns the player character into Tikal.")]
        public CommandConfig SwapTikal { get; set; } = new CommandConfig { Name = "echidnaprincess", Cooldown = 120 };

        [Category(CharacterCommands)]
        [DisplayName("Amy")]
        [Description("Turns the player character into Amy.")]
        public CommandConfig SwapAmy { get; set; } = new CommandConfig { Name = "rosyrascal", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Big")]
        [Description("Turns the player character into Big.")]
        public CommandConfig SwapBig { get; set; } = new CommandConfig { Name = "froggy", Cooldown = 30 };

        [Category(CharacterCommands)]
        [DisplayName("Gamma")]
        [Description("Turns the player character into Gamma.")]
        public CommandConfig SwapGamma { get; set; } = new CommandConfig { Name = "skynet", Cooldown = 30 };

        [Category("Miscelanious")]
        [DisplayName("Spawn Cart")]
        [Description("Spawns a cart at the player's location.")]
        public MultiCommandConfig CreateCart { get; set; }
            = new MultiCommandConfig
            {
                Name = "supersonicracing",
                Cooldown = 30,
                Arguments = new List<string>()
                {
                    "black",
                    "blue",
                    "green",
                    "lightblue",
                    "orange",
                    "pink",
                    "red"
                }
            };
    }
}
