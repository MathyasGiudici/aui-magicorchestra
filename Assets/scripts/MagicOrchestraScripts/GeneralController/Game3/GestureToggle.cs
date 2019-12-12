using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureToggle : MonoBehaviour
{
	public GameObject timePanel;

    void Start()
    {
        timePanel.SetActive(false);
    }

    public void ChangeTimeZenithPanel()
    {
        timePanel.SetActive(!gameObject.GetComponent<GeneralGameToggle>().GetToggleStatus());
    }
}
