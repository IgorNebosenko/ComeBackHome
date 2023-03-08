using CBH.Analytics;
using CBH.Analytics.Events;
using CBH.UI.Menu.Views;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;

namespace CBH.UI.Menu.Presenters
{
    public class HowPlayPresenter : Presenter<HowPlayView>
    {
        private ViewManager _viewManager;
        private IAnalyticsManager _analyticsManager;
        
        public HowPlayPresenter(HowPlayView view, ViewManager viewManager, IAnalyticsManager analyticsManager) : base(view)
        {
            _viewManager = viewManager;
            _analyticsManager = analyticsManager;
        }

        public void OnButtonBackPressed()
        {
            _analyticsManager.SendEvent(new CloseHowPlayMenuEvent());
            _viewManager.ShowView<MainMenuPresenter>();
        }
    }
}