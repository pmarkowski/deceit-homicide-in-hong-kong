using System;
using System.Collections.Generic;
using Deceit.Backend.Domain.Players;

namespace Deceit.Backend.Domain.Lobbies
{
    public class Lobby
    {
        public string LobbyId { get; }
        List<Player> players;
        public IEnumerable<Player> Players => players;

        public Lobby(string lobbyId)
        {
            LobbyId = lobbyId;
            players = new();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        internal void RemovePlayer(string connectionId)
        {
            players.RemoveAt(players.FindIndex(player => player.ConnectionId == connectionId));
        }
    }
}
