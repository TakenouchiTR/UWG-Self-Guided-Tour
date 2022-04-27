/// <summary>
///     Stores a Latitude-Longitude pair.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public struct Coordinate
{
    /// <summary>
    ///     Gets or sets the latitude.
    /// </summary>
    /// <value>
    ///     The latitude.
    /// </value>
    public double Latitude { get; set; }

    /// <summary>
    ///     Gets or sets the longitude.
    /// </summary>
    /// <value>
    ///     The longitude.
    /// </value>
    public double Longitude { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Coordinate"/> struct with a specified latitude and longitude.<br />
    ///     <br />
    ///     Precondition: None<br />
    ///     Postcondition: <br />
    ///     this.Latitude == latitude &amp;&amp;<br />
    ///     this.Longitude == longitude
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    public Coordinate(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}
