using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBtnController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PauseController _pauseController;

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene("Level");
    }

    public void ReturnBtn()
    {
        _pauseController.UnPauseGame();
    }
}
