using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBControler : MonoBehaviour
{
    private Color colour;
    [SerializeField]
    private Image colourImage;

    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

    public Color Colour
    {
        get { return colour; }
        set { 
            colour = value;
            rSlider.value = value.r;
            gSlider.value = value.g;
            bSlider.value = value.b;
        }
    }

    
    public void UpdateColour()
    {
        colour.r = rSlider.value;
        colour.g = gSlider.value;
        colour.b = bSlider.value;
        colour.a = 1;

        colourImage.color = colour;
    }

}
