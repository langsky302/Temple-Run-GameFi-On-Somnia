using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SgLib;

public class ShareUIController : MonoBehaviour
{
    public enum ImageFormat
    {
        GIF,
        PNG
    }

    public enum ScaleMode
    {
        AutoHeight,
        AutoWidth
    }

    public ImageFormat ImageType
    {
        get { return _imageType; }
        private set { _imageType = value; }
    }

    public Texture2D ImgTex { get; set; }

    [Header("Object References")]
    public GameObject container;
    public GameObject modal;
    public GameObject mask;
    public Image staticImage;
    public GameObject noImageMsg;
    public GameObject clipPlayer;
    public GameObject noClipMsg;
    public GameObject toolbar;
    public GameObject statusbar;
    public Text statusText;
    public Image progressBar;
    public Button gifButton;
    public Button pngButton;
    public GameObject giphyLogo;

    [Header("Apperance Config")]
    public ScaleMode scaleMode = ScaleMode.AutoHeight;
    public Color buttonEnableColor = Color.white;
    public Color buttonDisableColor = Color.gray;
    [HideInInspector]
    public float clipStartDelay = 0.5f;

    RectTransform containerRT;
    Image gifButtonImage;
    Image pngButtonImage;

    ImageFormat _imageType = ImageFormat.PNG;

    void Awake()
    {
        gifButtonImage = gifButton.GetComponent<Image>();
        pngButtonImage = pngButton.GetComponent<Image>();
        containerRT = container.GetComponent<RectTransform>();
        staticImage.GetComponent<RectTransform>().sizeDelta = containerRT.sizeDelta;
        clipPlayer.GetComponent<RectTransform>().sizeDelta = containerRT.sizeDelta;

        modal.SetActive(false);
        noImageMsg.SetActive(false);
        noClipMsg.SetActive(false);
        toolbar.SetActive(true);
        statusbar.SetActive(false);
        giphyLogo.SetActive(false);
    }

    void OnEnable()
    {
        if (ImageType == ImageFormat.GIF)
        {
            LoadAnimatedClip(clipStartDelay);
        }
        else
        {
            LoadStaticImage();
        }
    }

    void Update()
    {

        gifButtonImage.color = ImageType == ImageFormat.GIF ? buttonEnableColor : buttonDisableColor;
        pngButtonImage.color = ImageType == ImageFormat.PNG ? buttonEnableColor : buttonDisableColor;
    }

    public void SwitchFormat()
    {
        ImageType = (ImageType == ImageFormat.GIF) ? ImageFormat.PNG : ImageFormat.GIF;

        if (ImageType == ImageFormat.GIF)
            LoadAnimatedClip(clipStartDelay);
        else
            LoadStaticImage();
    }

    public void Share()
    {
        if (ImageType == ImageFormat.PNG)
            SharePNG();
        else
            ShareGIF();
    }

    void SharePNG()
    {
        if (ImgTex == null)
        {
            Debug.Log("SharePNG failed: no captured screenshot.");
            return;
        }

        Debug.Log("Sharing feature requires Easy Mobile plugin.");
    }

    void ShareGIF()
    {
        Debug.Log("Sharing feature requires Easy Mobile Pro plugin.");
    }

    void ShowGifExportingProgress(float progress)
    {
    }

    void ShowGifUploadingProgress(float progress)
    {

    }

    void ShowToolbar()
    {
        toolbar.SetActive(true);
        statusbar.SetActive(false);
    }

    string ConstructShareMessage()
    {
        string msg = ScreenshotSharer.Instance.shareMessage;
        msg = msg.Replace("[score]", ScoreManager.Instance.Score.ToString());
        msg = msg.Replace("[AppName]", AppInfo.Instance.APP_NAME);
        msg = msg.Replace("[#AppName]", "#" + AppInfo.Instance.APP_NAME.Replace(" ", ""));

        return msg;
    }

    void LoadStaticImage()
    {


        if (ImgTex != null)
        {
            noImageMsg.SetActive(false);
            Sprite sprite = Sprite.Create(ImgTex, new Rect(0.0f, 0.0f, ImgTex.width, ImgTex.height), new Vector2(0.5f, 0.5f));
            Transform imgTf = staticImage.gameObject.transform;
            RectTransform imgRtf = staticImage.GetComponent<RectTransform>();
            float scaleFactor = 1;

            if (scaleMode == ScaleMode.AutoHeight)
                scaleFactor = imgRtf.rect.width / sprite.rect.width;
            else
                scaleFactor = imgRtf.rect.height / sprite.rect.height;

            staticImage.sprite = sprite;
            staticImage.SetNativeSize();
            imgTf.localScale = imgTf.localScale * scaleFactor;

            ScaleContainer(sprite.rect.width / sprite.rect.height);
        }
        else
        {
            noImageMsg.SetActive(true);
        }

        clipPlayer.SetActive(false);
        staticImage.gameObject.SetActive(true);
    }

    void LoadAnimatedClip(float playStartDelay = 0f)
    {
        StartCoroutine(CRLoadAnimatedClip(playStartDelay));
    }

    IEnumerator CRLoadAnimatedClip(float playStartDelay)
    {
        staticImage.gameObject.SetActive(false);
        clipPlayer.SetActive(true);
        yield return null;

        noClipMsg.SetActive(true);
    }

    void ScaleContainer(float aspect)
    {
        if (scaleMode == ScaleMode.AutoHeight)
        {
            float y = containerRT.sizeDelta.x / aspect;
            containerRT.sizeDelta = new Vector2(containerRT.sizeDelta.x, y);
        }
        else
        {
            float x = containerRT.sizeDelta.y * aspect;
            containerRT.sizeDelta = new Vector2(x, containerRT.sizeDelta.y);
        }
    }
}

