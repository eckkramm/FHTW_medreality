using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class doors : MonoBehaviour
{
    Animator animator;
    bool doorOpen;
    DateTime time;

    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
        time = new DateTime();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(!doorOpen)
            {
                Debug.Log("OPEN");
                doorOpen = true;
                time = DateTime.Now.AddSeconds(3);
                Debug.Log("OP: "+time);
                DoorControl("Open");
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("EX: Entered Trigger Exit");
        if (doorOpen)
        {
            Debug.Log("EX: " + DateTime.Now);

            StartCoroutine(WaitForDoorToClose(time, value =>
            {
                Debug.Log("EX: Waited");
                if (time < DateTime.Now)
                {
                    Debug.Log("EX: CLOSE");
                    doorOpen = false;
                    DoorControl("Close");
                }
            }));
        }
    }

    IEnumerator WaitForDoorToClose(DateTime time, Action<DateTime> onComplete)
    {
        yield return new WaitForSeconds(4);
        onComplete(time);
    }

    void DoorControl(string direction)
    {
        animator.SetTrigger(direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
