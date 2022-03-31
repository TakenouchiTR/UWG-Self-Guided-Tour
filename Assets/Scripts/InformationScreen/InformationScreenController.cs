using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InformationScreenController : MonoBehaviour
{
    [SerializeField]
    private Image currentImage;

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

    private IList<Sprite> imagesInMemory;

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
        this.imagesInMemory = new List<Sprite>();
    }

    private void loadDemoData()
    {
        this.data = DemoData;
        this.poiName.text = this.data.Name;
        this.poiDescription.text = this.data.Description;
        foreach (var address in this.data.ImageLinks)
        {
            Texture2D texture = new Texture2D(2, 2);
            this.imagesInMemory.Add(texture);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
