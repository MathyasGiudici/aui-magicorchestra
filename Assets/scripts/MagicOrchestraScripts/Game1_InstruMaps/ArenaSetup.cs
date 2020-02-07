using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSetup : MonoBehaviour
{
    public GameObject arena;

    GameObject firstLevel;
    GameObject secondLevel;
    GameObject thirdLevel;

    int indexSlice1 = 0;
    int indexSlice2 = 1;
    int indexSlice3 = 2;
    int indexSlice4 = 3;

    private ArrayList adjacentSlices = new ArrayList();

    public Material white;
    public Material black;

    bool isContext = MagicOrchestraParameters.IsContext;

    // Singleton of the InstruMapsController class
    public static ArenaSetup singleton = null;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    private void Awake()
    {
        if ((singleton != null) && (singleton != this)) {
            Destroy(this);
            return;
        }
        singleton = this;
    }


    public ArrayList GetDoubleSlices()
    {
        return this.adjacentSlices;
    }

    /// <summary>
    /// Create the arena depending on the difficulty set by the expert.
    /// </summary>
    /// <param name="difficulty"></param>
    public void CreateArena(int difficulty)
    {
        switch (difficulty)
        {
            case 2:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;
                
                firstLevel.SetActive(false);
                secondLevel.SetActive(false);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(thirdLevel.transform.GetChild(indexSlice1).gameObject, thirdLevel.transform.GetChild(indexSlice2).gameObject);
                this.AggregateAdjacent(thirdLevel.transform.GetChild(indexSlice3).gameObject, thirdLevel.transform.GetChild(indexSlice4).gameObject);

                // Active double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);
                    
                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 3:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(false);
                secondLevel.SetActive(false);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(thirdLevel.transform.GetChild(indexSlice3).gameObject, thirdLevel.transform.GetChild(indexSlice4).gameObject);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);
                    
                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 4:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(false);
                secondLevel.SetActive(false);
                thirdLevel.SetActive(true);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    
                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 6:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(false);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(secondLevel.transform.GetChild(indexSlice1).gameObject, secondLevel.transform.GetChild(indexSlice2).gameObject);
                this.AggregateAdjacent(secondLevel.transform.GetChild(indexSlice3).gameObject, secondLevel.transform.GetChild(indexSlice4).gameObject);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 7:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(false);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(secondLevel.transform.GetChild(indexSlice3).gameObject, secondLevel.transform.GetChild(indexSlice4).gameObject);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Single and double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 8:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(false);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Single and double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 10:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(true);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(firstLevel.transform.GetChild(indexSlice1).gameObject, firstLevel.transform.GetChild(indexSlice2).gameObject);
                this.AggregateAdjacent(firstLevel.transform.GetChild(indexSlice3).gameObject, firstLevel.transform.GetChild(indexSlice4).gameObject);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Single and double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Double slices in first level
                firstLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice1).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);
                    firstLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 11:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(true);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                this.AggregateAdjacent(firstLevel.transform.GetChild(indexSlice3).gameObject, firstLevel.transform.GetChild(indexSlice4).gameObject);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Single and double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Double slices in first level
                firstLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice3).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextDoubleSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            case 12:
                thirdLevel = arena.transform.GetChild(0).gameObject;
                secondLevel = arena.transform.GetChild(1).gameObject;
                firstLevel = arena.transform.GetChild(2).gameObject;

                firstLevel.SetActive(true);
                secondLevel.SetActive(true);
                thirdLevel.SetActive(true);

                // Active single and double slices
                thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Single and double slices in second level
                secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                // Double slices in first level
                firstLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.SetActive(true);
                firstLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.SetActive(true);

                if (isContext)
                {
                    ArenaObjectsHandler.singleton.CreateInstruments(difficulty);
                }
                else
                {
                    thirdLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    thirdLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    secondLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice3).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);
                    firstLevel.transform.GetChild(indexSlice4).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(MagicOrchestraUtils.freeNoContextSlicePath);

                    ArenaObjectsHandler.singleton.CreateShapes(difficulty);
                }

                break;

            default:
                Debug.Log("Invalid choice!");
                break;

        }
    }


    /* <summary>
     * The function check whether two slices are adjacent or not and returns a boolean.
     * </summary> */
    public bool IsAdjacent(GameObject first, GameObject second)
    {
        if ((first.name == "slice1" && second.name == "slice2") ||
            (first.name == "slice3" && second.name == "slice4") ||
            (first.name == "slice5" && second.name == "slice6") ||
            (first.name == "slice7" && second.name == "slice8") ||
            (first.name == "slice9" && second.name == "slice10") ||
            (first.name == "slice11" && second.name == "slice12"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /* <summary>
     * The function give the same color to adjacent slices which have to take the same object during the game.
     * </summary>*/
     //TODO: fix this or delete it. 
    public void AggregateAdjacent(GameObject firstSlice, GameObject secondSlice)
    {
        if(IsAdjacent(firstSlice, secondSlice))
        {
            this.adjacentSlices.Add(new Slice(firstSlice, secondSlice));
            this.adjacentSlices.Add(new Slice(secondSlice, firstSlice));
        }
    }


    /* <summary>
     * The function returns the slices which are active with respect to arena initialization. 
     * </summary>
     * <returns></returns> */
    public ArrayList GetSlices()
    {
        int numOfLevels = 3;
        int numOfSlicesPerLevel = 4;

        ArrayList slices = new ArrayList();
        for(int i = 0; i < numOfLevels; i++)
        {
            GameObject level = this.arena.transform.GetChild(i).gameObject;

            if (level.activeSelf)
            {
                for (int j = 0; j < numOfSlicesPerLevel; j++)
                {
                    slices.Add(level.transform.GetChild(j).gameObject);
                }
            }
            else { break; }
        }

        return slices;
    }

}

/// <summary>
/// Class wrapper of object "slice" used to find adjacent slices.
/// </summary>
class Slice
{
    public GameObject slice;
    public GameObject adjacentSlice;

    public Slice(GameObject slice, GameObject adjacentSlice)
    {
        this.slice = slice;
        this.adjacentSlice = adjacentSlice;
    }
}
