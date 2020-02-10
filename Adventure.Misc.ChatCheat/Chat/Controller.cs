using System;
using System.Drawing;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Common;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects;
using static Adventure.SDK.Library.Classes.Native.Player;
using static Adventure.SDK.Library.Classes.Native.GameObject;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.ChatMessage;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch.Client;
using Adventure.SDK.Library.API.Objects.Player;
using System.Numerics;
using Adventure.SDK.Library.API.Game;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Adventure.SDK.Library.Definitions.Enums.Objects;

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
                    Function = new Action<ChatMessage>(CreateNewCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateSnowboard.Name, new Command()
                {
                    Function = new Action<ChatMessage>(CreateNewSnowboard),
                    Cooldown = Program.Configuration.CreateSnowboard.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetLowGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravity),
                    Cooldown = Program.Configuration.SetLowGravity.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetHighGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravity),
                    Cooldown = Program.Configuration.SetHighGravity.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SetNormalGravity.Name, new Command()
                {
                    Function = new Action<ChatMessage>(ChangeGravity),
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
#if DEBUG
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
        public unsafe static void RunGarbageCollector(ChatMessage chatMessage)
        {
            GC.Collect();
        }
#endif

        private static GameHandler _gameHandler = new GameHandler();
        public static Cart cart;

        public static void SwapToCharacter(ChatMessage chatMessage)
        {
            string replyMessage = $"{chatMessage.Sender} has turned the player into ";
            if (chatMessage.CommandText.Equals(Program.Configuration.SwapSonic.Name))
            {
                new SwapCharacter(Character.Sonic, Players.P1);
                replyMessage += "Sonic.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapMetalSonic.Name))
            {
                new SwapCharacter(Character.Sonic, Players.P1, true);
                replyMessage += "Metal Sonic.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapEggman.Name))
            {
                new SwapCharacter(Character.Eggman, Players.P1);
                replyMessage += "Eggman.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapTails.Name))
            {
                new SwapCharacter(Character.Tails, Players.P1);
                replyMessage += "Tails.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapKnuckles.Name))
            {
                new SwapCharacter(Character.Knuckles, Players.P1);
                replyMessage += "Knuckles.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapTikal.Name))
            {
                new SwapCharacter(Character.Tikal, Players.P1);
                replyMessage += "Tikal.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapAmy.Name))
            {
                new SwapCharacter(Character.Amy, Players.P1);
                replyMessage += "Amy.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapBig.Name))
            {
                new SwapCharacter(Character.Big, Players.P1);
                replyMessage += "Big.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SwapGamma.Name))
            {
                new SwapCharacter(Character.Gamma, Players.P1);
                replyMessage += "Gamma.";
            }

            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);
        }
        public static void SwapToSuper(ChatMessage chatMessage)
        {
            new SuperStateManager(Players.P1)
            {
                IsPlayerSuper = true
            };
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player Super.", chatMessage.Service);
        }
        public static void SendDamage(ChatMessage chatMessage)
        {
            string replyMessage = chatMessage.Sender;
            if (chatMessage.CommandText.Equals(Program.Configuration.ActionKill.Name))
            {
                HurtPlayer(Players.P1);
                HurtPlayer(Players.P1);
                replyMessage += " has killed the player.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.ActionDamage.Name))
            {
                HurtPlayer(Players.P1);
                replyMessage += " has killed the player.";
            }
            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);

        }
        public unsafe static void CreateNewCart(ChatMessage chatMessage)
        {
            CartColor color;
            if (chatMessage.Arguments.Count != 0)
                Enum.TryParse(chatMessage.Arguments[0], true, out color);
            else
                color = (CartColor)new Random().Next(Enum.GetNames(typeof(CartColor)).Length);

            cart = new Cart(color);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has spawned a cart.", chatMessage.Service);
        }
        public static void CreateNewSnowboard(ChatMessage chatMessage)
        {
            new Snowboard();

            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has spawned a snowboard.", chatMessage.Service);
        }
        public unsafe static void ChangeGravity(ChatMessage chatMessage)
        {
            Player currentPlayer = new Player();
            string replyMessage = $"{chatMessage.Sender} has changed to ";
            if (chatMessage.CommandText.Equals(Program.Configuration.SetLowGravity.Name))
            {
                currentPlayer.Gravity = new Vector3(0f, -0.75f, 0f);
                replyMessage += "low gravity.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SetNormalGravity.Name))
            {
                currentPlayer.Gravity = new Vector3(0f, -1f, 0f);
                replyMessage += "normal gravity.";
            }
            else if (chatMessage.CommandText.Equals(Program.Configuration.SetHighGravity.Name))
            {
                currentPlayer.Gravity = new Vector3(0f, -1.25f, 0f);
                replyMessage += "high gravity.";
            }

            LogCommand(chatMessage);
            BotReply(replyMessage, chatMessage.Service);
        }
        public unsafe static void RestartLevelAct(ChatMessage chatMessage)
        {
            Player currentPlayer = new Player();
            currentPlayer.Lives++;

            new GameHandler() { GameState = GameState.RestartLevelAct };
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has restarted the current level act.", chatMessage.Service);
        }
        public unsafe static void TeleportRandom(ChatMessage chatMessage)
        {
            var setData = new ReadOnlySpan<SETEntry>(_gameHandler.SETEntryArrayAddress, _gameHandler.CurrentLevelObjectCount);
            new Player() { Position = setData[new Random().Next(_gameHandler.CurrentLevelObjectCount)].Position };
        }
        public unsafe static void GiveItemToPlayer(ChatMessage chatMessage)
        {
            var itemBoxItems = new ReadOnlySpan<SDK.Library.Definitions.Structures.Object.ItemBoxItem>(_gameHandler.ItemBoxItemFunctionAddress, 9);
            ItemBoxItem item;
            if (chatMessage.Arguments != null)
                Enum.TryParse(chatMessage.Arguments[0], true, out item);
            else
                item = (ItemBoxItem)new Random().Next(Enum.GetNames(typeof(ItemBoxItem)).Length);

            // TODO
            switch (item)
            {
                case ItemBoxItem.SpeedShoes:
                    break;
                case ItemBoxItem.Invincibility:
                    break;
                case ItemBoxItem.FiveRings:
                    break;
                case ItemBoxItem.TenRings:
                    break;
                case ItemBoxItem.RandomRings:
                    break;
                case ItemBoxItem.Shield:
                    break;
                case ItemBoxItem.ExtraLife:
                    break;
                case ItemBoxItem.Bomb:
                    break;
                case ItemBoxItem.MagneticShield:
                    break;
                default:
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
