using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

/* <summary>
 *  The class is used to serialize and deserialize the position of each instrument/object used in game 1.
 * </summary> */
static class ArenaObjectsRetriever
{
    public static void Write()
    {
        Dictionary<int, Vector3Ser> dictFirstPositions = GetFirstPositions();
        Dictionary<int, Vector3Ser> dictSecondPositions = GetSecondPositions();

        // Arraylist of two dictionaries of vectors.
        ArrayList dictPositions = new ArrayList();
        dictPositions.Add(dictFirstPositions);
        dictPositions.Add(dictSecondPositions);        

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream("Assets/scripts/MagicOrchestraScripts/Game1_InstruMaps/objectsPosition.txt", FileMode.Create, FileAccess.Write);
        
        formatter.Serialize(stream, dictPositions);
        stream.Close();
        
    }

    public static ArrayList Read()
    {
        ArrayList dictPositions;

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream("Assets/scripts/MagicOrchestraScripts/Game1_InstruMaps/objectsPosition.txt", FileMode.Open, FileAccess.Read);

        stream.Position = 0;
        dictPositions = (ArrayList)formatter.Deserialize(stream);

        return dictPositions;
    }


    /// <summary>
    /// The function is useful to retreve the positions of the objects in the arena or outside it.
    /// </summary>
    /// <param name="first"> Get initial positions or last positions </param>
    /// <returns></returns>
    public static Dictionary<int, Vector3Ser> RetrievePositions(bool first)
    {
        ArrayList dictPositions = Read();

        if (first)
        {
            return (Dictionary<int, Vector3Ser>)dictPositions[0];
        }
        else
        {
            return (Dictionary<int, Vector3Ser>)dictPositions[1];
        }
    }



    /// <summary>
    /// Retrieve object positions inside the arena (used only for serialization).
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int, Vector3Ser> GetFirstPositions()
    {
        Dictionary<int, Vector3Ser> dictPositions = new Dictionary<int, Vector3Ser>();

        dictPositions.Add(1, new Vector3Ser(-6.6f, 0f, 3f));
        dictPositions.Add(2, new Vector3Ser(-2.9f, 0, 7.3f));
        dictPositions.Add(3, new Vector3Ser(2.9f, 0, 7.3f));
        dictPositions.Add(4, new Vector3Ser(6.6f, 0, 3f));
        dictPositions.Add(5, new Vector3Ser(-4.1f, 0, 2.1f));
        dictPositions.Add(6, new Vector3Ser(-1.8f, 0, 4.8f));
        dictPositions.Add(7, new Vector3Ser(1.8f, 0, 4.8f));
        dictPositions.Add(8, new Vector3Ser(4.1f, 0, 2.1f));
        dictPositions.Add(9, new Vector3Ser(-2.1f, 0, 1.2f));
        dictPositions.Add(10, new Vector3Ser(-0.8f, 0, 2.7f));
        dictPositions.Add(11, new Vector3Ser(0.8f, 0, 2.7f));
        dictPositions.Add(12, new Vector3Ser(2.1f, 0, 1.2f));

        return dictPositions;
    }



    /// <summary>
    /// Retrieve object positions outside the arena (used only for serialization).
    /// </summary>
    /// <returns></returns>
    public static Dictionary<int, Vector3Ser> GetSecondPositions()
    {
        Dictionary<int, Vector3Ser> dictPositions = new Dictionary<int, Vector3Ser>();

        dictPositions.Add(1, new Vector3Ser(-10f, 0f, 10.5f));
        dictPositions.Add(2, new Vector3Ser(10f, 0, 10.5f));
        dictPositions.Add(3, new Vector3Ser(-6f, 0, 10.5f));
        dictPositions.Add(4, new Vector3Ser(6f, 0, 10.5f));
        dictPositions.Add(5, new Vector3Ser(-10f, 0, 7.5f));
        dictPositions.Add(6, new Vector3Ser(10f, 0, 7.5f));
        dictPositions.Add(7, new Vector3Ser(-10f, 0, 4.5f));
        dictPositions.Add(8, new Vector3Ser(10f, 0, 4.5f));
        dictPositions.Add(9, new Vector3Ser(-10f, 0, 1.5f));
        dictPositions.Add(10, new Vector3Ser(10f, 0, 1.5f));
        dictPositions.Add(11, new Vector3Ser(-2f, 0, 10.5f));
        dictPositions.Add(12, new Vector3Ser(2f, 0, 10.5f));

        return dictPositions;
    }

}




[System.Serializable]
public class Vector3Ser
{
    public float x, y, z;
    
    public Vector3Ser(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}


public enum Instruments {Trumpet, Violin, Drums, Piano, Maracas, Clarinet, DoubleBass, Guitar, Harp, SaxoPhone, Cymbals, Triangle}

public enum GeometricalShapes {Square, Circle, Rhombus, Nabla, Heart, Cross, Star, Hashtag, Slash, Arrow, Tilde, Dollar}
