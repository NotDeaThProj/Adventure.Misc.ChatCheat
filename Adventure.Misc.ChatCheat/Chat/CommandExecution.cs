using System.Collections.Generic;
using System.Collections.Concurrent;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat
{
    public struct ChatMessage
    {
        // Properties
        public string Sender { get; }
        public string CommandText { get; }
        public string Service { get; }
        public List<string> Arguments { get; }

        // Constructors
        public ChatMessage(string sender, string commandText, List<string> commandArguments, string service)
        {
            Sender = sender;
            CommandText = commandText;
            Service = service;
            Arguments = commandArguments;

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
                    currentCommand.Function(currentMessage.CommandText, currentMessage.Arguments, currentMessage.Sender);
                }
            }

            return new OnFrameHook().OriginalFunction;
        }
    }
}
