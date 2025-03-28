using System;
using UnityEngine;

public class TimescaleManager : MonoBehaviour
{
    [SerializeField] float defaultTimeScale = 1f;

    public Action OnPauseGame = delegate { };
    public Action OnResumeGame = delegate { };

    public void SetTimeScale(float timeScale) => Time.timeScale = timeScale;

    public void ResetTimeScale() => SetTimeScale(defaultTimeScale);

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SetTimeScale(0f);
        OnPauseGame.Invoke();
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SetTimeScale(defaultTimeScale);
        OnResumeGame.Invoke();
    }
}
