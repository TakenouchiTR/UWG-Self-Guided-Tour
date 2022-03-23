using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest
{
    private string name;
    private string previewDescription;
    private string description;
    private List<string> imageLinks;

    public string Name 
    {
        get => this.name;
        set => this.name = value ?? throw new ArgumentNullException("Name must not be null.");
    }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public bool HasARTarget { get; set; }

    public string PreviewDescription
    {
        get => this.previewDescription;
        set => this.previewDescription = value ?? throw new ArgumentNullException("PreviewDescription must not be null.");
    }

    public string Description
    {
        get => this.description;
        set => this.description = value ?? throw new ArgumentNullException("Description must not be null.");
    }

    public List<string> ImageLinks
    {
        get => this.imageLinks;
        set => this.imageLinks = value ?? throw new ArgumentNullException("ImageLinks must not be null.");
    }

    public string JoinedImageLinks => string.Join(",", imageLinks);

    public PointOfInterest()
    {
        this.name = "";
        this.previewDescription = "";
        this.description = "";
        this.ImageLinks = new List<string>();
    }
}
