using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private TimescaleManager timescaleManager;
    public override void InstallBindings()
    {
        Container.Bind<DataManager>().FromInstance(dataManager).AsSingle().NonLazy();
        Container.Bind<TimescaleManager>().FromInstance(timescaleManager).AsSingle();
    }
}
