using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
///     Handles all functionality regarding the information screen, including loading and image scrolling.<br />
///     <br />
///     Author: Alexander Ayers<br />
///     Version: Spring 2022
/// </summary>
public class InformationScreenController : MonoBehaviour
{
    [SerializeField]
    private RawImage currentImage;

    [SerializeField] 
    private Button nextButton;

    [SerializeField]
    private Button previousButton;

    [SerializeField] 
    private TextMeshProUGUI poiName;

    [SerializeField] 
    private TextMeshProUGUI poiDescription;

    [SerializeField]
    private Button backButton;

    private PointOfInterest data;

    private int currentPhotoIndex;

    private SessionInformation session;

    private IList<Texture2D> imagesInMemory;

    // Start is called before the first frame update
    void Start()
    {
        this.imagesInMemory = new List<Texture2D>();
        this.currentPhotoIndex = 0;
        this.nextButton.onClick.AddListener(this.HandleNext);
        this.previousButton.onClick.AddListener(this.HandlePrevious);
        this.backButton.onClick.AddListener(this.ReturnToPreviousScreen);
        this.session = SessionInformation.GetInstance();
        this.data = this.session.CurrentPointOfInterest;
        this.LoadInformation();
    }

    private void HandlePrevious()
    {
        this.currentPhotoIndex--;
        if (this.currentPhotoIndex < 0)
        {
            this.currentPhotoIndex = this.imagesInMemory.Count - 1;
        }
        this.SetCurrentImage();
    }

    private void HandleNext()
    {
        this.currentPhotoIndex++;
        if (this.currentPhotoIndex >= this.imagesInMemory.Count)
        {
            this.currentPhotoIndex = 0;
        }
        this.SetCurrentImage();
    }

    private void LoadInformation()
    {
        this.poiName.text = this.data.Name;
        this.poiDescription.text = this.data.Description;
        foreach (string address in this.data.ImageLinks)
        {
#if UNITY_ANDROID
            if(BetterStreamingAssets.FileExists(address))
            {
                var texture = new Texture2D(2, 2);

                byte[] file = BetterStreamingAssets.ReadAllBytes(address);
                texture.LoadImage(file);
                this.imagesInMemory.Add(texture);
            }
#else
            string filePath = $"{Application.streamingAssetsPath}/{address}";

            if (File.Exists(filePath))
            {
                var texture = new Texture2D(2, 2);

                byte[] file = File.ReadAllBytes(filePath);
                texture.LoadImage(file);
                this.imagesInMemory.Add(texture);
            }
#endif
        }

        this.SetCurrentImage();
    }

    private void SetCurrentImage()
    {
        this.currentImage.texture = this.imagesInMemory[this.currentPhotoIndex];
    }

    private void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(0);
    }
}
