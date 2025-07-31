using UnityEngine;
using System.Collections;

namespace SgLib
{
    public class AchievementUnlocker : MonoBehaviour
    {
        [System.Serializable]
        public struct ScoreAchievement
        {
            public string achievementName;
            public int scoreToUnlock;
        }

        [Header("Check to disable automatic achievement unlocking")]
        public bool disable = false;

        [Header("List of achievements to unlock")]
        public ScoreAchievement[] achievements;
    }
}
