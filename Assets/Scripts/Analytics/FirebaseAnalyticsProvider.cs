using System;
using System.Collections.Generic;
using System.Linq;
using CBH.Analytics.Events;
// using Firebase;
// using Firebase.Analytics;
// using Firebase.Extensions;
using UnityEngine;

namespace CBH.Analytics
{
    public class FirebaseAnalyticsProvider : IAnalyticsProvider
    {
        private bool _enableLogs;
        private Queue<AnalyticsEvent> _bufferEvents;

        public bool Ready { get; private set; }
        
        public void Init(bool enableLogs)
        {
            _enableLogs = enableLogs;
            _bufferEvents = new Queue<AnalyticsEvent>();
            // FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task =>
            // {
            //     FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            //     SendBufferEvents();
            //     if (_enableLogs)
            //         Debug.Log($"[{GetType().Name}] analytics enabled!");
            //     Ready = true;
            // });
        }

        public void SendEvent(AnalyticsEvent analyticsEvent)
        {
            if (Ready)
                HandleEvent(analyticsEvent);
            else
                _bufferEvents.Enqueue(analyticsEvent);
        }

        private void SendBufferEvents()
        {
            while (_bufferEvents.Count > 0)
                HandleEvent(_bufferEvents.Dequeue());
                
        }
        private void HandleEvent(AnalyticsEvent analyticsEvent)
        {
            if (_enableLogs)
                Debug.Log($"[FirebaseAnalyticsProvider] send event {analyticsEvent.Key}");
            
            // if (analyticsEvent.Data != null)
            //     FirebaseAnalytics.LogEvent(analyticsEvent.Key, ConvertToParameters(analyticsEvent.Data));
            // else
            //     FirebaseAnalytics.LogEvent(analyticsEvent.Key);
        }

        // private Parameter[] ConvertToParameters(Dictionary<string, object> data)
        // {
        //     return data.Select(p => ConvertToParameter(p.Key, p.Value)).ToArray();
        // }
        //
        // private Parameter ConvertToParameter(string key, object value)
        // {
        //     switch (value)
        //     {
        //         case int _:
        //         case long _:
        //             return new Parameter(key, Convert.ToInt64(value));
        //         case float _:
        //         case double _:
        //             return new Parameter(key, Convert.ToDouble(value));
        //         case string s:
        //             return new Parameter(key, s);
        //         default:
        //             return new Parameter(key, value.ToString());
        //     }
        // }
    }
}