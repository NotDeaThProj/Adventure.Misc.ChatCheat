using System;
using System.Drawing;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Common;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects;
using static Adventure.SDK.Library.Classes.Native.UI;
using static Adventure.SDK.Library.Classes.Native.World;
using static Adventure.SDK.Library.Classes.Native.Player;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.ChatMessage;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch.Client;
using System.Numerics;
using Adventure.SDK.Library.API.Game;
using Adventure.SDK.Library.Definitions.Enums.Objects;
using Adventure.SDK.Library.API.Audio;
using Adventure.SDK.Library.API.Objects.Main;
using Adventure.SDK.Library.Definitions.Structures.SETData;
using Adventure.SDK.Library.Definitions.Structures.GameObject;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat
{
    public struct Command
    {
        // Command to execute
        public Action<ChatMessage> Function;

        // Cooldown is in seconds
        public int Cooldown;

        // Last Time Command was activated;
        public DateTime LastActivated;
    }

    public class Controller
    {
        private static readonly DateTime _defaultTime = new DateTime(1970, 1, 1, 0, 0, 0);
        public static readonly Dictionary<string, Command> CommandDictionary = new Dictionary<string, Command>()
        {
            { Program.Configuration.SwapSonic.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapSuper.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToSuper),
                    Cooldown = Program.Configuration.SwapSuper.Cooldown,
                    LastActivated =_defaultTime
                }
            },
            { Program.Configuration.SwapMetalSonic.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapMetalSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapEggman.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapEggman.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapTails.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapTails.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapKnuckles.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapKnuckles.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapTikal.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapTikal.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapAmy.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapAmy.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapBig.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapBig.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapGamma.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToCharacter),
                    Cooldown = Program.Configuration.SwapGamma.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ActionKill.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SendDamage),
                    Cooldown = Program.Configuration.ActionKill.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ActionDamage.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SendDamage),
                    Cooldown = Program.Configuration.ActionDamage.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateCart.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SpawnCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateSnowboard.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SpawnSnowboard),
                    Cooldown = Program.Configuration.CreateSnowboard.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetLowGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravitationalForce),
                    Cooldown = Program.Configuration.SetLowGravity.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetHighGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravitationalForce),
                    Cooldown = Program.Configuration.SetHighGravity.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetNormalGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravitationalForce),
                    Cooldown = Program.Configuration.SetNormalGravity.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ResetAct.Name, new Command()
                {
                    Function = new Action<ChatMessage>(RestartLevelAct),
                    Cooldown = Program.Configuration.ResetAct.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.Teleport.Name, new Command()
                {
                    Function = new Action<ChatMessage>(TeleportRandom),
                    Cooldown = Program.Configuration.Teleport.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.GiveItem.Name, new Command()
                {
                    Function = new Action<ChatMessage>(GiveItem),
                    Cooldown = Program.Configuration.GiveItem.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ResetLives.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ResetLifeCounter),
                    Cooldown = Program.Configuration.ResetLives.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ResetRings.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ResetRingCounter),
                    Cooldown = Program.Configuration.ResetRings.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.VoiceJapanese.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeLanguage),
                    Cooldown = Program.Configuration.VoiceJapanese.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.VoiceEnglish.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeLanguage),
                    Cooldown = Program.Configuration.VoiceEnglish.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetTextLanguage.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeLanguage),
                    Cooldown = Program.Configuration.SetTextLanguage.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetTimeOfDay.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeTimeOfDay),
                    Cooldown = Program.Configuration.SetTimeOfDay.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.PlayOhNo.Name, new Command()
                {
                    Function = new Action<ChatMessage>(PlayAudioClip),
                    Cooldown = Program.Configuration.PlayOhNo.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.RandomVoice.Name, new Command()
                {
                    Function = new Action<ChatMessage>(PlayAudioClip),
                    Cooldown = Program.Configuration.RandomVoice.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ResetShield.Name, new Command()
                {
                    Function = new Action<ChatMessage>(RemoveShield),
                    Cooldown = Program.Configuration.ResetShield.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ChangeSizeUp.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeCharacterSize),
                    Cooldown = Program.Configuration.ChangeSizeUp.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.ChangeSizeDown.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeCharacterSize),
                    Cooldown = Program.Configuration.ChangeSizeDown.Cooldown,
                    LastActivated = _defaultTime
                }
            },
#if DEBUG
            {
                "test", new Command()
                {
                    Function = new Action<ChatMessage>(TestCommand),
                    Cooldown = 0,
                    LastActivated = _defaultTime
                }
            },
            {
                "rungc", new Command()
                {
                    Function = new Action<ChatMessage>(RunGarbageCollector),
                    Cooldown = 0,
                    LastActivated = _defaultTime
                }
            }
#endif
        };

#if DEBUG
        public unsafe static void TestCommand(ChatMessage chatMessage)
        {
            _currentPlayer.Rescale(2f);
        }
        public unsafe static void RunGarbageCollector(ChatMessage chatMessage)
        {
            GC.Collect();
        }
#endif

        private static GameHandler _gameHandler = new GameHandler();
        private static AudioManager _audioManager = new AudioManager();
        private static Player _currentPlayer = new Player();

        public static void SwapToCharacter(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        string replyMessage = $"{chatMessage.Sender} has turned the player into ";
                        if (chatMessage.CommandText.Equals(Program.Configuration.SwapSonic.Name))
                        {
                            _currentPlayer.Swap(Character.Sonic);
                            replyMessage += "Sonic.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapMetalSonic.Name))
                        {
                            _currentPlayer.Swap(Character.Sonic, true);
                            replyMessage += "Metal Sonic.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapEggman.Name))
                        {
                            _currentPlayer.Swap(Character.Eggman);
                            replyMessage += "Eggman.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapTails.Name))
                        {
                            _currentPlayer.Swap(Character.Tails);
                            replyMessage += "Tails.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapKnuckles.Name))
                        {
                            _currentPlayer.Swap(Character.Knuckles);
                            replyMessage += "Knuckles.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapTikal.Name))
                        {
                            _currentPlayer.Swap(Character.Tikal);
                            replyMessage += "Tikal.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapAmy.Name))
                        {
                            _currentPlayer.Swap(Character.Amy);
                            replyMessage += "Amy.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapBig.Name))
                        {
                            _currentPlayer.Swap(Character.Big);
                            replyMessage += "Big.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SwapGamma.Name))
                        {
                            _currentPlayer.Swap(Character.Gamma);
                            replyMessage += "Gamma.";
                        }

                        LogCommand(chatMessage);
                        BotReply(replyMessage, chatMessage.Service);
                    }
                    break;
            }
        }
        public static void SwapToSuper(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        new SuperStateManager() { IsPlayerSuper = true };
                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has turned the player Super.", chatMessage.Service);
                    }
                    break;
            }
        }
        public unsafe static void SendDamage(ChatMessage chatMessage)
        {
            Info tornadoInfo = **(Info**)0x3B42E10;

            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal)
                    {
                        string replyMessage = $"{chatMessage.Sender} has ";
                        if (chatMessage.CommandText.Equals(Program.Configuration.ActionKill.Name))
                        {
                            if (_gameHandler.CurrentStage == Stage.SkyChaseOne || _gameHandler.CurrentStage == Stage.SkyChaseTwo)
                            {
                                _gameHandler.TornadoHealth = 0.5f;
                                tornadoInfo.Status |= Status.Hurt;
                            }
                            else
                            {
                                HurtPlayer();
                                HurtPlayer();
                            }
                            replyMessage += "killed the player.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.ActionDamage.Name))
                        {
                            if (_gameHandler.CurrentStage == Stage.SkyChaseOne || _gameHandler.CurrentStage == Stage.SkyChaseTwo)
                                tornadoInfo.Status |= Status.Hurt;
                            else
                                HurtPlayer();
                            replyMessage += "damaged the player.";
                        }
                        LogCommand(chatMessage);
                        BotReply(replyMessage, chatMessage.Service);
                    }
                    break;
            }
        }
        public static void SpawnCart(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        CartColor color;
                        if (chatMessage.Arguments.Count != 0)
                            Enum.TryParse(chatMessage.Arguments[0], true, out color);
                        else
                            color = (CartColor)new Random().Next(Enum.GetNames(typeof(CartColor)).Length);

                        new Cart(color);
                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has spawned a cart.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void SpawnSnowboard(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        new Snowboard();

                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has spawned a snowboard.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void ChangeGravitationalForce(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal)
                    {
                        string replyMessage = $"{chatMessage.Sender} has changed to ";
                        if (chatMessage.CommandText.Equals(Program.Configuration.SetLowGravity.Name))
                        {
                            _currentPlayer.Gravity = new Vector3(0f, -0.75f, 0f);
                            replyMessage += "low gravity.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SetNormalGravity.Name))
                        {
                            _currentPlayer.Gravity = new Vector3(0f, -1f, 0f);
                            replyMessage += "normal gravity.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.SetHighGravity.Name))
                        {
                            _currentPlayer.Gravity = new Vector3(0f, -1.25f, 0f);
                            replyMessage += "high gravity.";
                        }

                        LogCommand(chatMessage);
                        BotReply(replyMessage, chatMessage.Service);
                    }
                    break;
            }
        }
        public static void RestartLevelAct(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal)
                    {
                        _currentPlayer.Lives++;
                        _gameHandler.GameState = GameState.RestartLevelAct;

                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has restarted the current level act.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void TeleportRandom(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal)
                    {
                        // Get random object to teleport to
                        int objectID = new Random().Next(_gameHandler.CurrentLevelObjectCount);

                        // Move player to selected object
                        _currentPlayer.Position = _gameHandler.SETData[objectID].Position;

                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has teleported away the player.", chatMessage.Service);
                    }
                    break;
            }
        }
        public unsafe static void GiveItem(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal)
                    {
                        string replyMessage = $"{chatMessage.Sender} has given ";

                        // Parse chat message arguments into an enum
                        ItemBoxItem item;
                        if (chatMessage.Arguments.Count > 0)
                            Enum.TryParse(chatMessage.Arguments[0], true, out item);
                        else
                            item = (ItemBoxItem)new Random().Next(Enum.GetNames(typeof(ItemBoxItem)).Length);

                        // Create HUD Display of the Item Box Item
                        CreateItemDisplayHUD(item);

                        // Create Chat Reply
                        switch (item)
                        {
                            case ItemBoxItem.SpeedShoes:
                                // Rewrite of the Speed Shoes powerup code
                                if (_currentPlayer.CharacterID == Character.Gamma)
                                    _audioManager.Sound = 1307;
                                _audioManager.Sound = 11;
                                GiveSpeedUp(Players.P1);

                                replyMessage += "a speed up!";
                                break;
                            case ItemBoxItem.Invincibility:
                                replyMessage += "invincibility!";
                                break;
                            case ItemBoxItem.FiveRings:
                                replyMessage += "5 rings!";
                                break;
                            case ItemBoxItem.TenRings:
                                replyMessage += "10 rings!";
                                break;
                            case ItemBoxItem.RandomRings:
                                replyMessage += "a random amount of rings!";
                                break;
                            case ItemBoxItem.Shield:
                                replyMessage += "a shield!";
                                break;
                            case ItemBoxItem.ExtraLife:
                                replyMessage += "a one-up!";
                                break;
                            case ItemBoxItem.Bomb:
                                replyMessage += "a bomb!";
                                break;
                            case ItemBoxItem.MagneticShield:
                                replyMessage += "an electric shield!";
                                break;
                        }

                        // Use the item box item function
                        if (item != ItemBoxItem.SpeedShoes)
                            _gameHandler.ItemBoxItemFunctionArray[(int)item].Function((IntPtr)_currentPlayer.Info);

                        // Log and Reply
                        LogCommand(chatMessage);
                        BotReply(replyMessage, chatMessage.Service);
                    }
                    break;
            }
        }
        public static void ResetLifeCounter(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        _currentPlayer.Lives = 4;

                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has reset the life counter.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void ResetRingCounter(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal &&
                        _gameHandler.CurrentStage != Stage.SkyChaseOne && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        _currentPlayer.Rings = 0;
                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has reset the ring counter.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void ChangeLanguage(ChatMessage chatMessage)
        {
            string replyMessage = $"{chatMessage.Sender} has changed the ";

            if (chatMessage.CommandText.Equals(Program.Configuration.SetTextLanguage.Name))
            {
                // Parse chat message arguments into an enum
                Language language;
                if (chatMessage.Arguments.Count > 0)
                    Enum.TryParse(chatMessage.Arguments[0], true, out language);
                else
                    language = (Language)new Random().Next(Enum.GetNames(typeof(Language)).Length);

                // Set language of text in-game
                _gameHandler.TextLanguage = language;

                replyMessage += $"text language to {language}.";
            }
            else if(chatMessage.CommandText.Equals(Program.Configuration.VoiceJapanese.Name))
            {
                // Set language of spoken language in-game
                _gameHandler.VoiceLanguage = Language.Japanese;

                replyMessage += $"text language to {Language.Japanese}.";
            }
            else if(chatMessage.CommandText.Equals(Program.Configuration.VoiceEnglish.Name))
            {
                // Set language of spoken language in-game
                _gameHandler.VoiceLanguage = Language.English;

                replyMessage += $"text language to {Language.English}.";
            }

            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);
        }
        public static void ChangeTimeOfDay(ChatMessage chatMessage)
        {
            string replyMessage = $"{chatMessage.Sender} has changed the time of day to  ";

            TimeOfDay time;
            if (chatMessage.Arguments.Count > 0)
                Enum.TryParse(chatMessage.Arguments[0], true, out time);
            else
                time = (TimeOfDay)new Random().Next(Enum.GetNames(typeof(TimeOfDay)).Length);

            switch (time)
            {
                case TimeOfDay.Day:
                    replyMessage += "day.";
                    break;
                case TimeOfDay.Evening:
                    replyMessage += "evening.";
                    break;
                case TimeOfDay.Night:
                    replyMessage += "night.";
                    break;
            }

            SetAdventureFieldTime(time);

            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);
        }
        public static void PlayAudioClip(ChatMessage chatMessage)
        {
            string replyMessage = $"{chatMessage.Sender} has ";
            if (chatMessage.CommandText.Equals(Program.Configuration.PlayOhNo.Name))
            {
                _audioManager.Voice = 164;
                replyMessage += "made Knuckles say the funny line.";
            }

            if (chatMessage.CommandText.Equals(Program.Configuration.RandomVoice.Name))
            {
                _audioManager.Voice = new Random().Next(2043);
                replyMessage += "played a random voice line.";
            }

            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);
        }
        public unsafe static void RemoveShield(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        _currentPlayer.CharacterData->Powerups = ~(Powerup.ElectricShield | Powerup.Shield);
                        LogCommand(chatMessage);
                        BotReply($"{chatMessage.Sender} has removed the shield from the player.", chatMessage.Service);
                    }
                    break;
            }
        }
        public static void ChangeCharacterSize(ChatMessage chatMessage)
        {
            switch (_gameHandler.GameMode)
            {
                case GameMode.Stage:
                case GameMode.Field:
                case GameMode.Trial:
                case GameMode.Mission:
                    if (_gameHandler.GameState == GameState.Normal &&
                        _gameHandler.CurrentStage != Stage.SkyChaseOne && _gameHandler.CurrentStage != Stage.SkyChaseTwo)
                    {
                        string replyMessage = $"{chatMessage.Sender} has ";

                        if (chatMessage.CommandText.Equals(Program.Configuration.ChangeSizeUp.Name))
                        {
                            _currentPlayer.Rescale(2f);
                            replyMessage += "shrank the character.";
                        }
                        else if (chatMessage.CommandText.Equals(Program.Configuration.ChangeSizeDown.Name))
                        {
                            _currentPlayer.Rescale(0.5f);
                            replyMessage += "increased the character's size.";
                        }

                        LogCommand(chatMessage);
                        BotReply(replyMessage, chatMessage.Service);
                    }
                    break;
            }
        }

        public static void LogCommand(ChatMessage chatMessage)
        {
            Program.Logger.Write(chatMessage.Sender, Color.Red);
            
            Program.Logger.Write(" has activated the ", Color.White);
            
            Program.Logger.Write($"{chatMessage.CommandText} ", Color.Yellow);
            if (chatMessage.Arguments.Count > 0)
                Program.Logger.Write($"{string.Join(" ", chatMessage.Arguments)} ", Color.Yellow);
            
            Program.Logger.WriteLine("code!", Color.White);
        }

        public static void BotReply(string message, StreamingService service)
        {
            switch (service)
            {
                case StreamingService.Twitch:
                    if (Program.Configuration.TwitchReply)
                        TwitchClient.SendMessage(Program.Configuration.TwitchChannelName, message);
                    break;
                default:
                    break;
            }
        }
    }
}