using UnityEngine;

public interface IMovementStrategy
{
    void Move(CharacterController controller, CharacterData data, Vector3 input, float deltaTime);
}
