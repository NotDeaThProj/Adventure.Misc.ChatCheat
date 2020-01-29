using System;
using System.Collections.Generic;
using System.Drawing;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects;
using Adventure.SDK.Library.Definitions.Enums;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat
{
    public struct Command
    {
        // Command to execute
        public Action<ChatMessage.Message, string> Function;

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
                    Function = new Action<ChatMessage.Message, string>(SwapToSonic),
                    Cooldown = Program.Configuration.SwapSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapSuper.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToSuper),
                    Cooldown = Program.Configuration.SwapSuper.Cooldown,
                    LastActivated =_defaultTime
                }
            },
            { Program.Configuration.SwapMetalSonic.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToMetalSonic),
                    Cooldown = Program.Configuration.SwapMetalSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapEggman.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToEggman),
                    Cooldown = Program.Configuration.SwapEggman.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapTails.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToTails),
                    Cooldown = Program.Configuration.SwapTails.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapKnuckles.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToKnuckles),
                    Cooldown = Program.Configuration.SwapKnuckles.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapTikal.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToTikal),
                    Cooldown = Program.Configuration.SwapTikal.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapAmy.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToAmy),
                    Cooldown = Program.Configuration.SwapAmy.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapBig.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToBig),
                    Cooldown = Program.Configuration.SwapBig.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapGamma.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(SwapToGamma),
                    Cooldown = Program.Configuration.SwapGamma.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateCart.Name, new Command()
                {
                    Function = new Action<ChatMessage.Message, string>(CreateNewCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime,
                }
            }
        };

        public unsafe static void SwapToSonic(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1);
            LogCommand(messageSender, command);
        }
        public static void SwapToSuper(ChatMessage.Message command, string messageSender)
        {
            new SuperStateManager(Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToMetalSonic(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1, true);
            LogCommand(messageSender, command);
        }
        /*public unsafe static void SwapToEggman(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Eggman, Players.P1);
            LogCommand(messageSender, command);
        }*/
        public unsafe static void SwapToTails(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Tails, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToKnuckles(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Knuckles, Players.P1);
            LogCommand(messageSender, command);
        }
        /*public unsafe static void SwapToTikal(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Tikal, Players.P1);
            LogCommand(messageSender, command);
        }*/
        public unsafe static void SwapToAmy(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Amy, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToBig(ChatMessage.Message command, string messageSender)
        {
            new SwapCharacter(Character.Big, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToGamma(ChatMessage.Message command, string messageSender)
        {
            // TODO - FIX CRASH
            new SwapCharacter(Character.Gamma, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void CreateNewCart(ChatMessage.Message command, string messageSender)
        {
            Enum.TryParse(command.Argument, true, out SDK.Library.API.Objects.Color color);
            new SpawnCart(color);

        }

        public static void LogCommand(string sender, ChatMessage.Message command)
        {
            Program.Logger.Write(sender, Color.Red);
            Program.Logger.Write(" has activated the ", Color.White);
            Program.Logger.Write(string.Join(" ", new string[] { command.Name, command.Argument }), Color.Yellow);
            Program.Logger.WriteLine(" code!", Color.White);
        }
    }
}
