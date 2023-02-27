using CBH.Ads;
using CBH.Analytics;
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
    
    [SerializeField] private AdsConfig adsConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<GameData>().AsSingle();
        Container.BindInstance(fpsConfig).AsSingle();
        Container.Bind<AudioManager>().AsSingle().WithArguments(audioMixer);

        Container.BindInstance(adsConfig).AsSingle();
        Container.Bind<AdsData>().AsSingle();
        
#if UNITY_EDITOR
        Container.Bind<IAdsProvider>().To<EditorAdsProvider>().AsSingle();
        Container.Bind<IAnalyticsManager>().To<EditorAnalyticsManager>().AsSingle();
#elif UNITY_ANDROID
        Container.Bind<IAnalyticsManager>().To<AndroidAnalyticsManager>().AsSingle();
        Container.Bind<IAdsProvider>().To<AndroidAdsProvider>().AsSingle();
#endif
    }
}