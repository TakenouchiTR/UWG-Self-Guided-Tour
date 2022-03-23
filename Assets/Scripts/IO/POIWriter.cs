using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class POIWriter
{
    public const int Version = 1;

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
