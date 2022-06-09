using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    public int NumberOfPlayer;
    public int NumberOfSpy;
    public int IndexOfSpy;
    public int NumberOfRound;
    public int CurrentRound;

    public float RoundDuration;
    

    
    public void AddPlayer(string name)
    {
        Player player = new Player();
        player.name = name;
        players.Add(player);
        NumberOfPlayer++;
    }

    
}

[SerializeField]
public class Player
{
    public string name;
    public int Score;
}