using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    SaveLeaderBoard save;
    private List<string> _players;
    private List<int> _playersScores;
    string saveContent;
    string saveFile;

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/T-TypeLeaderBoard.json";
    }

    private void Start()
    {
        if (File.Exists(saveFile))
            ReadSaveGame();
        else
            CreateSaveGame();
    }

    public void AddPlayer(string _playerPseudo, int _playerScore)
    {
        for (int i = 0; i < _playersScores.Count; i++)
        {
            if (_playerScore >= _playersScores[i])
            {
                _playersScores.RemoveAt(_playersScores.Count - 1);
                _playersScores.Insert(i, _playerScore);
                _players.RemoveAt(_players.Count - 1);
                _players.Insert(i, _playerPseudo);
            }
        }
    }

    private void CreateSaveGame()
    {
        save = new SaveLeaderBoard();
        save.players = new List<string>(10);
        save.playersScores = new List<int>(10);
        saveContent = JsonUtility.ToJson(save);
        File.WriteAllText(saveFile, saveContent);
    }

    private void ReadSaveGame()
    {
        saveContent = File.ReadAllText(saveFile);
        save = JsonUtility.FromJson<SaveLeaderBoard>(saveContent);
        _players = save.players;
        _playersScores = save.playersScores;
    }
}
