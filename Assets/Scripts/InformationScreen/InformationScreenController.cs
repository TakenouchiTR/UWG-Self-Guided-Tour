using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
        this.loadDemoButton.onClick.AddListener(this.loadDemoData);
        this.imagesInMemory = new List<Texture2D>();
        this.currentPhotoIndex = 0;
        this.nextButton.onClick.AddListener(this.handleNext);
        this.previousButton.onClick.AddListener(this.handlePrevious);
    }

    private void handlePrevious()
    {
        this.currentPhotoIndex--;
        if (this.currentPhotoIndex < 0)
        {
            this.currentPhotoIndex = this.imagesInMemory.Count - 1;
        }
        this.setCurrentImage();
    }

    private void handleNext()
    {
        this.currentPhotoIndex++;
        if (this.currentPhotoIndex >= this.imagesInMemory.Count)
        {
            this.currentPhotoIndex = 0;
        }
        this.setCurrentImage();
    }

    private void loadDemoData()
    {
        this.data = DemoData;
        this.poiName.text = this.data.Name;
        this.poiDescription.text = this.data.Description;
        foreach (var address in this.data.ImageLinks)
        {
            String filePath = $"{Application.streamingAssetsPath}/{address}";

            if (File.Exists(filePath))
            {
                var texture = new Texture2D(2, 2);

                byte[] file = File.ReadAllBytes(filePath);
                texture.LoadImage(file);
                this.imagesInMemory.Add(texture);
            }
        }

        this.setCurrentImage();

    }

    private void setCurrentImage()
    {
        this.currentImage.texture = this.imagesInMemory[this.currentPhotoIndex];
    }
}
