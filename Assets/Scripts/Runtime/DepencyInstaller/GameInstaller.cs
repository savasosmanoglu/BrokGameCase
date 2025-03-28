using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterLoader characterLoaderPrefab;
    public override void InstallBindings()
    {
        Container.Bind<CharacterLoader>().FromComponentInNewPrefab(characterLoaderPrefab).AsSingle().NonLazy();
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        Container.Bind<ICharacterFactory>().To<CharacterFactory>().AsSingle();
        Container.Bind<IMovementStrategy>().To<WalkStrategy>().AsSingle();
    }
}
