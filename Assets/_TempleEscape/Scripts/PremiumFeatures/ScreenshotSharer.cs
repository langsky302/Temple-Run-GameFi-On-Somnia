using UnityEngine;
using System.Collections;

namespace SgLib
{
    public class ScreenshotSharer : MonoBehaviour
    {
        public enum SharedImageType
        {
            PNG,
            GIF,
            Both
        }

        [Header("Check to disable sharing")]
        public bool disableSharing = false;

        [Header("Sharing Config")]
        public SharedImageType sharedImageFormat = SharedImageType.Both;
        [Tooltip("Any instances of [score] will be replaced by the actual score achieved in the last game, [AppName] will be replaced by the app name declared in AppInfo")]
        [TextArea(3, 3)]
        public string shareMessage = "Awesome! I've just scored [score] in [AppName]! [#AppName]";
        public string pngFilename = "screenshot";

        public static ScreenshotSharer Instance { get; private set; }

        public Texture2D CapturedScreenshot { get; private set; }

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

        void OnEnable()
        {
            GameManager.GameStateChanged += GameManager_GameStateChanged;
            PlayerController.PlayerDied += PlayerController_PlayerDied;
        }

        void OnDisable()
        {
            GameManager.GameStateChanged -= GameManager_GameStateChanged;
            PlayerController.PlayerDied -= PlayerController_PlayerDied;
        }

        void OnDestroy()
        {

        }

        void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
        }

        void PlayerController_PlayerDied()
        {
            if (PremiumFeaturesManager.Instance != null && PremiumFeaturesManager.Instance.enablePremiumFeatures && !disableSharing && (sharedImageFormat == SharedImageType.PNG || sharedImageFormat == SharedImageType.Both))
            {
                StartCoroutine(CRCaptureScreenshot());
            }
        }

        IEnumerator CRCaptureScreenshot()
        {
            // Wait for right timing to take screenshot
            yield return new WaitForEndOfFrame();

            if (CapturedScreenshot == null)
                CapturedScreenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            CapturedScreenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            CapturedScreenshot.Apply();
        }
    }
}
