using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class CharacterFactory : ICharacterFactory
{
    private readonly DiContainer _container;

    public CharacterFactory(DiContainer container)
    {
        _container = container;
    }

    public async Task<GameObject> CreateCharacter(int index)
    {
        var handle = Addressables.LoadAssetsAsync<GameObject>("Characters");
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return _container.InstantiatePrefab(handle.Result[index]);
        }
        else
        {
            Debug.LogError("Character not found!");
            return null;
        }
    }
}
