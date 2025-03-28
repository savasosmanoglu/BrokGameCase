using UnityEngine;

public class PlayerNormalState : IState<PlayerController>
{
    private PlayerController player;
    private IInteractable currentInteractable = null;

    public PlayerNormalState(PlayerController player)
    {
        this.player = player;
    }

    public void OnUpdate()
    {
        DetectObject();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.TimescaleManager.PauseGame();
        }
    }

    public void OnExit()
    {
        if (currentInteractable is null) return;
        currentInteractable.UnFocus();
        currentInteractable = null;
    }

    private void DetectObject()
    {
        if (player.TryGetInteractable(player, out IInteractable interactable, player.InteractableLayerMask, QueryTriggerInteraction.Collide, 5f))
        {
            if (currentInteractable != interactable)
            {
                currentInteractable?.UnFocus();
                currentInteractable = interactable;
                currentInteractable.Focus();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact(player);
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
