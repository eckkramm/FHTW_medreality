using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiter : MonoBehaviour
{
    private Vector3 exitPoint;
    public GameObject toTrack;
    private bool outSide = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(outSide)
        moveBallBack();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name==toTrack.name)
        {
            exitPoint = other.transform.position;
            Debug.Log("In TriggerExit");
            outSide = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == toTrack.name)
        {
            exitPoint = other.transform.position;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == toTrack.name)
        {
            outSide = false;

        }
    }
    private void moveBallBack()
    {
        toTrack.transform.position = exitPoint;
    }
}
