using System;
using System.Collections.Generic;
using Deceit.Backend.Domain.Players;

namespace Deceit.Backend.Domain.Lobbies
{
    public class Lobby
    {
        public string LobbyId { get; }
        List<Player> players;
        public string? ForensicScientistId { get; private set; }

        public IEnumerable<Player> Players => players;

        public Lobby(string lobbyId)
        {
            LobbyId = lobbyId;
            players = new();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            SetForensicScientistForFirstPlayer(player);
        }

        private void SetForensicScientistForFirstPlayer(Player player)
        {
            if (players.Count == 1)
            {
                this.ForensicScientistId = player.ConnectionId;
            }
        }

        public void SetForensicScientistPlayer(string connectionId)
        {
            this.ForensicScientistId = connectionId;
        }

        internal void RemovePlayer(string connectionId)
        {
            players.RemoveAt(players.FindIndex(player => player.ConnectionId == connectionId));
        }
    }
}
