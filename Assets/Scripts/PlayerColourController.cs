using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColourController : MonoBehaviour
{
    [Header("Renderers")]
    public SpriteRenderer baseRenderer;
    public SpriteRenderer hairRenderer;
    public SpriteRenderer bootsRenderer;
    public SpriteRenderer pantsRenderer;
    public SpriteRenderer jacketRenderer;

    // Set colours to the renderers
    public void SetColours(PlayerColourData colours)
    {
        if (baseRenderer != null) baseRenderer.color = colours.BaseColour;
        if (hairRenderer != null) hairRenderer.color = colours.HairColour;
        if (bootsRenderer != null) bootsRenderer.color = colours.BootsColour;
        if (pantsRenderer != null) pantsRenderer.color = colours.PantsColour;
        if (jacketRenderer != null) jacketRenderer.color = colours.JacketColour;
    }

    // Get the current colours from the renderers, using PlayerColour.DefaultColours for defaults
    public PlayerColourData GetColours()
    {
        return new PlayerColourData(
            baseRenderer != null ? baseRenderer.color : PlayerColourData.DefaultColours.BaseColour,
            hairRenderer != null ? hairRenderer.color : PlayerColourData.DefaultColours.HairColour,
            bootsRenderer != null ? bootsRenderer.color : PlayerColourData.DefaultColours.BootsColour,
            pantsRenderer != null ? pantsRenderer.color : PlayerColourData.DefaultColours.PantsColour,
            jacketRenderer != null ? jacketRenderer.color : PlayerColourData.DefaultColours.JacketColour
        );
    }
}
