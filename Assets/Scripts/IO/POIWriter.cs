using System.Collections.Generic;
using System.IO;

/// <summary>
///     Writes a list of <see cref="PointOfInterest"/> to a specified file location.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Spring 2022
/// </summary>
public static class POIWriter
{
    /// <summary>
    ///     The current file version.
    /// </summary>
    public const int Version = 1;

    /// <summary>
    ///     Writes a list of <see cref="PointOfInterest"/> to a specified file location.<br />
    ///     <br />
    ///     Precondition: None<br />
    ///     Postcondition: None
    /// </summary>
    /// <param name="fileLocation">The file location.</param>
    /// <param name="pointsOfInterest">The points of interest.</param>
    public static void WriteFile(string fileLocation, List<PointOfInterest> pointsOfInterest)
    {
        using BinaryWriter writer = new BinaryWriter(new FileStream(fileLocation, FileMode.Create));

        writer.Write(Version);
        writer.Write(pointsOfInterest.Count);
        foreach (PointOfInterest poi in pointsOfInterest)
        {
            writer.Write(poi.Name);
            writer.Write(poi.Latitude);
            writer.Write(poi.Longitude);
            writer.Write(poi.HasARTarget);
            writer.Write(poi.Description);
            writer.Write(poi.PreviewDescription);
            writer.Write(poi.ImageLinks.Count);
            foreach (string imageLink in poi.ImageLinks)
            {
                writer.Write(imageLink);
            }
        }
    }
}
