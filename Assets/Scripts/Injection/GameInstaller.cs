using CBH.Core;
using CBH.Core.Configs;
using CBH.Core.Entity.Input;
using CBH.UI.Presenters;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private RocketController rocketController;
    [SerializeField] private Camera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<RocketController>().FromInstance(rocketController);
        Container.Bind<Camera>().FromInstance(mainCamera).WhenInjectedInto<GamePresenter>();

        Container.Bind<InputSchema>().WhenInjectedInto<RocketController>();
    }
}