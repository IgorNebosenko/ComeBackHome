using CBH.Analytics.Events;

namespace CBH.Analytics
{
    public interface IAnalyticsManager
    {
        void SendEvent(AnalyticsEvent analyticsEvent);
    }
}