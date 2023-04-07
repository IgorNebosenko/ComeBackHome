using UnityEngine;

namespace CBH.Ads
{
    [CreateAssetMenu(fileName = "AdsConfig", menuName = "Configs/Ads config")]
    public class AdsConfig : ScriptableObject
    {
        public float timeFlyBetweenAds = 60f;
        public int countRestartsBetweenAds = 5;
        [Range(0f, 1f)] public float chanceNoAdsPopup = 0.15f;
    }
}