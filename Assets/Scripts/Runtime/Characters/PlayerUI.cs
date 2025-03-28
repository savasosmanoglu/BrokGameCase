using UnityEngine;
using Zenject;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    private TimescaleManager _timescaleManager;
    [Inject]
    public void Construct(TimescaleManager timescaleManager)
    {
        _timescaleManager = timescaleManager;
    }

    void OnEnable()
    {
        _timescaleManager.OnPauseGame += HideCrosshair;
        _timescaleManager.OnResumeGame += ShowCrosshair;
    }

    void OnDisable()
    {
        _timescaleManager.OnPauseGame -= HideCrosshair;
        _timescaleManager.OnResumeGame -= ShowCrosshair;
    }

    private void ShowCrosshair() => crosshair.SetActive(true);
    private void HideCrosshair() => crosshair.SetActive(false);
}
