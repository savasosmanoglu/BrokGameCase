
using UnityEngine;

public interface IInteractable : IHighlight
{
    bool IsSelectable { get; set; }
    void Interact(PlayerController player);
    Transform GetTransform();

}
