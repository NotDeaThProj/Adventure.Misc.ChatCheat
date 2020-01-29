using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace Adventure.Misc.ChatCheat.ReloadedII.Chat.Twitch
{
    public class Client
    {
        private static readonly ConnectionCredentials _credentials
            = new ConnectionCredentials(Program.Configuration.TwitchBotUsername, Program.Configuration.TwitchBotToken);
        private static readonly TwitchClient _client = new TwitchClient();

        public void Connect()
        {
            if (!_client.IsConnected)
            {
                Program.Logger.WriteLine($"Connecting to {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);

                _client.Initialize(_credentials, Program.Configuration.TwitchChannelName, Program.Configuration.TwitchPrefix, Program.Configuration.TwitchPrefix);

                _client.OnLog += Client_OnLog;
                _client.OnConnectionError += Client_OnConnectionError;
                _client.OnConnected += Client_OnConnected;
                _client.OnChatCommandReceived += Client_OnChatCommandReceived;

                try
                {
                    _client.Connect();
                }
                catch (AggregateException)
                {
                    Program.Logger.WriteLine("Couldn't connect to Twitch servers!", Color.Red);
                }
            }
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            new ChatMessage(e.Command.ChatMessage.DisplayName, e.Command.CommandText, e.Command.ArgumentsAsList, "Twitch");
        }

        public void Disconnect()
        {
            if (_client.IsConnected)
            {
                Program.Logger.WriteLine($"Disconnecting from {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);

                try
                {
                    _client.Disconnect();
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

        /*private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith(Program.Configuration.TwitchPrefix)
            {
                string[] message = e.ChatMessage.Message[1..].Split(' ');
                ChatMessage asd = new ChatMessage(e.ChatMessage.DisplayName, message, "Twitch");
                Console.WriteLine(asd);
            }
        }*/

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
