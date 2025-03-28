using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tween _hoverTween;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverTween?.Kill();
        _hoverTween = transform.DOScale(1.15f, 0.25f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverTween?.Kill();
        _hoverTween = transform.DOScale(1f, 0.25f).SetUpdate(true);
    }

    private void OnDisable()
    {
        _hoverTween?.Kill();
    }

    private void OnDestroy()
    {
        _hoverTween?.Kill();
    }
}
