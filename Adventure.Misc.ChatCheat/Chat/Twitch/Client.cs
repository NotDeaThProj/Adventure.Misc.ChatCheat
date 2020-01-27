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
        public bool IsOnline
        {
            get => _isOnline;
        }

        private static readonly ConnectionCredentials _credentials
            = new ConnectionCredentials(Program.Configuration.TwitchChannelName, Program.Configuration.TwitchBotToken);
        private static readonly TwitchClient _client = new TwitchClient();
        private static bool _isOnline;

        public void InitEvents()
        {
            _client.OnLog += Client_OnLog;
            _client.OnConnectionError += Client_OnConnectionError;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public async Task Connect()
        {
            if (!_client.IsConnected)
            {
                Program.Logger.WriteLine($"Connecting to {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);
                _isOnline = true;

                _client.Initialize(_credentials, Program.Configuration.TwitchChannelName);

                try
                {
                    _client.Connect();
                }
                catch (AggregateException)
                {
                    Program.Logger.WriteLine("Couldn't connect to Twitch servers!", Color.Red);
                    _isOnline = false;
                }
            }
        }

        public async Task Disconnect()
        {
            if (_client.IsConnected)
            {
                Program.Logger.WriteLine($"Disconnecting from {Program.Configuration.TwitchChannelName}...", Color.Fuchsia);
                _isOnline = false;

                try
                {
                    _client.Disconnect();
                }
                catch (AggregateException)
                {
                    Program.Logger.WriteLine("Couldn't disconnect from Twitch servers!", Color.Red);
                    _isOnline = true;
                }
            }
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            Program.Logger.WriteLine($"Succesfully disconnected from {Program.Configuration.TwitchChannelName}!", Color.Lime);
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Program.Logger.WriteLine($"Succesfully connected to {e.AutoJoinChannel}!", Color.Lime);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith(Program.Configuration.TwitchPrefix))
            {
                string message = e.ChatMessage.Message[1..];
                new ChatMessage(e.ChatMessage.DisplayName, message, "Twitch");
            }
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
