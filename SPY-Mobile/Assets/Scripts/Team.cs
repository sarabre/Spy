using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List<Player> players;
    public int NumberOfPlayer;
    public int IndexOfSpy;
    public int NumberOfRound;
    public int CurrentRound;

    public float RoundDuration;
    public List<string> Words;
}

public class Player
{
    public string name;
    public int Score;
}