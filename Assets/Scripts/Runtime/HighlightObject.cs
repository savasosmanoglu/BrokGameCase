using EPOOutline;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
public class HighlightObject : MonoBehaviour, IHighlight
{
    private Outlinable _outline;

    public bool IsHighlighted { get; set; }

    void Awake()
    {
        _outline = GetComponent<Outlinable>();
    }

    public void Focus()
    {
        _outline.OutlineParameters.Enabled = true;
        IsHighlighted = true;
    }

    public void UnFocus()
    {
        _outline.OutlineParameters.Enabled = false;
        IsHighlighted = false;
    }
}
