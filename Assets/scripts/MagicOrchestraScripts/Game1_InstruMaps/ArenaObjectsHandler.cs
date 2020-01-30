using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaObjectsHandler : MonoBehaviour
{
    public GameObject prefab;
    private ArrayList arenaObjects;

    // Arraylist of elements of type ObjectSliceCouple
    public ArrayList objectSliceCouples;

    // Singleton of the ArenaObjectsHandler class
    public static ArenaObjectsHandler singleton = null;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if ((singleton != null) && (singleton != this))
        {
            Destroy(this);
            return;
        }
        singleton = this;
    }


    /* <summary>
    * This function creates randomly the instruments that will be used during the game in the context mode. 
    * </summary>
    * <param name="numElems"></param> */
    public void CreateInstruments(int numElems)
    {
        this.arenaObjects = new ArrayList();
        var random = new System.Random();

        Array enumArray = Enum.GetValues(typeof(Instruments));
        ArrayList instrNames = new ArrayList(enumArray);

        //Dictionary<int, Vector3Ser> dict = ArenaObjectsRetriever.Read();

        //Instatiate the number of elements depending on the difficulty of the task.
        for (int i = 1; i <= numElems; i++)
        {
            GameObject newInstr = Instantiate(prefab);

            int index = random.Next(0, instrNames.Count);

            //Assign a name to the object in context mode -> than also an image.
            newInstr.name = instrNames[index].ToString();
            instrNames.RemoveAt(index);
            
            //TODO
            //newInstr.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Design/Game1/" + newInstr.name);

            this.arenaObjects.Add(newInstr);
        }
        
        this.arenaObjects = ShuffleObjects(this.arenaObjects);
        CombineObjectsWithSlices();
        SetArenaPositions();
        
    }

    /* <summary>
    * This function creates randomly the instruments that will be used during the game in the context mode. 
    * </summary>
    * <param name="numElems"></param> */
    public void CreateShapes(int numElems)
    {
        this.arenaObjects = new ArrayList();
        var randomShapes = new System.Random();

        Array enumArray = Enum.GetValues(typeof(GeometricalShapes));
        ArrayList shapeNames = new ArrayList(enumArray);

        //Dictionary<int, Vector3Ser> dict = ArenaObjectsRetriever.Read();

        for (int i = 1; i <= numElems; i++)
        {
            GameObject newInstr = Instantiate(prefab);

            int index = randomShapes.Next(0, shapeNames.Count);

            //Assign a name to the object in context mode -> than also an image.
            newInstr.name = shapeNames[index].ToString();
            shapeNames.RemoveAt(index);

            this.arenaObjects.Add(newInstr);
        }

        this.arenaObjects = ShuffleObjects(this.arenaObjects);
        CombineObjectsWithSlices();
        SetArenaPositions();
    }


    /// <summary>
    /// Set the initial position of the objects on the arena, which is the exact position that the user has to remember to win the game.
    /// </summary>
    private void CombineObjectsWithSlices()
    {
        ArrayList slices = ArenaSetup.singleton.GetSlices();

        this.objectSliceCouples = new ArrayList();

        int sliceIndex = 0;

        for (int objectIndex = 0 ; objectIndex < this.arenaObjects.Count; objectIndex++)
        {
            GameObject adjacent = ObjectInAdjacentList((GameObject)slices[sliceIndex]);

            if (adjacent != null){

                this.objectSliceCouples.Add(new ObjectSliceCouple((GameObject)slices[sliceIndex], (GameObject)this.arenaObjects[objectIndex]));
                this.objectSliceCouples.Add(new ObjectSliceCouple(adjacent, (GameObject)this.arenaObjects[objectIndex]));
                sliceIndex++;
            }
            else
            {
                this.objectSliceCouples.Add(new ObjectSliceCouple((GameObject)slices[sliceIndex], (GameObject)this.arenaObjects[objectIndex]));
            }

            sliceIndex++;
        }

        //Print only for verification...
        for(int i = 0; i < this.objectSliceCouples.Count; i++)
        {
           Debug.Log("Elemento " + i + ": " + ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaObject.ToString() + " in " + ((ObjectSliceCouple)this.objectSliceCouples[i]).slice.ToString());
        }
    }


    /// <summary>
    /// Controls if the passed slice is in the adjacentSet.
    /// </summary>
    /// <param name="slice"></param>
    /// <returns></returns>
    public GameObject ObjectInAdjacentList(GameObject slice)
    {
        ArrayList adjacentSlices = ArenaSetup.singleton.GetDoubleSlices();

        Debug.Log("Adjacent slices are " + adjacentSlices.Count);

        //Control of the adjacent slices.
        for (int j = 0; j < adjacentSlices.Count; j++)
        {
            Slice tempAdjacent = (Slice)adjacentSlices[j];

            if (slice == tempAdjacent.slice)
            {
                return tempAdjacent.adjacentSlice;
            }
        }

        return null;
    }


    /// <summary>
    /// This method is useful to shuffle created objects to randomly assign them to precise slice.
    /// </summary>
    /// <param name="listToShuffle"></param>
    /// <returns></returns>
    private ArrayList ShuffleObjects(ArrayList listToShuffle)
    {
        ArrayList randomList = new ArrayList();

        var rnd = new System.Random();
        int randomIndex = 0;
        while (listToShuffle.Count > 0)
        {
            randomIndex = rnd.Next(0, listToShuffle.Count); //Choose a random object in the list
            randomList.Add(listToShuffle[randomIndex]); //add it to the new, random list
            listToShuffle.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }


    /// <summary>
    /// This method set the initial position of each object for each slice and it takes into account also the different cases in which slices have 
    /// to be considered together and the object is moved a bit from is natural position (just to have a better distribution of the space).
    /// </summary>
    public void SetArenaPositions()
    {        
        Dictionary<int, Vector3Ser> positions = ArenaObjectsRetriever.RetrievePositions(true);

        ArrayList adjacents = ArenaSetup.singleton.GetDoubleSlices();

        this.SetZeroPosition();

        int currentSliceIndex = 1;
        int adjacentSliceIndex = 0;
        int objectIndex = 0;

        for (int i = 0; i < this.objectSliceCouples.Count; i++)
        {   
            if(adjacents.Count != 0)
            {
                if (((ObjectSliceCouple)this.objectSliceCouples[i]).slice == ((Slice)adjacents[adjacentSliceIndex]).slice)
                {
                    float x = (positions[currentSliceIndex].x + positions[currentSliceIndex + 1].x) / 3;
                    float y = (positions[currentSliceIndex].y + positions[currentSliceIndex + 1].y) / 3;
                    float z = (positions[currentSliceIndex].z + positions[currentSliceIndex + 1].z) / 3;

                    ((GameObject)this.arenaObjects[objectIndex]).transform.position = new Vector3(x, y, z);
                    ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = new Vector3(x, y, z);

                    currentSliceIndex += 2;
                    adjacentSliceIndex += 2;
                    i++;
                }
                else
                {
                    float x = positions[currentSliceIndex].x;
                    float y = positions[currentSliceIndex].y;
                    float z = positions[currentSliceIndex].z;

                    ((GameObject)this.arenaObjects[objectIndex]).transform.position += new Vector3(x, y, z);

                    if (Game1Parameters.Difficulty <= 4)
                    {
                        ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.5f;
                    }

                    if (Game1Parameters.Difficulty > 4 && Game1Parameters.Difficulty <= 8 && currentSliceIndex > 4)
                    {
                        ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.2f;
                    }

                    ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = ((GameObject)this.arenaObjects[objectIndex]).transform.position;
                    currentSliceIndex++;
                }
            }
            else
            {
                float x = positions[currentSliceIndex].x;
                float y = positions[currentSliceIndex].y;
                float z = positions[currentSliceIndex].z;

                ((GameObject)this.arenaObjects[objectIndex]).transform.position += new Vector3(x, y, z);

                if (Game1Parameters.Difficulty <= 4)
                {
                    ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.5f;
                }

                if (Game1Parameters.Difficulty > 4 && Game1Parameters.Difficulty <= 8 && currentSliceIndex > 4)
                {
                    ((GameObject)this.arenaObjects[objectIndex]).transform.position /= 1.2f;
                }

                ((ObjectSliceCouple)this.objectSliceCouples[i]).arenaPosition = ((GameObject)this.arenaObjects[objectIndex]).transform.position;
                currentSliceIndex++;
            }

            objectIndex++;

        }
    }

    /// <summary>
    /// This Method set the position from which the user has to drag objects inside the arena
    /// </summary>
    public void SetDragAndDropPositions()
    {
        Dictionary<int, Vector3Ser> positions = ArenaObjectsRetriever.RetrievePositions(false);

        for (int i = 0; i < this.objectSliceCouples.Count; i++)
        {
            float x = positions[i + 1].x;
            float y = positions[i + 1].y;
            float z = positions[i + 1].z;

            ((GameObject)this.arenaObjects[i]).transform.position = new Vector3(x, y, z);

            foreach (ObjectSliceCouple couple in this.objectSliceCouples)
            {
                if (couple.arenaObject == (GameObject)this.arenaObjects[i])
                {
                    couple.dragAndDropPosition = ((GameObject) this.arenaObjects[i]).transform.position;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Set the position of the objects in (0,0,0)
    /// </summary>
    private void SetZeroPosition()
    {
        for(int i = 0; i < this.arenaObjects.Count; i++)
        {
            ((GameObject)this.arenaObjects[i]).transform.position = new Vector3(0, 0, 0);
        }       
    }
}



/// <summary>
/// Class used to wrap slice and object in a single object.
/// </summary>
class ObjectSliceCouple
{
    public GameObject slice;
    public GameObject arenaObject;
    public Vector3 arenaPosition;
    public Vector3 dragAndDropPosition;

    public ObjectSliceCouple(GameObject slice, GameObject arenaObject)
    {
        this.slice = slice;
        this.arenaObject = arenaObject;
    }


}