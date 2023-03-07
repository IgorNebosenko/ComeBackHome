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
        private ViewManager _viewManager;
        private GameData _gameData;

        private IAnalyticsManager _analyticsManager;

        private int _cachedSelectedLevel = -1;

        public int LastCompletedScene => _gameData.LastCompletedScene;
        
        public LevelSelectPresenter(ViewManager viewManager, GameData gameData, LevelSelectView view, 
            IAnalyticsManager analyticsManager) : base(view)
        {
            _viewManager = viewManager;
            _gameData = gameData;

            _analyticsManager = analyticsManager;
        }

        public void OnToMenuClicked()
        {
            _analyticsManager.SendEvent(new CloseLevelSelectMenuEvent(LastCompletedScene));
            _viewManager.ShowView<MainMenuPresenter>();
        }

        public void LoadLevel(int level)
        {
            _cachedSelectedLevel = level;
            Observable.FromCoroutine(LoadLevelProcess).Subscribe();
        }

        private IEnumerator LoadLevelProcess()
        {
            yield return SceneManager.LoadSceneAsync(_cachedSelectedLevel);
        }
    }
}