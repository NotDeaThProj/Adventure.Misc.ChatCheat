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
        public string Message { get; }
        public string Service { get; }

        // Constructors
        public ChatMessage(string sender, string message, string service)
        {
            Sender = sender;
            Message = message;
            Service = service;

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
                if (CommandController.CommandList.ContainsKey(currentMessage.Message))
                {
                    Command currentCommand = CommandController.CommandList[currentMessage.Message];
                    currentCommand.Function(currentMessage.Message, currentMessage.Sender);
                }
            }

            return new OnFrameHook().OriginalFunction;
        }
    }
}
