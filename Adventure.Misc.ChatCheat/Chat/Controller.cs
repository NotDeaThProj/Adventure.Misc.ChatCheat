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
        public Action<string, List<string>, string> Function;

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
                    Function = new Action<string, List<string>, string>(SwapToSonic),
                    Cooldown = Program.Configuration.SwapSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapSuper.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToSuper),
                    Cooldown = Program.Configuration.SwapSuper.Cooldown,
                    LastActivated =_defaultTime
                }
            },
            { Program.Configuration.SwapMetalSonic.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToMetalSonic),
                    Cooldown = Program.Configuration.SwapMetalSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapEggman.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToEggman),
                    Cooldown = Program.Configuration.SwapEggman.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapTails.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToTails),
                    Cooldown = Program.Configuration.SwapTails.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapKnuckles.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToKnuckles),
                    Cooldown = Program.Configuration.SwapKnuckles.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapTikal.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToTikal),
                    Cooldown = Program.Configuration.SwapTikal.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapAmy.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToAmy),
                    Cooldown = Program.Configuration.SwapAmy.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapBig.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToBig),
                    Cooldown = Program.Configuration.SwapBig.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapGamma.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(SwapToGamma),
                    Cooldown = Program.Configuration.SwapGamma.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateCart.Name, new Command()
                {
                    Function = new Action<string, List<string>, string>(CreateNewCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime,
                }
            }
        };

        public unsafe static void SwapToSonic(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public static void SwapToSuper(string command, List<string> arguments, string messageSender)
        {
            new SuperStateManager(Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public unsafe static void SwapToMetalSonic(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1, true);
            LogCommand(messageSender, command, arguments);
        }
        /*public unsafe static void SwapToEggman(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Eggman, Players.P1);
            LogCommand(messageSender, command, arguments);
        }*/
        public unsafe static void SwapToTails(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Tails, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public unsafe static void SwapToKnuckles(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Knuckles, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        /*public unsafe static void SwapToTikal(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Tikal, Players.P1);
            LogCommand(messageSender, command, arguments);
        }*/
        public unsafe static void SwapToAmy(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Amy, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public unsafe static void SwapToBig(string command, List<string> arguments, string messageSender)
        {
            new SwapCharacter(Character.Big, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public unsafe static void SwapToGamma(string command, List<string> arguments, string messageSender)
        {
            // TODO - FIX CRASH
            new SwapCharacter(Character.Gamma, Players.P1);
            LogCommand(messageSender, command, arguments);
        }
        public unsafe static void CreateNewCart(string command, List<string> arguments, string messageSender)
        {
            if (arguments != null)
            {
                Enum.TryParse(arguments[0], true, out SDK.Library.API.Objects.Color color);
                new SpawnCart(color);
                LogCommand(messageSender, command, arguments);
            }
        }

        public static void LogCommand(string sender, string command, List<string> arguments)
        {
            Program.Logger.Write(sender, Color.Red);
            Program.Logger.Write(" has activated the ", Color.White);
            Program.Logger.Write($"{command} ", Color.Yellow);
            if (arguments.Count > 0)
                Program.Logger.Write($"{string.Join(" ", arguments)} ", Color.Yellow);
            Program.Logger.WriteLine("code!", Color.White);
        }
    }
}
