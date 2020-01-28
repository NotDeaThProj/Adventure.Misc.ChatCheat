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
        public Action<string, string> Function;

        // Cooldown is in seconds
        public int Cooldown;

        // Last Time Command was activated;
        public DateTime LastActivated;
    }

    public class CommandController
    {
        private static readonly DateTime _defaultTime = new DateTime(1970, 1, 1, 0, 0, 0);
        public static readonly Dictionary<string, Command> CommandList = new Dictionary<string, Command>()
        {
            { Program.Configuration.SwapSonic.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToSonic),
                    Cooldown = Program.Configuration.SwapSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapSuper.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToSuper),
                    Cooldown = Program.Configuration.SwapSuper.Cooldown,
                    LastActivated =_defaultTime
                }
            },
            { Program.Configuration.SwapMetalSonic.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToMetalSonic),
                    Cooldown = Program.Configuration.SwapMetalSonic.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapEggman.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToEggman),
                    Cooldown = Program.Configuration.SwapEggman.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapTails.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToTails),
                    Cooldown = Program.Configuration.SwapTails.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapKnuckles.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToKnuckles),
                    Cooldown = Program.Configuration.SwapKnuckles.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            /*{ Program.Configuration.SwapTikal.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToTikal),
                    Cooldown = Program.Configuration.SwapTikal.Cooldown,
                    LastActivated = _defaultTime
                }
            },*/
            { Program.Configuration.SwapAmy.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToAmy),
                    Cooldown = Program.Configuration.SwapAmy.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapBig.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToBig),
                    Cooldown = Program.Configuration.SwapBig.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.SwapGamma.Name, new Command()
                {
                    Function = new Action<string, string>(SwapToGamma),
                    Cooldown = Program.Configuration.SwapGamma.Cooldown,
                    LastActivated = _defaultTime
                }
            },
            { Program.Configuration.CreateCart.Name, new Command()
                {
                    Function = new Action<string, string>(CreateNewCart),
                    Cooldown = Program.Configuration.CreateCart.Cooldown,
                    LastActivated = _defaultTime
                }
            }
        };

        public unsafe static void SwapToSonic(string command, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1);
            LogCommand(messageSender, command);
        }
        public static void SwapToSuper(string command, string messageSender)
        {
            new SuperStateManager(Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToMetalSonic(string command, string messageSender)
        {
            new SwapCharacter(Character.Sonic, Players.P1, true);
            LogCommand(messageSender, command);
        }
        /*public unsafe static void SwapToEggman(string command, string messageSender)
        {
            new SwapCharacter(Character.Eggman, Players.P1);
            LogCommand(messageSender, command);
        }*/
        public unsafe static void SwapToTails(string command, string messageSender)
        {
            new SwapCharacter(Character.Tails, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToKnuckles(string command, string messageSender)
        {
            new SwapCharacter(Character.Knuckles, Players.P1);
            LogCommand(messageSender, command);
        }
        /*public unsafe static void SwapToTikal(string command, string messageSender)
        {
            new SwapCharacter(Character.Tikal, Players.P1);
            LogCommand(messageSender, command);
        }*/
        public unsafe static void SwapToAmy(string command, string messageSender)
        {
            new SwapCharacter(Character.Amy, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToBig(string command, string messageSender)
        {
            new SwapCharacter(Character.Big, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void SwapToGamma(string command, string messageSender)
        {
            // TODO - FIX CRASH
            new SwapCharacter(Character.Gamma, Players.P1);
            LogCommand(messageSender, command);
        }
        public unsafe static void CreateNewCart(string command, string messageSender)
        {
            
        }

        public static void LogCommand(string sender, string command)
        {
            Program.Logger.Write(sender, Color.Red);
            Program.Logger.Write(" has activated the ", Color.White);
            Program.Logger.Write(string.Join(" ", command), Color.Yellow);
            Program.Logger.WriteLine(" code!", Color.White);
        }
    }
}
