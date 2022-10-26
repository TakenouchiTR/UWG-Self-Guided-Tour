using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour
{

    public GameObject Entity;
    private bool isLoaded;

    // Start is called before the first frame update
    void Start()
    {
     this.Entity.SetActive(false);
     this.isLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.Entity.SetActive(this.isLoaded);
    }

    /// <summary>
    /// Swaps whether the game object has been loaded or not.
    /// </summary>
    public void SwapLoaded()
    {
        this.isLoaded = !this.isLoaded;
    }
}
