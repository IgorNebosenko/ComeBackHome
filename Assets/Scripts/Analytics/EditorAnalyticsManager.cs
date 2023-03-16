using System.Collections.Generic;
using CBH.Analytics.Events;
using Newtonsoft.Json;
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
            Debug.Log($"[EditorAnalyticsManager] Send analytics event with key: {analyticsEvent.Key}. Data: {DictionaryToString(analyticsEvent.Data)}");
        }

        private string DictionaryToString(Dictionary<string, object> dictionary)
        {
            return JsonConvert.SerializeObject(dictionary);
        }
    }
}