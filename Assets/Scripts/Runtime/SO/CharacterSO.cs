using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class CharacterSO : ScriptableObject
{
    public float WalkSpeed = 5f;
    public float SprintSpeed = 10f;
    public float JumpForce = 5f;
    public float HorizontalLookSensitivity = 2f;
    public float VerticalLookSensitivity = 2f;
    public Vector2 VerticalLookLimits = new Vector2(-90f, 90f);

    public void SetWalkSpeed(float value) => WalkSpeed = value;
    public void SetSprintSpeed(float value) => SprintSpeed = value;
    public void SetJumpForce(float value) => JumpForce = value;
}
