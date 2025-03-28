using UnityEngine;

public interface ICarryable
{
    void Throw();
    void Trash(Transform target, PlayerController player);

}
