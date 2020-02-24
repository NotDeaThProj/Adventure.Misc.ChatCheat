using System.Collections.Generic;
using System.Collections.Concurrent;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.ChatMessage;
using System;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat
{
    public struct ChatMessage
    {
        public enum StreamingService
        {
            Twitch
        }

        public enum Rank : byte
        {
            Standard = 0x1,
            Moderator = 0x2,
            Broadcaster = 0x4,
        }

        // Properties
        public string Sender { get; }
        public string CommandText { get; }
        public StreamingService Service { get; }
        public List<string> Arguments { get; }
        public Rank Role { get; }

        // Constructors
        public ChatMessage(string sender, string commandText, List<string> commandArguments, StreamingService service, bool isBroadcast = false, bool isModerator = false)
        {
            Sender = sender;
            CommandText = commandText;
            Service = service;
            Arguments = commandArguments;

            Role = Rank.Standard;
            if (isModerator)
                Role |= Rank.Moderator;
            if (isBroadcast)
                Role |= Rank.Broadcaster;

            CommandExecution.SetCommand = this;
        }
    }

    public class CommandExecution
    {
        private static readonly ConcurrentQueue<ChatMessage> _commands = new ConcurrentQueue<ChatMessage>();

        public static ChatMessage SetCommand
        {
            set => _commands.Enqueue(value);
        }

        public static int ExecuteCommands()
        {
            _commands.TryDequeue(out ChatMessage currentMessage);
            if (!string.IsNullOrEmpty(currentMessage.Sender))
            {
                if (Controller.CommandDictionary.ContainsKey(currentMessage.CommandText))
                {
                    Command currentCommand = Controller.CommandDictionary[currentMessage.CommandText];

                    // No Cooldown for Broadcaster
                    if (currentMessage.Role.HasFlag(Rank.Broadcaster))
                        currentCommand.Function(currentMessage);
                    else
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime lastActivationTime = currentCommand.LastActivated.AddSeconds(currentCommand.Cooldown);
                        if (currentTime > lastActivationTime)
                        {
                            // Execute Command
                            currentCommand.Function(currentMessage);

                            // Overwrite Last Activation Time
                            currentCommand.LastActivated = DateTime.Now;
                        }
                    }

                }
            }

            return new OnFrameHook().OriginalFunction;
        }
    }
}
