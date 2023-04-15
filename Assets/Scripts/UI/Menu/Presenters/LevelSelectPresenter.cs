using System;
using System.Collections;
using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.Core.Levels;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using UniRx;

namespace CBH.UI.Menu.Presenters
{
    public class LevelSelectPresenter : Presenter<LevelSelectView>
    {
        private ViewManager _viewManager;

        private IAnalyticsManager _analyticsManager;
        private ILevelsManager _levelsManager;
        private IUserLevelsInfo _userLevelsInfo;

        private const string TooMuchTime = "--:--:--";
        private const double MaxTimeToDisplay = 3600;

        private int _cachedSelectedLevel = -1;

        public int LastCompletedScene => _userLevelsInfo.LastOpenedLevel;
        public int TotalLevels => _userLevelsInfo.TotalLevels;
        
        public LevelSelectPresenter(ViewManager viewManager, LevelSelectView view, 
            IAnalyticsManager analyticsManager, ILevelsManager levelsManager, IUserLevelsInfo userLevelsInfo) : base(view)
        {
            _viewManager = viewManager;

            _analyticsManager = analyticsManager;
            _levelsManager = levelsManager;
            _userLevelsInfo = userLevelsInfo;
        }

        public void OnToMenuClicked()
        {
            _analyticsManager.SendEvent(new CloseLevelSelectMenuEvent(LastCompletedScene));
            _viewManager.ShowView<MainMenuPresenter>();
        }

        public void LoadLevel(int level)
        {
            _cachedSelectedLevel = level;
            _analyticsManager.SendEvent(new StartLevelFromMenuEvent(_cachedSelectedLevel, LastCompletedScene));
            
            Observable.FromCoroutine(LoadLevelProcess).Subscribe();
        }

        private IEnumerator LoadLevelProcess()
        {
            yield return _levelsManager.LoadLevelByIndex(_cachedSelectedLevel);
        }

        public string GetBestLevelTime(int id)
        {
            var levelData = _userLevelsInfo.GetDataAboutLevel(id);
            if (levelData.timeFly >= MaxTimeToDisplay)
                return TooMuchTime;
            
            var time = TimeSpan.FromSeconds(levelData.timeFly);

            return $"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds / 10:00}";
        }
    }
}