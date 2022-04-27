using System;
using System.Collections.Generic;


/// <summary>
///     Stores information about the current session using a singleton.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public class SessionInformation
{
    private static SessionInformation instance;

    /// <summary>
    ///     Gets the points of interest.
    /// </summary>
    /// <value>
    ///     The points of interest.
    /// </value>
    public List<PointOfInterest> PointsOfInterest { get; private set; }

    /// <summary>
    ///     Gets the current point of interest.
    /// </summary>
    /// <value>
    ///     The current point of interest.
    /// </value>
    public PointOfInterest CurrentPointOfInterest { get; set; }

    public bool LoadedFile { get; private set; }

    public float LastMapPosition { get; set; }

    private SessionInformation()
    {
        this.PointsOfInterest = new List<PointOfInterest>();
    }

    /// <summary>
    ///     Gets the instance of <see cref="SessionInformation"/>.
    /// </summary>
    /// <returns>The single instance of <see cref="SessionInformation"/></returns>
    public static SessionInformation GetInstance()
    {
        if (instance == null)
        {
            instance = new SessionInformation();
        }
        return instance;
    }

    /// <summary>
    ///     Loads the points of interest from a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    public void LoadPointsOfInterest(string filePath)
    {
        this.PointsOfInterest = POIReader.ReadFile(filePath);
    }
}
