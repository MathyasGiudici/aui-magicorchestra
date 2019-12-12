using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneToggle : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        CheckContextMode();
    }

    // Update is called once per frame
    void Update()
    {
        CheckContextMode();
    }

    private void CheckContextMode()
    {
        if (MagicOrchestraParameters.IsContext)
            panel.SetActive(true);
        else
            panel.SetActive(false);
    }
}
