using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2HitImageController : MonoBehaviour
{
    //Singleton of the CorsiContextManager class
    public static Game2HitImageController singleton = null;

    public List<Sprite> sprites;

    /* <summary>
     * The function is called when the component is instantiated
     * </summary>
     */
    void Awake()
    {
        //Code to manage the singleton uniqueness
        if ((singleton != null) && (singleton != this))
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }    

    public void ChangeImage()
    {
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[0];
        sprites.Remove(sprites[0]);
    }

    public void SetActiveDisplayedHints(bool isActive)
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(isActive);
    }

    public void ShuffleSprites()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            int rnd = Random.Range(0, sprites.Count);
            Sprite temp = sprites[rnd];
            sprites[rnd] = sprites[i];
            sprites[i] = temp;
        }
    }
}
