using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (ContextSliderController.singleton.GetContextStatus())
        {
            if (GuidedSliderController.singleton.GetGuidedStatus())
            {
                //I'm in context and guided
                GuidedSliderController.singleton.gameObject.SetActive(true);
                IndexesSelecterController.singleton.gameObject.SetActive(false);
                return;
            }
            else
            {
                //I'm in context and index
                GuidedSliderController.singleton.gameObject.SetActive(true);
                IndexesSelecterController.singleton.gameObject.SetActive(true);
            }
        }
        else
        {
            //I'm not in context mode
            GuidedSliderController.singleton.gameObject.SetActive(false);
            IndexesSelecterController.singleton.gameObject.SetActive(true);
        }
    }
}
