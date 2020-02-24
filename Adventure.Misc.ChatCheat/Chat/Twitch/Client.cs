using System;
using System.Drawing;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using static Adventure.Misc.ChatCheat.ReloadedII.Chat.ChatMessage;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch
{
    public class Client
    {
        private static readonly ConnectionCredentials _credentials
            = new ConnectionCredentials(Program.Configuration.TwitchBotUsername, Program.Configuration.TwitchBotToken);
        public static readonly TwitchClient TwitchClient = new TwitchClient();

        public void Connect()
        {
            if (!TwitchClient.IsConnected)
            {
                Program.Logger.WriteLine($"Connecting to {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);

                TwitchClient.Initialize(_credentials, Program.Configuration.TwitchChannelName, Program.Configuration.TwitchPrefix, Program.Configuration.TwitchPrefix);

                TwitchClient.OnLog += Client_OnLog;
                TwitchClient.OnConnectionError += Client_OnConnectionError;
                TwitchClient.OnConnected += Client_OnConnected;
                TwitchClient.OnChatCommandReceived += Client_OnChatCommandReceived;

                try
                {
                    TwitchClient.Connect();
                }
                catch (AggregateException)
                {
                    Program.Logger.WriteLine("Couldn't connect to Twitch servers!", Color.Red);
                }
            }
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            new ChatMessage(e.Command.ChatMessage.DisplayName,
                e.Command.CommandText, e.Command.ArgumentsAsList,
                StreamingService.Twitch, e.Command.ChatMessage.IsBroadcaster, e.Command.ChatMessage.IsModerator);
        }

        public void Disconnect()
        {
            if (TwitchClient.IsConnected)
            {
                Program.Logger.WriteLine($"Disconnecting from {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);
                TwitchClient.OnDisconnected += Client_OnDisconnected;

                try
                {
                    TwitchClient.Disconnect();
                }
                catch (AggregateException)
                {
                    Program.Logger.WriteLine("Couldn't disconnect from Twitch servers!", Color.Red);
                }
            }
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            Program.Logger.WriteLine($"Succesfully disconnected from {Program.Configuration.TwitchChannelName}!", Color.Lime);
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Program.Logger.WriteLine($"Succesfully connected to {Program.Configuration.TwitchChannelName}!", Color.Lime);
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Program.Logger.WriteLine($"Error: {e.Error}", Color.Red);
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            //Program.Logger.WriteLine(e.Data, Color.Yellow);
        }
    }
}
