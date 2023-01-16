using CBH.UI.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Views
{
    [AutoRegisterView]
    public class SettingsView : View<SettingsPresenter>
    {
        [SerializeField] private Toggle enableMusic;
        [SerializeField] private Toggle enableSounds;
        [SerializeField] private Slider fpsSlider;
        [SerializeField] private TMP_Text fpsText;
        [SerializeField] private Button buttonExit;

        public void Init(SettingsPresenter presenter)
        {
            
        }
    }
}