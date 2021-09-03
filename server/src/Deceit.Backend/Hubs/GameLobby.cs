using System.Collections.Generic;

namespace Deceit.Backend.Hubs
{
    class GameLobby
    {
        List<string> Players = new();

        public void AddPlayer(string name)
        {
            Players.Add(name);
        }
    }
}
