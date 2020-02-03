using System;
using System.Drawing;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.ChatMessage;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch.Client;
using static Adventure.SDK.Library.API.Objects.StageObjects.TwinklePark.Cart;
using Adventure.SDK.Library.API.Objects.Common;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks;
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
                    Function = new Action<ChatMessage>(SwapToSonic),
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
                    Function = new Action<ChatMessage>(SwapToMetalSonic),
                    Cooldown = Program.Configuration.SwapMetalSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapEggman.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToEggman),
                    Cooldown = Program.Configuration.SwapEggman.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapTails.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToTails),
                    Cooldown = Program.Configuration.SwapTails.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapKnuckles.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToKnuckles),
                    Cooldown = Program.Configuration.SwapKnuckles.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapTikal.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToTikal),
                    Cooldown = Program.Configuration.SwapTikal.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapAmy.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToAmy),
                    Cooldown = Program.Configuration.SwapAmy.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapBig.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToBig),
                    Cooldown = Program.Configuration.SwapBig.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapGamma.Name, new Command()
                {
                    Function = new Action<ChatMessage>(SwapToGamma),
                    Cooldown = Program.Configuration.SwapGamma.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateCart.Name, new Command()
                {
                    Function = new Action<ChatMessage>(CreateNewCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime,
                }
            }
        };

        public unsafe static void SwapToSonic(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Sonic, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Sonic.", chatMessage.Service);
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
        public unsafe static void SwapToMetalSonic(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Sonic, Players.P1, true);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Metal Sonic.", chatMessage.Service);
        }
        public unsafe static void SwapToEggman(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Eggman, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Eggman.", chatMessage.Service);
        }
        public unsafe static void SwapToTails(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Tails, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Tails.", chatMessage.Service);
        }
        public unsafe static void SwapToKnuckles(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Knuckles, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Knuckles.", chatMessage.Service);
        }
        public unsafe static void SwapToTikal(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Tikal, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Tikal.", chatMessage.Service);
        }
        public unsafe static void SwapToAmy(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Amy, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Amy.", chatMessage.Service);
        }
        public unsafe static void SwapToBig(ChatMessage chatMessage)
        {
            new SwapCharacter(Character.Big, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Big.", chatMessage.Service);
        }
        public unsafe static void SwapToGamma(ChatMessage chatMessage)
        {
            // TODO - FIX CRASH
            new SwapCharacter(Character.Gamma, Players.P1);
            LogCommand(chatMessage);
            BotReply($"{chatMessage.Sender} has turned the player into Gamma.", chatMessage.Service);
        }
        public unsafe static void CreateSnowboard(ChatMessage chatMessage)
        {
            new Snowboard();
        }
        public unsafe static void CreateNewCart(ChatMessage chatMessage)
        {
            if (chatMessage.Arguments != null)
            {
                Enum.TryParse(chatMessage.Arguments[0], true, out CartColor color);
                new SpawnCart(color);
                LogCommand(chatMessage);
                BotReply($"{chatMessage.Sender} has spawned a cart.", chatMessage.Service);
            }
            else
                new SpawnCart((CartColor)new Random().Next(Enum.GetNames(typeof(CartColor)).Length));
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
