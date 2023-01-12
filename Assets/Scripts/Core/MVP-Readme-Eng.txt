Version 1.0.0

1. Required ZenJect
2. Data structures are realized by check ELECTRUM_CORE. If Core connected - data structures will uses from core
3. Plugin works with IEnumerable, variant fore custom tasks - not realized

Using:
- Installer, wich creates instances ViewManager & PopupManager = at method AutoRegisterViewAttribute.GetViews() - must referenced to assembly, where contains presenyers and views, else - it not found anything and nothing register
- Prefabs with Views - must be at Resources folder, as default - at Resources/Views, if path different from this - at AutoRegisterView - need to set other path
- Presenters are support inject from ctor

At examples - writed MvpSceneInstaller, and at examples of view or poup - you can see, how to make correct connection MVP