using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///     Controls interactions within the MainScene.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public class MainSceneHandler : MonoBehaviour
{
    private Map uwgMap;

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

    private void Start()
    {
        this.InitializeMap();
        string filePath = $"{Application.streamingAssetsPath}/PointsOfInterest.poi";
        this.LoadFile(filePath);
    }

    private void InitializeMap()
    {
        Coordinate startCoordinate = new Coordinate(33.575852881258044, -85.10966455008551);
        Coordinate endCoordinate = new Coordinate(33.56894868560781, -85.09343717215947);
        this.uwgMap = new Map(startCoordinate, endCoordinate);
    }

    /// <summary>
    ///     Loads the file.<br />
    ///     Will be moved to the Singleton when it's implemented.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private void LoadFile(string filePath)
    {
        List<PointOfInterest> pois = POIReader.ReadFile(filePath);

        foreach (PointOfInterest poi in pois)
        {
            this.PlacePoi(poi);
        }
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

    private void OnMapMarkerTapped(PointOfInterest poi) 
    {
        this.UpdateUI(poi);
    }
}
