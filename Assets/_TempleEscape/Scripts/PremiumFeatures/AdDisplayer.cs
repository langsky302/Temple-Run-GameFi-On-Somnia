using UnityEngine;
using System.Collections;

namespace SgLib
{
    public class AdDisplayer : MonoBehaviour
    {
        

        public enum BannerAdPos
        {
            Top,
            Bottom
        }
        public static AdDisplayer Instance { get; private set; }

        [Header("Banner Ad Display Config")]
        [Tooltip("Whether or not to show banner ad")]
        public bool showBannerAd = true;
        public BannerAdPos bannerAdPosition = BannerAdPos.Bottom;

        [Header("Interstitial Ad Display Config")]
        [Tooltip("Whether or not to show interstitial ad")]
        public bool showInterstitialAd = true;
        [Tooltip("Show interstitial ad every [how many] games")]
        public int gamesPerInterstitial = 3;
        [Tooltip("How many seconds after game over that interstitial ad is shown")]
        public float showInterstitialDelay = 2f;

        [Header("Rewarded Ad Display Config")]
        [Tooltip("Check to allow watching ad to earn coins")]
        public bool watchAdToEarnCoins = true;
        [Tooltip("How many coins the user earns after watching a rewarded ad")]
        public int rewardedCoins = 50;

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
