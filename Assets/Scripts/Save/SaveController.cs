using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [Header("Components")]
    private SaveLeaderBoard save;
    [SerializeField] private TextMeshProUGUI _pseudoText;
    [SerializeField] private ScoreUI _scoreUI;

    [Header("Save variables")]
    private List<string> _playersPseudo;
    private List<int> _playersScores;
    private string saveContent;
    private string saveFile;

    [Header("Variables")]
    private string _playerPseudo;
    private string _playerPseudoTemp;
    private int _playerScore;
    private int _playerScoreTemp;

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/T-TypeLeaderBoard.json";
        SaveControllerInitialization();
    }

    private void Start()
    {
        if (File.Exists(saveFile))
            ReadSaveGame();
        else
            CreateSaveGame();
    }

    public List<string> GetPlayersPseudoList()
    {
        ReadSaveGame();
        return _playersPseudo;
    }

    public List<int> GetPlayersScoresList()
    {
        ReadSaveGame();
        return _playersScores;
    }

    public void GetPseudoAndScore()
    {
        ReadSaveGame();
        _playerPseudo = _pseudoText.text;
        _playerScore = _scoreUI._finalScore;
        AddPlayer(_playerPseudo, _playerScore);
    }

    private void AddPlayer(string _playerPseudo, int _playerScore)
    {
        _playersScores.Add(_playerScore);
        _playersPseudo.Add(_playerPseudo);

        if (_playersScores.Count >= 2)
        {
            int x = 0;
            while (x < _playersScores.Count - 1)
            {
                int y = 0;
                while (y < _playersScores.Count - 1 - x)
                {
                    if (_playersScores[y + 1] > _playersScores[y])
                    {
                        _playerScoreTemp = _playersScores[y + 1];
                        _playersScores[y + 1] = _playersScores[y];
                        _playersScores[y] = _playerScoreTemp;

                        _playerPseudoTemp = _playersPseudo[y + 1];
                        _playersPseudo[y + 1] = _playersPseudo[y];
                        _playersPseudo[y] = _playerPseudoTemp;
                    }
                    y++;
                }
                x++;
            }
        }
        _playersScores.RemoveRange(9, 1);
        _playersPseudo.RemoveRange(9, 1);
        SaveGame();
    }

    private void CreateSaveGame()
    {
        save = new SaveLeaderBoard();
        save.playersPseudo = new List<string>();
        save.playersScores = new List<int>();
        saveContent = JsonUtility.ToJson(save);
        File.WriteAllText(saveFile, saveContent);
    }

    private void ReadSaveGame()
    {
        saveContent = File.ReadAllText(saveFile);
        save = JsonUtility.FromJson<SaveLeaderBoard>(saveContent);
        _playersPseudo = save.playersPseudo;
        _playersScores = save.playersScores;
    }

    private void SaveGame()
    {
        save.playersScores = _playersScores;
        save.playersPseudo = _playersPseudo;
        saveContent = JsonUtility.ToJson(save);
        JsonUtility.FromJsonOverwrite(saveContent, save);
        File.WriteAllText(saveFile, saveContent);
    }

    private void SaveControllerInitialization()
    {
        _playersPseudo = new List<string>();
        _playersScores = new List<int>();
    }
}
