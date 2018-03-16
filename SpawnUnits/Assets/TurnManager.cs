using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Player> Players;
    int maxPlayers;
    int currentPlayer = 0;

    public void Start()
    {
        foreach (Player player in Players)
        {
            player.Disable();
        }
        EndTurn();
    }
        public void EndTurn()
        {
            Players[currentPlayer].Enable();

            maxPlayers = Players.Count - 1;

            if (maxPlayers > currentPlayer)
            {
                currentPlayer++;
            }
            else
            {
                currentPlayer = 0;
            }
            Players[currentPlayer].Disable();
        }
    }

