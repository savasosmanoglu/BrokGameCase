using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PanelGame : PanelBase
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
    private TimescaleManager _timescaleManager;

    private const string SCENE_MENU = "Scene_MainMenu";

    [Inject]
    public void Construct(TimescaleManager timescaleManager)
    {
        _timescaleManager = timescaleManager;
    }

    void OnEnable()
    {
        _timescaleManager.OnPauseGame += ShowPanel;
        _timescaleManager.OnResumeGame += HidePanel;
        resumeButton.onClick.AddListener(_timescaleManager.ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    void OnDisable()
    {
        _timescaleManager.OnPauseGame -= ShowPanel;
        _timescaleManager.OnResumeGame -= HidePanel;
        resumeButton.onClick.RemoveListener(_timescaleManager.ResumeGame);
        restartButton.onClick.RemoveListener(RestartGame);
        mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    private void RestartGame()
    {
        _timescaleManager.ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu()
    {
        _timescaleManager.ResetTimeScale();
        SceneManager.LoadScene(SCENE_MENU);
    }
}
