using UnityEngine;

public class SprintStrategy : IMovementStrategy
{
    public void Move(CharacterController controller, CharacterData data, Vector3 input, float deltaTime)
    {
        Vector3 move = input.normalized * data.sprintSpeed;
        controller.Move(move * deltaTime);
    }
}

