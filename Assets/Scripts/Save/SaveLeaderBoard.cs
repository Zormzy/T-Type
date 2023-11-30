using System;
using System.Collections.Generic;

[Serializable]
public class SaveLeaderBoard
{
    public Dictionary<string, int> leaderBoard;
    public List<string> players;
    public List<int> playersScores;
}
