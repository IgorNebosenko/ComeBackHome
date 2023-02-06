using System.Collections;
using System.Collections.Generic;
using CBH.Core;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Utils;
using UniRx;
using UnityEngine.SceneManagement;

namespace CBH.UI.Menu.Presenters
{
    public class NewGameWarnPresenter : PopupPresenterCoroutine<NewGameWarnPopup, PopupArgs, PopupResult>
    {
        private GameData _gameData;
        
        public NewGameWarnPresenter(NewGameWarnPopup view, GameData gameData) : base(view)
        {
            _gameData = gameData;
            view.SubscribeEvents(this);
        }

        public void RestartButtonClicked()
        {
            _gameData.ResetGame();
            Observable.FromCoroutine(ProcessLoadScene).Subscribe();

        }

        private IEnumerator ProcessLoadScene()
        {
            yield return SceneManager.LoadSceneAsync(_gameData.LastCompletedScene);
            Close();
        }

        public override IEnumerable<PopupResult> Init(PopupArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}