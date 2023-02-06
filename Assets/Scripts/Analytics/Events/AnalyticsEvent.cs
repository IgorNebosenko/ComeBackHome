using System;
using System.Collections.Generic;

namespace CBH.Analytics.Events
{
    public abstract class AnalyticsEvent
    {
        public readonly int RegistrationTime;
        public abstract string Key { get; }
        public readonly Dictionary<string, object> Data;

        protected AnalyticsEvent()
        {
            RegistrationTime = DateTime.UtcNow.Second;
            Data = new Dictionary<string, object>();
        }
    }
}