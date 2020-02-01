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

        public bool IsEnabled { get; set; }

        public override string ToString()
        {
            return "Chat Command";
        }
    }

    public class Config : Configurable<Config>
    {
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
        [Category("Twitch")]
        [DisplayName("Do Reply in Chat")]
        [Description("Toggles if the bot should reply to command input in chat.")]
        public bool TwitchReply { get; set; } = false;

        [Category("Commands")]
        [DisplayName("Swap Character: Sonic")]
        [Description("Turns the player character into Sonic.")]
        public CommandConfig SwapSonic { get; set; } =
            new CommandConfig { Name = "gottagofast", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Super")]
        [Description("Changes the player into Super state.")]
        public CommandConfig SwapSuper { get; set; } =
            new CommandConfig { Name = "nowillshowyou", Cooldown = 120, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Metal Sonic")]
        [Description("Turns the player character into Metal Sonic.")]
        public CommandConfig SwapMetalSonic { get; set; } =
            new CommandConfig { Name = "strangeisntit", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Eggman")]
        [Description("Turns the player character into Eggman.")]
        public CommandConfig SwapEggman { get; set; } = 
            new CommandConfig { Name = "gianttalkingegg", Cooldown = 120, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Tails")]
        [Description("Turns the player character into Tails.")]
        public CommandConfig SwapTails { get; set; } =
            new CommandConfig { Name = "flyhigh", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Knuckles")]
        [Description("Turns the player character into Knuckles.")]
        public CommandConfig SwapKnuckles { get; set; } =
            new CommandConfig { Name = "rougherthantherestofthem", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Tikal")]
        [Description("Turns the player character into Tikal.")]
        public CommandConfig SwapTikal { get; set; } =
            new CommandConfig { Name = "echidnaprincess", Cooldown = 120, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Amy")]
        [Description("Turns the player character into Amy.")]
        public CommandConfig SwapAmy { get; set; } =
            new CommandConfig { Name = "rosyrascal", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Big")]
        [Description("Turns the player character into Big.")]
        public CommandConfig SwapBig { get; set; } =
            new CommandConfig { Name = "froggy", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Swap Character: Gamma")]
        [Description("Turns the player character into Gamma.")]
        public CommandConfig SwapGamma { get; set; } =
            new CommandConfig { Name = "skynet", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Kill")]
        [Description("Kills the player.")]
        public CommandConfig ActionKill { get; set; } =
            new CommandConfig { Name = "giveup", Cooldown = 120, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Damage")]
        [Description("Damages the player.")]
        public CommandConfig ActionDamage { get; set; } =
            new CommandConfig { Name = "tearmeapart", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Spawn Cart")]
        [Description("Spawns a cart at the player's location.")]
        public CommandConfig CreateCart { get; set; } =
            new CommandConfig { Name = "supersonicracing", Cooldown = 90, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Spawn Snowboard")]
        [Description("Spawns a snowboard at the player's location. (Only if Sonic or Tails)")]
        public CommandConfig CreateSnowboard { get; set; } =
            new CommandConfig { Name = "escapefromthecity", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Low Gravity")]
        [Description("Sets low gravity for the player.")]
        public CommandConfig SetLowGravity { get; set; } =
            new CommandConfig { Name = "takemetothemoon", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("High Gravity")]
        [Description("Sets high gravity for the player.")]
        public CommandConfig SetHighGravity { get; set; } =
            new CommandConfig { Name = "stickmetotheground", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Normalize Gravity")]
        [Description("Sets gravity to the default value for the player.")]
        public CommandConfig SetNormalGravity { get; set; } =
            new CommandConfig { Name = "takemetothemoon", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Restart Level Act")]
        [Description("Restarts the act that the player is currently in.")]
        public CommandConfig ResetAct { get; set; } =
            new CommandConfig { Name = "itallstartswiththis", Cooldown = 180, IsEnabled = true };
        
        [Category("Commands")]
        [DisplayName("Random Teleport")]
        [Description("Teleports the player to a random object in the level.")]
        public CommandConfig Teleport { get; set; } =
            new CommandConfig { Name = "chaoscontrol", Cooldown = 180, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Item")]
        [Description("Gives an item to the player.")]
        public CommandConfig GiveItem { get; set; } =
            new CommandConfig { Name = "item", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Reset Life Counter")]
        [Description("Resets the life counter in the game to the default value (4).")]
        public CommandConfig ResetLives { get; set; } =
            new CommandConfig { Name = "yourlivesaremine", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Reset Ring Counter")]
        [Description("Resets the ring counter in the game to the default value (0).")]
        public CommandConfig ResetRings { get; set; } =
            new CommandConfig { Name = "emptypocket", Cooldown = 30, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Voice Language: Japanese")]
        [Description("Sets the voice language to Japanese.")]
        public CommandConfig VoiceJapanese { get; set; } =
            new CommandConfig { Name = "sorewadoukana", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Voice Language: English")]
        [Description("Sets the voice language to English.")]
        public CommandConfig VoiceEnglish { get; set; } =
            new CommandConfig { Name = "wedontspeakweaboo", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Text Language")]
        [Description("Sets the text language.")]
        public CommandConfig SetTextLanguage { get; set; } =
            new CommandConfig { Name = "idontunderstand", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Time of Day")]
        [Description("Sets the time of day in Adventure Fields.")]
        public CommandConfig SetTimeOfDay { get; set; } =
            new CommandConfig { Name = "whattimeisit", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Oh no")]
        [Description("Plays Knuckles's 'Oh No!' voice clip.")]
        public CommandConfig PlayOhNo { get; set; } =
            new CommandConfig { Name = "ohno", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Random Audio Clip")]
        [Description("Plays a random audio clip from the game.")]
        public CommandConfig RandomVoice { get; set; } =
            new CommandConfig { Name = "stoptalkingtome", Cooldown = 0, IsEnabled = true };

        [Category("Commands")]
        [DisplayName("Remove Shield")]
        [Description("Removes the shield powerup from the player.")]
        public CommandConfig ResetShield { get; set; } =
            new CommandConfig { Name = "removeprotection", Cooldown = 0, IsEnabled = true };
    }
}