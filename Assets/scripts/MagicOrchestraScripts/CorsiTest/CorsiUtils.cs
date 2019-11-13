using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorsiUtils
{
    public const float panelTimeFocus = 4.0f;
    public const float panelTimeAfterFocus = 2.0f;

    /* <summary>
     * The function create a new Material from a given (light one) with a given new color
     * </summary>
     */
    public static void CreateMaterialFromColor (Material material, string color) 
     {
        //Converting the string
        //Escaping the intial char
        if (color.IndexOf('#') != -1)
            color = color.Replace("#", "");

        //Retriving RGB
        int r, g, b = 0;
        r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
        g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
        b = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

        material.color = new Color(r,g,b);
        material.SetColor("_EMISSION", material.color);

    }

    /* <summary>
     * The function changes the material of a cube in order to "turn on" the light
     * </summary>
     */
    public static void ShowLightOnCube(GameObject cube, Material lightMaterial)
    {
        cube.GetComponent<Renderer>().material = lightMaterial;
    }

    /* <summary>
     * The function changes the material of a cube in order to "turn off" the light
     * </summary>
     */
    public static void RestoreIntialCube(GameObject cube, Material defaultMaterial)
    {
        cube.GetComponent<Renderer>().material = defaultMaterial;
    }
}
