using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorsiUtils
{
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

    public static void ShowLightOnCube(GameObject cube, Material lightMaterial)
    {
        cube.GetComponent<Renderer>().material = lightMaterial;
    }

    public static void RestoreIntialCube(GameObject cube, Material defaultMaterial)
    {
        cube.GetComponent<Renderer>().material = defaultMaterial;
    }
}
