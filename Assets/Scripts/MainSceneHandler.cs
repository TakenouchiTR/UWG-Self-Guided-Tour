using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Android;

/// <summary>
///     Controls interactions within the MainScene.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public class MainSceneHandler : MonoBehaviour
{
    private Map uwgMap;
    private MapMarker userMarker;
    private SessionInformation sessionInformation;

    [SerializeField]
    private MapMarker mapMarkerPrefab;
    [SerializeField]
    private RawImage raw_Map;
    [SerializeField]
    private TextMeshProUGUI txt_Name;
    [SerializeField]
    private TextMeshProUGUI txt_Preview;
    [SerializeField]
    private Button btn_MoreInfo;
    [SerializeField]
    private PoiContentPanel contentPanel;
    [SerializeField]
    private string filePath;
    [SerializeField]
    private ScrollableMap scrollableMap;

    private void Start()
    {
        this.sessionInformation = SessionInformation.GetInstance();

        this.InitializeMap();
        try
        {
            if (!this.sessionInformation.LoadedFile)
            {
                BetterStreamingAssets.Initialize();
                this.sessionInformation.LoadPointsOfInterest(this.filePath);
            }
        }
        catch (Exception e)
        {
            this.txt_Preview.text = e.Message;
            Debug.LogError(e.StackTrace);
        }

        this.PlacePointsOfInterest();

        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(RunLocationService());
        }
        else
        {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            Permission.RequestUserPermission(Permission.FineLocation, callbacks);
        }
    }

    private void InitializeMap()
    {
        Coordinate startCoordinate = new Coordinate(33.575852881258044, -85.10966455008551);
        Coordinate endCoordinate = new Coordinate(33.56894868560781, -85.09343717215947);
        this.uwgMap = new Map(startCoordinate, endCoordinate);
    }

    private void PlacePointsOfInterest()
    {
        foreach (PointOfInterest poi in this.sessionInformation.PointsOfInterest)
        {
            this.PlacePoi(poi);
        }
    }

    private void PlaceUser()
    {
        Vector2 mapPosition = Vector2.zero;
        this.userMarker = Instantiate(mapMarkerPrefab, raw_Map.transform);

        mapPosition.x *= raw_Map.rectTransform.rect.width;
        mapPosition.y *= -raw_Map.rectTransform.rect.height;

        this.userMarker.Color = Color.blue;

        this.userMarker.MoveToPosition(mapPosition);
    }

    private void PlacePoi(PointOfInterest poi)
    {
        Vector2 mapPosition = uwgMap.GetPositionInMap(poi.Coords);
        MapMarker instance = Instantiate(mapMarkerPrefab, raw_Map.transform);

        instance.PoI = poi;
        mapPosition.x *= raw_Map.rectTransform.rect.width;
        mapPosition.y *= -raw_Map.rectTransform.rect.height;

        instance.MoveToPosition(mapPosition);
        instance.Tapped.AddListener(this.OnMapMarkerTapped);
    }

    private void UpdateUI(PointOfInterest poi)
    {
        this.txt_Name.text = poi.Name;
        this.txt_Preview.text = poi.PreviewDescription;
    }

    private IEnumerator RunLocationService()
    {
        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }

        this.PlaceUser();

        while (true)
        {
            this.UpdateUserPosition();
            yield return new WaitForSeconds(1);
        }
    }

    private void PermissionCallbacks_PermissionGranted(string obj)
    {
        StartCoroutine(this.RunLocationService());
    }

    private void UpdateUserPosition()
    {
        Coordinate curCoordinates = new Coordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Vector2 mapPosition = this.uwgMap.GetPositionInMap(curCoordinates);

        mapPosition.x *= raw_Map.rectTransform.rect.width;
        mapPosition.y *= -raw_Map.rectTransform.rect.height;

        this.userMarker.MoveToPosition(mapPosition);
    }

    private void OnMapMarkerTapped(PointOfInterest poi)
    {
        this.sessionInformation.CurrentPointOfInterest = poi;
        this.contentPanel.DisplayPanel();
        this.UpdateUI(poi);
    }

    public void OnMoreInfoPressed()
    {
        if (this.sessionInformation.CurrentPointOfInterest == null)
        {
            Debug.Log("Point of Interest not selected");
            return;
        }
        SceneManager.LoadScene(1);
    }

    public void OnMapTapped()
    {
        this.contentPanel.HidePanel();
    }
}
