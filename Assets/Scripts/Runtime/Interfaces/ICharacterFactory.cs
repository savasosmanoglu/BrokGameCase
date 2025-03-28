using System.Threading.Tasks;
using UnityEngine;

public interface ICharacterFactory
{
    Task<GameObject> CreateCharacter(int index);
}
