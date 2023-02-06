using System.Collections.Generic;
using CBH.Analytics.Events;
using UnityEngine;

namespace CBH.Analytics
{
    public class DefaultAnalyticsManager : IAnalyticsManager

    {
        private List<IAnalyticsProvider> _analyticsProviders;
        private bool _enableLogs;

        public DefaultAnalyticsManager()
        {
            _analyticsProviders = new List<IAnalyticsProvider>()
            {
                new FirebaseAnalyticsProvider()
            };

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _enableLogs = true;
#endif
            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.Init(_enableLogs);
        }

        public void SendEvent(AnalyticsEvent analyticsEvent)
        {
            if (_analyticsProviders.Count == 0 && _enableLogs)
                Debug.LogWarning($"[{GetType().Name}] no analytics providers in list for send event {analyticsEvent}!");

            foreach (var analyticsProvider in _analyticsProviders)
                analyticsProvider.SendEvent(analyticsEvent);

        }
    }
}
