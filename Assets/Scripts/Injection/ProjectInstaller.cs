using CBH.Core;
using CBH.Core.Audio;
using CBH.Core.Configs;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [SerializeField] private FpsConfig fpsConfig;
    [SerializeField] private AudioMixer audioMixer;
    
    public override void InstallBindings()
    {
        Container.Bind<GameData>().AsSingle();
        Container.BindInstance(fpsConfig).AsSingle();
        Container.Bind<AudioManager>().AsSingle().WithArguments(audioMixer);
    }
}