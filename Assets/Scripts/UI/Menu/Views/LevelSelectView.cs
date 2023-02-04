using CBH.Core;
using CBH.UI.Menu.Presenters;
using CBH.UI.Menu.UI.Menu.Components;
using ElectrumGames.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Menu.Views
{
    [AutoRegisterView]
    public class LevelSelectView : View<LevelSelectPresenter>
    {
        [SerializeField] private Button toMenuButton;
        [SerializeField] private RectTransform containerLevels;
        [Space] 
        [SerializeField] private LevelButton levelButtonTemplate;

        public void Init(LevelSelectPresenter presenter, int lastCompletedScene)
        {
            toMenuButton.onClick.AddListener(presenter.OnToMenuClicked);

            for (var i = 0; i < GameData.CountLevels; i++)
            {
                var j = i + 1; //<- Solve closure
                var levelButton = Instantiate(levelButtonTemplate, containerLevels);
                levelButton.Init(() => presenter.LoadLevel(j), j, j <= lastCompletedScene);
            }
        }

        private void OnDestroy()
        {
            toMenuButton.onClick.RemoveListener(Presenter.OnToMenuClicked);
        }
    }
}