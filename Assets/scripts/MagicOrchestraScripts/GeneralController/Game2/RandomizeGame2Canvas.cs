using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeGame2Canvas : MonoBehaviour
{
    public GameObject randomPanel;

    public void ChangeRandomPanelStatus()
    {
        randomPanel.SetActive(!randomPanel.activeInHierarchy);
    }
}
