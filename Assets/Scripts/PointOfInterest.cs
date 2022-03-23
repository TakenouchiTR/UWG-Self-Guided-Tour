using System;
using System.Collections.Generic;

/// <summary>
///     Stores data representing a point of interest, or a location that a user may find interesting.<br />
///     <br />
///     Author: Shawn Carter<br />
///     Version: Sprint 2022
/// </summary>
public class PointOfInterest
{
    private string name;
    private string previewDescription;
    private string description;
    private List<string> imageLinks;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    /// <exception cref="System.ArgumentNullException">Name must not be null.</exception>
    public string Name 
    {
        get => this.name;
        set => this.name = value ?? throw new ArgumentNullException("Name must not be null.");
    }

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
    ///     Gets or sets a value indicating whether this instance has AR target.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance has ar target; otherwise, <c>false</c>.
    /// </value>
    public bool HasARTarget { get; set; }

    /// <summary>
    ///     Gets or sets the preview description.
    /// </summary>
    /// <value>
    ///     The preview description.
    /// </value>
    /// <exception cref="System.ArgumentNullException">PreviewDescription must not be null.</exception>
    public string PreviewDescription
    {
        get => this.previewDescription;
        set => this.previewDescription = value ?? throw new ArgumentNullException("PreviewDescription must not be null.");
    }

    /// <summary>
    ///     Gets or sets the description.
    /// </summary>
    /// <value>
    ///     The description.
    /// </value>
    /// <exception cref="System.ArgumentNullException">Description must not be null.</exception>
    public string Description
    {
        get => this.description;
        set => this.description = value ?? throw new ArgumentNullException("Description must not be null.");
    }

    /// <summary>
    ///     Gets or sets the list of image links.
    /// </summary>
    /// <value>
    ///     The image links.
    /// </value>
    /// <exception cref="System.ArgumentNullException">ImageLinks must not be null.</exception>
    public List<string> ImageLinks
    {
        get => this.imageLinks;
        set => this.imageLinks = value ?? throw new ArgumentNullException("ImageLinks must not be null.");
    }

    /// <summary>
    ///     Gets the image links as a string, joined by commas.
    /// </summary>
    /// <value>
    ///     The joined image links.
    /// </value>
    public string JoinedImageLinks => string.Join(",", imageLinks);

    /// <summary>
    ///     Initializes a new instance of the <see cref="PointOfInterest"/> class.<br />
    ///     <br />
    ///     Precondition: None<br />
    ///     Postcondition:<br />
    ///     this.Name == string.Empty &amp;&amp;<br />
    ///     this.Latitude == 0 &amp;&amp;<br />
    ///     this.Longitude == 0 &amp;&amp;<br />
    ///     this.HasARTarget == false &amp;&amp;<br />
    ///     this.PreviewDescription == string.Empty &amp;&amp;<br />
    ///     this.Description == string.Empty &amp;&amp;<br />
    ///     this.ImageLinks.isEmpty()
    /// </summary>
    public PointOfInterest()
    {
        this.name = "";
        this.previewDescription = "";
        this.description = "";
        this.ImageLinks = new List<string>();
    }
}
