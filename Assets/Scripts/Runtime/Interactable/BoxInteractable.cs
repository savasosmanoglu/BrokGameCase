using DG.Tweening;
using UnityEngine;

public class BoxInteractable : InteractableBase, ICarryable
{
    private Rigidbody _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    public override void Interact(PlayerController player)
    {
        player.CarryObject = this;
        UnFocus();
        _rb.isKinematic = true;
        transform.SetParent(player.CameraTransform);
        transform.localPosition = new Vector3(0, -0.7f, 1.4f);
        transform.localRotation = Quaternion.Euler(-30f, 0, 0);
        player.StateMachine.TransitionTo(player.StateMachine.carryState);
    }

    public void Throw()
    {
        transform.SetParent(null);
        _rb.isKinematic = false;
        _rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    public void Trash(Transform target, PlayerController player)
    {
        transform.DOJump(target.position, 1.5f, 1, .35f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
