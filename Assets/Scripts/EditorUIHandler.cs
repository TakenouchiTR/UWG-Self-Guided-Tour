using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using SimpleFileBrowser;

public class EditorUIHandler : MonoBehaviour
{
    private int currentIndex;
    private List<PointOfInterest> pointsOfInterest;

    [SerializeField]
    private TMP_InputField inp_currentPoi;
    [SerializeField]
    private TextMeshProUGUI txt_totalPois;
    [SerializeField]
    private TMP_InputField inp_PoiName;
    [SerializeField]
    private TMP_InputField inp_latitude;
    [SerializeField]
    private TMP_InputField inp_longitude;
    [SerializeField]
    private Toggle tog_hasARTarget;
    [SerializeField]
    private TMP_InputField inp_description;
    [SerializeField]
    private TMP_InputField inp_previewDescription;
    [SerializeField]
    private TMP_InputField inp_pictures;

    private PointOfInterest CurrentPointOfInterest => pointsOfInterest[currentIndex];
    private int PointOfInterestCount => this.pointsOfInterest.Count;

    void Start()
    {
        this.pointsOfInterest = new List<PointOfInterest>();
        this.pointsOfInterest.Add(new PointOfInterest());
        UpdateUI();
    }

    private void UpdateUI()
    {
        this.inp_currentPoi.text = (this.currentIndex + 1).ToString();
        this.txt_totalPois.text = "/" + this.PointOfInterestCount;
        this.inp_PoiName.text = this.CurrentPointOfInterest.Name;
        this.inp_latitude.text = this.CurrentPointOfInterest.Latitude.ToString();
        this.inp_longitude.text = this.CurrentPointOfInterest.Longitude.ToString();
        this.tog_hasARTarget.isOn = this.CurrentPointOfInterest.HasARTarget;
        this.inp_description.text = this.CurrentPointOfInterest.Description;
        this.inp_previewDescription.text = this.CurrentPointOfInterest.PreviewDescription;
        this.inp_pictures.text = this.CurrentPointOfInterest.JoinedImageLinks;
    }

    private List<string> GetImageLocationsFromTextbox()
    {
        char[] separators = new char[] { ',' };
        string picturesText = this.inp_pictures.text;

        picturesText = picturesText.Replace('\n', ',');
        string[] imageLocations = picturesText.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

        return imageLocations.ToList();
    }

    private void ChangeCurrentIndex(int index)
    {
        if (index < 0 || index >= this.PointOfInterestCount)
        {
            this.inp_currentPoi.text = (this.currentIndex + 1).ToString();
            return;
        }

        this.currentIndex = index;
        UpdateUI();
    }

    /// <summary>
    ///     Displays the file browser to select a save location. If one is selected, the POI file will be saved.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/> for running the coroutine.</returns>
    IEnumerator SaveFileCoroutine()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            POIWriter.WriteFile(FileBrowser.Result[0], this.pointsOfInterest);
        }
    }

    /// <summary>
    ///     Displays the file browser to select a file to load. If one is selected, the file will be loaded.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/> for running the coroutine.</returns>
    IEnumerator LoadFileCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            this.currentIndex = 0;
            this.pointsOfInterest = POIReader.ReadFile(FileBrowser.Result[0]);

            this.UpdateUI();
        }
    }

    public void OnAddClicked()
    {
        this.pointsOfInterest.Add(new PointOfInterest());
        this.ChangeCurrentIndex(this.PointOfInterestCount - 1);
    }

    public void OnRemoveClicked()
    {
        if (this.PointOfInterestCount == 1)
        {
            return;
        }

        this.pointsOfInterest.RemoveAt(currentIndex);
        if (this.currentIndex == this.PointOfInterestCount)
        {
            this.currentIndex--;
        }

        this.UpdateUI();
    }

    public void OnSaveClicked()
    {
        base.StartCoroutine(this.SaveFileCoroutine());
    }

    public void OnLoadClicked()
    {
        base.StartCoroutine(this.LoadFileCoroutine());
    }

    public void OnPoiInfoUpdated()
    {
        this.CurrentPointOfInterest.Name = this.inp_PoiName.text;
        this.CurrentPointOfInterest.Latitude = double.Parse(this.inp_latitude.text);
        this.CurrentPointOfInterest.Longitude = double.Parse(this.inp_longitude.text);
        this.CurrentPointOfInterest.HasARTarget = this.tog_hasARTarget.isOn;
        this.CurrentPointOfInterest.Description = this.inp_description.text;
        this.CurrentPointOfInterest.PreviewDescription = this.inp_previewDescription.text;
        this.CurrentPointOfInterest.ImageLinks = this.GetImageLocationsFromTextbox();
    }

    public void OnCurrentPoiEndEdit(string text)
    {
        this.ChangeCurrentIndex(int.Parse(text) - 1);
    }
}
