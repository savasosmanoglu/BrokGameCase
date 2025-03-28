using UnityEngine;

public class PlayerCarryState : IState<PlayerController>
{
    private PlayerController player;
    private ICarryable _carryObject;
    private IInteractable currentInteractable;


    public PlayerCarryState(PlayerController player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        if (player.CarryObject is ICarryable Carryable)
        {
            _carryObject = Carryable;
        }
    }

    public void OnUpdate()
    {
        DetectObject();

        if (Input.GetKeyDown(KeyCode.R))
        {
            _carryObject.Throw();
            player.StateMachine.TransitionTo(player.StateMachine.normalState);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void OnExit()
    {
        if (currentInteractable is null) return;
        currentInteractable.UnFocus();
        currentInteractable = null;
    }

    private void Interact()
    {
        if (currentInteractable is ITrashable trashable)
        {
            _carryObject.Trash(currentInteractable.GetTransform(), player);
            player.StateMachine.TransitionTo(player.StateMachine.normalState);
        }
    }

    private void DetectObject()
    {
        if (player.TryGetInteractable(player, out IInteractable interactable, player.CarryStateLayerMask))
        {
            if (currentInteractable != interactable)
            {
                currentInteractable?.UnFocus();
                currentInteractable = interactable;
                currentInteractable.Focus();
            }
        }
        else
        {
            if (currentInteractable is null) return;
            currentInteractable?.UnFocus();
            currentInteractable = null;
        }
    }
}
