using System.Collections;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UniRx;
using UnityEngine.SceneManagement;

namespace CBH.UI.Menu.Presenters
{
    public class LevelSelectPresenter : Presenter<LevelSelectView>
    {
        private IAnalyticsManager _analyticsManager;
        private ViewManager _viewManager;
        private GameData _gameData;

        private int _cachedSelectedLevel = -1;

        public int LastCompletedScene => _gameData.LastCompletedScene;
        
        public LevelSelectPresenter(ViewManager viewManager, IAnalyticsManager analyticsManager,
            GameData gameData, LevelSelectView view) : base(view)
        {
            _analyticsManager = analyticsManager;
            _viewManager = viewManager;
            _gameData = gameData;
        }

        public void OnToMenuClicked()
        {
            _analyticsManager.SendEvent(new CloseLevelSelectMenuEvent(_gameData.LastCompletedScene));
            _viewManager.ShowView<MainMenuPresenter>();
        }

        public void LoadLevel(int level)
        {
            _cachedSelectedLevel = level;
            _analyticsManager.SendEvent(new StartLevelFromMenuEvent(level, _gameData.LastCompletedScene));
            Observable.FromCoroutine(LoadLevelProcess).Subscribe();
        }

        private IEnumerator LoadLevelProcess()
        {
            yield return SceneManager.LoadSceneAsync(_cachedSelectedLevel);
        }
    }
}