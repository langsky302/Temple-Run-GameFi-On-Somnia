using UnityEngine;
using System.Collections;

namespace SgLib
{
    public class InAppPurchaser : MonoBehaviour
    {
        public static InAppPurchaser Instance { get; private set; }

        [System.Serializable]
        public struct CoinPack
        {
            public string productName;
            public string priceString;
            public int coinValue;
        }

        [Header("Name of Remove Ads products")]
        public string removeAds = "Remove_Ads";

        [Header("Name of coin pack products")]
        public CoinPack[] coinPacks;

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

