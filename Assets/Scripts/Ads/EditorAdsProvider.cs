#if UNITY_EDITOR
using UnityEngine;

namespace CBH.Ads
{
    public class EditorAdsProvider : IAdsProvider
    {
        public EditorAdsProvider()
        {
            Debug.Log("[EditorAdsProvider] CTOR");
            LoadInterstitial();
        }
        
        public void LoadInterstitial()
        {
            Debug.Log("[EditorAdsProvider] Load interstitial");
        }

        public void ShowInterstitial()
        {
            Debug.Log("[EditorAdsProvider] Show interstitial");
            LoadInterstitial();
        }
    }
}
#endif