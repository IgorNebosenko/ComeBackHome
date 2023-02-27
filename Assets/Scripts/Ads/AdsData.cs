namespace CBH.Ads
{
    public class AdsData
    {
        public bool hasNoAds;
        public float timeFlyFromLastAd;
        public int countRestartsFromLastAd;

        public void Reset()
        {
            timeFlyFromLastAd = 0f;
            countRestartsFromLastAd = 0;
        }
    }
}