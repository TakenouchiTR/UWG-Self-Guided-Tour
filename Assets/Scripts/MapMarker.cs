using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///     Displays an interactable map marker on a map.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public class MapMarker : MonoBehaviour
{
    [SerializeField]
    private Image img_Marker;

    /// <summary>
    ///     Gets or sets the <see cref="PointOfInterest"/>.
    /// </summary>
    /// <value>
    ///     The <see cref="PointOfInterest"/>.
    /// </value>
    public PointOfInterest PoI { get; set; }

    /// <summary>
    ///     Fires when the point of interest is tapped.
    /// </summary>
    public UnityEvent<PointOfInterest> Tapped;

    /// <summary>
    ///     Moves the marker's image to the specified position.
    /// </summary>
    /// <param name="newPosition">The new position.</param>
    public void MoveToPosition(Vector2 newPosition)
    {
        this.img_Marker.rectTransform.anchoredPosition = newPosition;
    }

    public void OnPointerClick()
    {
        this.Tapped?.Invoke(this.PoI);
    }
}
