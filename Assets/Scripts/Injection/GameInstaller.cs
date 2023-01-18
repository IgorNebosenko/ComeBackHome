using CBH.Core;
using CBH.Core.Entity;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RocketManager>().AsSingle();
        Container.Bind<GameManager>().AsSingle();
    }
}