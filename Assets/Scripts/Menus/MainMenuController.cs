using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject _playBtn;
    [SerializeField] private GameObject _creditsBtn;
    [SerializeField] private GameObject _leaderBoardBtn;
    [SerializeField] private GameObject _quitBtn;
    [SerializeField] private GameObject _returnBtn;

    [Header("Credits")]
    [SerializeField] private GameObject _creditsCanvas;

    [Header("LeaderBoard")]
    [SerializeField] private GameObject _leaderBoardCanvas;
    [SerializeField] private SaveController _saveController;
    [SerializeField] private TextMeshProUGUI _leaderBoardPseudoText;
    [SerializeField] private TextMeshProUGUI _leaderBoardScoreText;
    private List<string> _playersPseudo = new List<string>();
    private List<int> _playersScore = new List<int>();
    private string _leaderBoardPseudos;
    private string _leaderBoardScores;

    public void OnPlayBtn()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnCreditsBtn()
    {
        _playBtn.SetActive(false);
        _creditsBtn.SetActive(false);
        _leaderBoardBtn.SetActive(false);
        _quitBtn.SetActive(false);
        _creditsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_returnBtn);
    }

    public void OnLeaderBoardButton()
    {
        _playBtn.SetActive(false);
        _creditsBtn.SetActive(false);
        _leaderBoardBtn.SetActive(false);
        _quitBtn.SetActive(false);
        _leaderBoardCanvas.SetActive(true);
        PrepareLeaderBoard();
        EventSystem.current.SetSelectedGameObject(_returnBtn);
    }

    public void OnReturnBtn()
    {
        _playBtn.SetActive(true);
        _creditsBtn.SetActive(true);
        _leaderBoardBtn.SetActive(true);
        _quitBtn.SetActive(true);
        _creditsCanvas.SetActive(false);
        _leaderBoardCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_playBtn);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

    private void PrepareLeaderBoard()
    {
        _leaderBoardPseudos = "";
        _leaderBoardScores = "";
        _playersPseudo = _saveController.GetPlayersPseudoList();
        _playersScore = _saveController.GetPlayersScoresList();
        int i = 1;
        foreach (string player in _playersPseudo)
        {
            _leaderBoardPseudos += "N°" + i + " : " + player + "\n";
            i++;
        }

        foreach (int score in  _playersScore)
        {
            _leaderBoardScores += score + "\n";
        }
        _leaderBoardPseudoText.text = _leaderBoardPseudos;
        _leaderBoardScoreText.text = _leaderBoardScores;
    }
}
