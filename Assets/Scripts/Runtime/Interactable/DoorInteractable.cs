using DG.Tweening;
using UnityEngine;

public class DoorInteractable : InteractableBase
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    private Tween _leftDoorTween;
    private Tween _rightDoorTween;

    protected override void Awake()
    {
        base.Awake();
    }
    public override void Interact(PlayerController player)
    {
        IsSelectable = !IsSelectable;
        if (IsSelectable)
        {
            _leftDoorTween.Kill();
            _rightDoorTween.Kill();
            _leftDoorTween = leftDoor.DOLocalRotate(new Vector3(0, -90, 0), 1f);
            _rightDoorTween = rightDoor.DOLocalRotate(new Vector3(0, -90, 0), 1f);
        }
        else
        {
            _leftDoorTween.Kill();
            _rightDoorTween.Kill();
            _leftDoorTween = leftDoor.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            _rightDoorTween = rightDoor.DOLocalRotate(new Vector3(0, -180, 0), 1f);
        }
    }
}
