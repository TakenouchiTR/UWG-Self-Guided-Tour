using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiContentPanel : MonoBehaviour
{
    public void DisplayPanel()
    {
        base.GetComponent<Animator>().SetBool("Open", true);
    }

    public void HidePanel()
    {
        base.GetComponent<Animator>().SetBool("Open", false);
    }
}
