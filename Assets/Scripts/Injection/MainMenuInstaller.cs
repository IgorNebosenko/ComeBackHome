using System.Reflection;
using CBH.UI.Menu.UI.Menu;

public class MainMenuInstaller : BaseSceneInstaller
{
    protected override Assembly UiAssembly => typeof(MenuAssemblyPlaceholder).Assembly;
}