using EPOOutline;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
public abstract class InteractableBase : MonoBehaviour, IInteractable, IHighlight
{
    public virtual bool IsSelectable { get; set; }
    private Outlinable _outline;

    public bool IsHighlighted { get; set; }

    protected virtual void Awake()
    {
        _outline = GetComponentInChildren<Outlinable>();
    }

    public virtual void Focus()
    {
        _outline.OutlineParameters.Enabled = true;
        IsHighlighted = true;
    }

    public virtual void UnFocus()
    {
        _outline.OutlineParameters.Enabled = false;
        IsHighlighted = false;
    }

    public virtual Transform GetTransform()
    {
        return transform;
    }

    public virtual void Interact(PlayerController player)
    {

    }
}
