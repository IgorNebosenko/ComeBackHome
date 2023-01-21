using CBH.Core;
using CBH.Core.Configs;
using CBH.Core.Entity.Input;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();

        Container.Bind<InputSchema>().WhenInjectedInto<RocketController>();
    }
}