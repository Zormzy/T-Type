using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _creditsCanvas;
    [SerializeField] private GameObject _playBtn;
    [SerializeField] private GameObject _creditsBtn;
    [SerializeField] private GameObject _quitBtn;

    public void OnPlayBtn()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnCreditsBtn()
    {
        _playBtn.SetActive(false);
        _creditsBtn.SetActive(false);
        _quitBtn.SetActive(false);
        _creditsCanvas.SetActive(true);
    }

    public void OnReturnBtn()
    {
        _playBtn.SetActive(true);
        _creditsBtn.SetActive(true);
        _quitBtn.SetActive(true);
        _creditsCanvas.SetActive(false);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }
}
