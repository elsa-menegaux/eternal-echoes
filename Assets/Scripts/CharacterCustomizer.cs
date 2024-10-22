using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    [Header("Player Sprite Controller")]
    public PlayerColourController playerColourController;
    [Header("UI Controllers")]
    public RGBControler hairController;
    public RGBControler baseController;
    public RGBControler jacketController;
    public RGBControler pantsController;
    public RGBControler bootsController;

    private PlayerColourData playerColourData;

    [Header("Other")]
    public bool loadDefaults = true;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (loadDefaults)
        {
            playerColourData = PlayerColourData.DefaultColours;
            hairController.Colour = playerColourData.HairColour;
            baseController.Colour = playerColourData.BaseColour;
            jacketController.Colour = playerColourData.JacketColour;
            pantsController.Colour = playerColourData.PantsColour;
            bootsController.Colour = playerColourData.BootsColour;
        }
    }

    private void Update()
    {
        playerColourData = new PlayerColourData(baseController.Colour, 
            hairController.Colour, 
            bootsController.Colour, 
            pantsController.Colour, 
            jacketController.Colour);

        playerColourController.SetColours(playerColourData);
    }
}
