using CBH.Core;
using CBH.Core.Collision;
using CBH.Core.Configs;
using CBH.Core.Entity.Input;
using CBH.UI.Game.Presenters;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private RocketController rocketController;
    [SerializeField] private FinishCollisionObject landingPad;
    [SerializeField] private Camera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<RocketController>().FromInstance(rocketController);
        Container.Bind<FinishCollisionObject>().FromInstance(landingPad);
        Container.Bind<Camera>().FromInstance(mainCamera).WhenInjectedInto<GamePresenter>();

        Container.Bind<InputSchema>().WhenInjectedInto<RocketController>();
    }
}