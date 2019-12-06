using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceToggle : MonoBehaviour
{
    private int index = -1;

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public int GetIndex()
    {
        return this.index;
    }
}
