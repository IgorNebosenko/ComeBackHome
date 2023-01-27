using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;

namespace CBH.UI.Menu.Presenters
{
    public class HowPlayPresenter : Presenter<HowPlayView>
    {
        private ViewManager _viewManager;
        
        public HowPlayPresenter(HowPlayView view, ViewManager viewManager) : base(view)
        {
            _viewManager = viewManager;
            
            view.SubscribeEvents(this);
        }

        public void OnButtonBackPressed()
        {
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}