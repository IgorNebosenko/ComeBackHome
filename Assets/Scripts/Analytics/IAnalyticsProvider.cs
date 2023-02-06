using CBH.Analytics.Events;

namespace CBH.Analytics
{
    public interface IAnalyticsProvider
    {
        bool Ready { get; }
        void Init(bool enableLogs);
        void SendEvent(AnalyticsEvent analyticsEvent);
    }
}