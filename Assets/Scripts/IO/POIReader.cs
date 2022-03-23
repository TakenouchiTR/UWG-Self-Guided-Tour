using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class POIReader
{
    public static List<PointOfInterest> ReadFile(string fileLocation)
    {
        if (!File.Exists(fileLocation))
        {
            throw new FileNotFoundException($"{fileLocation} not found");
        }

        using BinaryReader reader = new BinaryReader(new FileStream(fileLocation, FileMode.Open));
        int version = reader.ReadInt32();
        reader.Close();

        List<PointOfInterest> result = null;
        switch (version)
        {
            case 1:
                result = ReadVersion1File(fileLocation);
                break;
        }

        return result;
    }

    private static List<PointOfInterest> ReadVersion1File(string fileLocation)
    {
        List<PointOfInterest> result = new List<PointOfInterest>();

        using BinaryReader reader = new BinaryReader(new FileStream(fileLocation, FileMode.Open));
        //Skip past the version number
        reader.ReadInt32();

        int poiCount = reader.ReadInt32();
        for (int i = 0; i < poiCount; i++)
        {
            PointOfInterest poi = new PointOfInterest();
            poi.Name = reader.ReadString();
            poi.Latitude = reader.ReadDouble();
            poi.Longitude = reader.ReadDouble();
            poi.HasARTarget = reader.ReadBoolean();
            poi.Description = reader.ReadString();
            poi.PreviewDescription = reader.ReadString();

            int imageCount = reader.ReadInt32();
            for (int j = 0; j < imageCount; j++)
            {
                poi.ImageLinks.Add(reader.ReadString());
            }

            result.Add(poi);
        }

        return result;
    }
}
