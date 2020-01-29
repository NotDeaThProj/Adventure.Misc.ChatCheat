using Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat
{
    public struct ChatMessage
    {
        // Properties
        public string Sender { get; }
        public Message Command { get; }
        public string Service { get; }

        // Constructors
        public ChatMessage(string sender, string[] message, string service)
        {
            Sender = sender;
            Command = new Message(message[0], message[1]);
            Service = service;

            CommandExecution.SetCommand = this;
        }

        public struct Message
        {
            public string Name { get; }
            
            public string Argument { get; }

            public Message(string name, string argument)
            {
                Name = name;
                Argument = argument;
            }
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
                if (Controller.CommandDictionary.ContainsKey(currentMessage.Command.Name))
                {
                    Command currentCommand = Controller.CommandDictionary[currentMessage.Command.Name];
                    currentCommand.Function(currentMessage.Command, currentMessage.Sender);
                }
            }

            return new OnFrameHook().OriginalFunction;
        }
    }
}
