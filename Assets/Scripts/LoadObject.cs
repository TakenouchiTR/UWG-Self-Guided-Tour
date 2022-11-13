using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : DefaultObserverEventHandler
{
    public GameObject loadedObject;

    protected override void OnTrackingFound()
    {
        loadedObject.SetActive(true);
        
    }
}
