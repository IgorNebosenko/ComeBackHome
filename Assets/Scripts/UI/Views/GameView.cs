using CBH.UI.Presenters;
using ElectrumGames.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.UI.Views
{
    public class GameView : View<GamePresenter>
    {
        [SerializeField] private TMP_Text textHeader;
        [SerializeField] private Image gpsImage;
    }
}