using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    private Button loadDemoButton;

    private PointOfInterest data;

    private int currentPhotoIndex;

    private SessionInformation session;

    private IList<Texture2D> imagesInMemory;

    private static readonly PointOfInterest DemoData = new PointOfInterest {
        Name = "Z6 Dining Hall",
        Description = "Z6 Dining Hall is open Monday - Friday, and provides a lot of dining options." +
                      "It is beloved by many students due to it's quality food and friendly service. " +
                      "It is most famed for it's Fried Chicken Wednesdays. " +
                      "Additionally, it has a piano on the ground floor that is very fun to play.",
        ImageLinks = { "Photos/Z6DiningHall/Z6-1.png", "Photos/Z6DiningHall/Z6-2.png", "Photos/Z6DiningHall/Z6-3.png", "Photos/Z6DiningHall/Z6-4.png" }

    };

    // Start is called before the first frame update
    void Start()
    {
        this.loadDemoButton.onClick.AddListener(this.LoadDemoData);
        this.imagesInMemory = new List<Texture2D>();
        this.currentPhotoIndex = 0;
        this.nextButton.onClick.AddListener(this.HandleNext);
        this.previousButton.onClick.AddListener(this.HandlePrevious);
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

    private void LoadDemoData()
    {
        this.data = DemoData;
        this.LoadInformation();
    }

    private void LoadInformation()
    {
        this.poiName.text = this.data.Name;
        this.poiDescription.text = this.data.Description;
        foreach (string address in this.data.ImageLinks)
        {
            string filePath = $"{Application.streamingAssetsPath}/{address}";

            if (File.Exists(filePath))
            {
                var texture = new Texture2D(2, 2);

                byte[] file = File.ReadAllBytes(filePath);
                texture.LoadImage(file);
                this.imagesInMemory.Add(texture);
            }
        }

        this.SetCurrentImage();
    }

    private void SetCurrentImage()
    {
        this.currentImage.texture = this.imagesInMemory[this.currentPhotoIndex];
    }
}
