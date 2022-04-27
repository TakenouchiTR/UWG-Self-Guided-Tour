using UnityEngine;

/// <summary>
///     Represents a rectangular area of the Earth, constructed by start and end <see cref="Coordinate"/>s.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public struct Map
{
    /// <summary>
    ///     Gets or sets the start coordinate, representing the top latitude and the left longitude
    /// </summary>
    /// <value>
    ///     The start coordinate.
    /// </value>
    public Coordinate StartCoordinate { get; set; }

    /// <summary>
    ///     Gets or sets the start coordinate, representing the bottom latitude and the right longitude
    /// </summary>
    /// <value>
    ///     The end coordinate.
    /// </value>
    public Coordinate EndCoordinate { get; set; }

    /// <summary>
    ///     Gets the width.
    /// </summary>
    /// <value>
    ///     The width.
    /// </value>
    public double Width => this.EndCoordinate.Longitude - this.StartCoordinate.Longitude;

    /// <summary>
    ///     Gets the height.
    /// </summary>
    /// <value>
    ///     The height.
    /// </value>
    public double Height => this.StartCoordinate.Latitude - this.EndCoordinate.Latitude;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Map"/> struct with specified start and end <see cref="Coordinate"/>s.<br />
    ///     <br />
    ///     Precondition: None<br />
    ///     Postcondition:<br />
    ///     this.StartCoordinate == startCoordniate &amp;&amp;
    ///     this.EndCoordinate == endCoordniate
    /// </summary>
    /// <param name="startCoordinate">The start coordinate.</param>
    /// <param name="endCoordinate">The end coordinate.</param>
    public Map(Coordinate startCoordinate, Coordinate endCoordinate)
    {
        this.StartCoordinate = startCoordinate;
        this.EndCoordinate = endCoordinate;
    }

    /// <summary>
    ///     Gets a <see cref="Vector2"/> representing the position of a given coordniate in the map as percentages relative<br />
    ///     to the start and end positions.<br />
    ///     <br />
    ///     ie: a coordniate that is in the middle of the map would return (.5f, .5f).<br />
    ///     <br />
    ///     Precondition: None<br />
    ///     Postcondition: None
    /// </summary>
    /// <param name="coord">The coord.</param>
    /// <returns>A <see cref="Vector2"/> representing the relative position of the coordinate as percentages.</returns>
    public Vector2 GetPositionInMap(Coordinate coord)
    {
        Vector2 result = new Vector2();

        result.x = (float)((coord.Longitude - this.StartCoordinate.Longitude) / this.Width);
        result.y = (float)((this.StartCoordinate.Latitude - coord.Latitude) / this.Height);

        return result;
    }
}
