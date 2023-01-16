using System;
using System.Collections.Generic;
using System.Reflection;
using CBH.Core;
using CBH.Core.Configs;
using CBH.UI;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.MVP.Utils;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [SerializeField] private CanvasContainer canvasContainerTemplate;
    [SerializeField] private FpsConfig fpsConfig;
    
    public override void InstallBindings()
    {
        var canvasContainer = Instantiate(canvasContainerTemplate);

        var presenterFactory = new PresenterFactory(Container);
        var allViews = AutoViewInstall();
        
        Container.Bind<GameData>().AsSingle();
        
        var viewManager = new ViewManager(allViews.views, canvasContainer.viewContainer, presenterFactory);
        var popupManager = new PopupManager(allViews.popups, canvasContainer.popupContainer, presenterFactory);
        
        Container.Bind<PresenterFactory>().FromInstance(presenterFactory).AsSingle();
        Container.BindInstance(viewManager);
        Container.BindInstance(popupManager);

        Container.BindInstance(fpsConfig).AsSingle();
    }

    private (List<(Type view, Type presenter)> views, List<(Type view, Type presenter)> popups) AutoViewInstall()
    {
        var bindWrapper = typeof(ProjectInstaller)
            .GetMethod("BindWrapper", BindingFlags.Instance | BindingFlags.NonPublic);
        
        var viewRegistrationList = new List<(Type, Type)>();
        var popupsRegistrationList = new List<(Type, Type)>();

        var bindings = AutoRegisterViewAttribute.GetViews(new[] {typeof(CanvasContainer).Assembly});
        foreach (var binding in bindings)
        {
            var viewType = binding.view;
            var presenterType = viewType.BaseType.GetGenericArguments()[0];

            var bindingMethod = bindWrapper.MakeGenericMethod(viewType, presenterType);
            bindingMethod.Invoke(this, new object[] {binding.path});

            if (CheckForPopupCoroutine(presenterType))
                popupsRegistrationList.Add((viewType, presenterType));
            else
                viewRegistrationList.Add((viewType, presenterType));
        }

        return (viewRegistrationList, popupsRegistrationList);
    }
    
    private bool CheckForPopupCoroutine(Type presenterType)
    {
        var tType = presenterType;
            
        do
        {
            if (tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(PopupPresenterCoroutine<,,>))
                return true;

            tType = tType.BaseType;
        }
        while(tType != null);

        return false;
    }
    
    private void BindWrapper<TView, TPresenter>(string path)
    {
        Container.BindViewPresenter<TView, TPresenter>(path);
    }
}