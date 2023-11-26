using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _creditsCanvas;
    [SerializeField] private GameObject _playBtn;
    [SerializeField] private GameObject _creditsBtn;
    [SerializeField] private GameObject _quitBtn;
    [SerializeField] private GameObject _returnBtn;

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
        EventSystem.current.SetSelectedGameObject(_returnBtn);
    }

    public void OnReturnBtn()
    {
        _playBtn.SetActive(true);
        _creditsBtn.SetActive(true);
        _quitBtn.SetActive(true);
        _creditsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_playBtn);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }
}
