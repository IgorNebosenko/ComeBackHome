using System.Collections;
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

        private int _cachedSelectedLevel = -1;

        public int LastCompletedScene => _gameData.LastCompletedScene;
        
        public LevelSelectPresenter(ViewManager viewManager, GameData gameData, LevelSelectView view) : base(view)
        {
            _viewManager = viewManager;
            _gameData = gameData;
        }

        public void OnToMenuClicked()
        {
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