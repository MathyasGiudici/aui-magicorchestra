using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralGameToggle : MonoBehaviour
{
    // Internal status
	private bool status = false;

    public void Update()
	{
        this.status = gameObject.GetComponent<Toggle>().isOn;
	}

    public bool GetToggleStatus()
	{
		return this.status;
	}
}
