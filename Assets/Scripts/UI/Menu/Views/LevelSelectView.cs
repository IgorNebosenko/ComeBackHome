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

        private void Start()
        {
            toMenuButton.onClick.AddListener(Presenter.OnToMenuClicked);

            for (var i = 0; i < Presenter.TotalLevels; i++)
            {
                var closureRepairIndex = i;
                var levelButton = Instantiate(levelButtonTemplate, containerLevels);
                levelButton.Init(() => Presenter.LoadLevel(closureRepairIndex), closureRepairIndex, Presenter.GetBestLevelTime(closureRepairIndex),
                    closureRepairIndex <= Presenter.LastCompletedScene);
            }
        }

        protected override void OnBeforeClose()
        {
            toMenuButton.onClick.RemoveListener(Presenter.OnToMenuClicked);
        }
    }
}