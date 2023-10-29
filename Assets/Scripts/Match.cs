using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    private bool cage = false;
    private Wrestler wrestler1;
    private Wrestler wrestler2;
    
    void Start()
    {
        // create wrestler prefabs here instead of finding existing objects
        wrestler1 = transform.Find("Wrestler").GetComponent<Wrestler>();
        wrestler2 = transform.Find("W2").GetComponent<Wrestler>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}
