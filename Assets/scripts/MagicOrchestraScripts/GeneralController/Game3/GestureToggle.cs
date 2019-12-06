using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureToggle : MonoBehaviour
{
	private bool status = true;

    public void ChangeStatus()
	{
		this.status = !this.status;
	}

    public bool GetGestureMode()
	{
		return this.status;
	}
}
