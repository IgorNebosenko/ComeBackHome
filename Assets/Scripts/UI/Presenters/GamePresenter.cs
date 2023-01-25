using CBH.Core.Entity.Input;
using CBH.UI.Views;
using ElectrumGames.MVP;

namespace CBH.UI.Presenters
{
    public class GamePresenter : Presenter<GameView>
    {
        public GamePresenter(GameView view, RocketController rocketController) : base(view)
        {
        }
    }
}