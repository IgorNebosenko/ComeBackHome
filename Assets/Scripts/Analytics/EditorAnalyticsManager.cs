using CBH.Analytics.Events;
using UnityEngine;

namespace CBH.Analytics
{
    public class EditorAnalyticsManager : IAnalyticsManager
    {
        public EditorAnalyticsManager()
        {
            Debug.Log("[EditorAnalyticsManager] CTOR");
        }
        public void SendEvent(AnalyticsEvent analyticsEvent)
        {
            Debug.Log($"[EditorAnalyticsManager] Send analytics event with key: {analyticsEvent.Key}. Data: {analyticsEvent.Data}");
        }
    }
}