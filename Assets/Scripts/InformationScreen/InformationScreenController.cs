using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
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

    [SerializeField]
    private TextMeshProUGUI hasScanTargetText;

    [SerializeField] 
    private Button playButton;

    [SerializeField]
    private Button stopButton;

    [SerializeField]
    private Button pauseButton;

    [SerializeField] 
    private AudioClip oldAcademicBuildingAudio;

    [SerializeField]
    private AudioClip melsonHallAudio;

    private PointOfInterest data;

    private int currentPhotoIndex;

    private SessionInformation session;

    private IList<Texture2D> imagesInMemory;

    [SerializeField]
    private AudioSource audio;

    private IDictionary<string, AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        this.audio.Stop();
        this.imagesInMemory = new List<Texture2D>();
        this.currentPhotoIndex = 0;
        this.nextButton.onClick.AddListener(this.HandleNext);
        this.previousButton.onClick.AddListener(this.HandlePrevious);
        this.backButton.onClick.AddListener(this.ReturnToPreviousScreen);
        this.playButton.onClick.AddListener(this.PlayAudio);
        this.stopButton.onClick.AddListener(this.StopAudio);
        this.pauseButton.onClick.AddListener(this.PauseAudio);
        this.session = SessionInformation.GetInstance();
        this.data = this.session.CurrentPointOfInterest;
        this.LoadInformation();
        this.SetupAudioForScreen();


    }

    private void SetupAudioForScreen()
    {
        this.audioClips = new Dictionary<string, AudioClip> {
            { "Old Academic Building", this.oldAcademicBuildingAudio },
            { "Melson Hall", this.melsonHallAudio }
        };

        if (this.audioClips.ContainsKey(this.data.Name))
        {
            this.audio.clip = this.audioClips[this.data.Name];
        }
        else
        {
            this.playButton.gameObject.SetActive(false);
            this.stopButton.gameObject.SetActive(false);
            this.pauseButton.gameObject.SetActive(false);
        }
    }

    private void PauseAudio()
    {
        if (this.audio.isPlaying)
        {
            this.audio.Pause();
        }
    }

    private void StopAudio()
    {
        if (this.audio.isPlaying)
        {
            this.audio.Stop();
        }
    }

    private void PlayAudio()
    {
        if (!this.audio.isPlaying)
        {
            this.audio.Play();
        }
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
        this.hasScanTargetText.enabled = this.data.HasARTarget;
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
