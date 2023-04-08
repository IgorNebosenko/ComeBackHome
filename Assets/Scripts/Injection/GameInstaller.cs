using System.Reflection;
using CBH.Analytics;
using CBH.Core;
using CBH.Core.Collision;
using CBH.Core.Configs;
using CBH.Core.Entity.Input;
using CBH.UI.Game.UI.Game;
using UnityEngine;

public class GameInstaller : BaseSceneInstaller
{
    [SerializeField] private RocketController rocketController;
    [SerializeField] private FinishCollisionObject landingPad;
    [SerializeField] private Camera mainCamera;

    protected override Assembly UiAssembly => typeof(GameAssemblyPlaceholder).Assembly;

    public override void InstallBindings()
    {
        base.InstallBindings();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<RocketController>().FromInstance(rocketController);
        Container.Bind<FinishCollisionObject>().FromInstance(landingPad);

        Container.Bind<InputSchema>().WhenInjectedInto<RocketController>();
        Container.Bind<InputData>().AsSingle();
    }
}