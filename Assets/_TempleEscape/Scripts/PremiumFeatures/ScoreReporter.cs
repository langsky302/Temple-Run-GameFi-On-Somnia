using UnityEngine;
using System.Collections;

namespace SgLib
{
    public class ScoreReporter : MonoBehaviour
    {
        [Header("Check to disable automatic score reporting")]
        public bool disable = false;

        [Header("Name of the leaderboard to report score as declared with EasyMobile")]
        public string leaderboardName = "Score";
    }
}