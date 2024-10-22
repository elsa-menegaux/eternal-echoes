using System;
using UnityEngine;

[Serializable]
public class PlayerColourData
{
    // Private fields for encapsulation
    [SerializeField] private Color baseColour;
    [SerializeField] private Color hairColour;
    [SerializeField] private Color bootsColour;
    [SerializeField] private Color pantsColour;
    [SerializeField] private Color jacketColour;

    // Static field holding default colors
    public static readonly PlayerColourData DefaultColours = new PlayerColourData(
        new Color32(255,224,201,255),   // Default base color
        new Color32(123, 80, 0 ,255),   // Default hair color
        Color.gray,    // Default boots color
        Color.blue,    // Default pants color
        Color.red      // Default jacket color
    );

    // Parameterless constructor (uses default colours)
    public PlayerColourData() : this(DefaultColours.baseColour, DefaultColours.hairColour, DefaultColours.bootsColour, DefaultColours.pantsColour, DefaultColours.jacketColour)
    {
    }

    // Constructor with parameters
    public PlayerColourData(Color baseColour, Color hairColour, Color bootsColour, Color pantsColour, Color jacketColour)
    {
        this.baseColour = baseColour;
        this.hairColour = hairColour;
        this.bootsColour = bootsColour;
        this.pantsColour = pantsColour;
        this.jacketColour = jacketColour;
    }

    // Encapsulated properties
    public Color BaseColour
    {
        get { return baseColour; }
        set { baseColour = value; }
    }

    public Color HairColour
    {
        get { return hairColour; }
        set { hairColour = value; }
    }

    public Color BootsColour
    {
        get { return bootsColour; }
        set { bootsColour = value; }
    }

    public Color PantsColour
    {
        get { return pantsColour; }
        set { pantsColour = value; }
    }

    public Color JacketColour
    {
        get { return jacketColour; }
        set { jacketColour = value; }
    }
}
