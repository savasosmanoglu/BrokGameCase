using UnityEngine;

public class WalkStrategy : IMovementStrategy
{
    public void Move(CharacterController controller, CharacterData data, Vector3 input, float deltaTime)
    {
        Vector3 move = input.normalized * data.walkSpeed;
        controller.Move(move * deltaTime);
    }
}

