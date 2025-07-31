using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SgLib
{
    public class RatingRequester : MonoBehaviour
    {
        public enum RequestMode
        {
            GameBased,
            TimeBased

        }

        [Header("Select rating request mode")]
        public RequestMode requestMode;

        [Header("Game-based rating request settings")]
        [Range(3, 500)]
        public int gamesPlayedAfterInstall = 2;
        [Range(3, 500)]
        public int gamesPlayedBetweenRequests = 10;

        [Header("Time-based rating request settings")]
        [Range(3, 300)]
        public int daysAfterInstall = 14;
        [Range(3, 300)]
        public int daysBetweenRequests = 14;
    }
}
