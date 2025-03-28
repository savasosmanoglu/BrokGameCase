using System;
using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public abstract class PanelBase : MonoBehaviour
{
    public Action OnPanelShown;
    public Action OnPanelHide;

    private CanvasGroup _canvasGroup;
    protected CanvasGroup CanvasGroup => _canvasGroup;

    protected void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ShowPanel()
    {
        if (CanvasGroup.alpha > 0)
            return;

        IPanelAnimation panelAnimation = GetComponent<IPanelAnimation>();

        if (panelAnimation != null)
            panelAnimation.DoShowAnimation();
        else SetPanel(1, true, true);
    }

    public virtual void HidePanel()
    {
        if (CanvasGroup.alpha == 0)
            return;

        IPanelAnimation panelAnimation = GetComponent<IPanelAnimation>();

        if (panelAnimation != null)
            panelAnimation.DoHideAnimation();
        else SetPanel(0, false, false);
    }

    private void SetPanel(float alpha, bool interactable, bool blocksRaycast)
    {
        CanvasGroup.alpha = alpha;
        CanvasGroup.interactable = interactable;
        CanvasGroup.blocksRaycasts = blocksRaycast;
    }

    private void TogglePanel()
    {
        if (CanvasGroup.alpha == 0)
            ShowPanel();
        else HidePanel();
    }
}

public interface IPanelAnimation
{
    public float Duration { get; set; }
    public Ease ShowEase { get; set; }
    public Ease HideEase { get; set; }

    void DoShowAnimation();
    void DoHideAnimation();


}
