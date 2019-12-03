using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class stickToGround : MonoBehaviour
{
    public GameObject stickyObject = null;
    private GameObject myObject = null;
    private GameObject attachedObject = null;
    private Player player = null;

    Transform t;
    private float y;
    private float x_r;
    private float y_r;
    private float z_r;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        y = transform.position.y;
        t = transform;
        y_r = transform.rotation.y;
        x_r = transform.rotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(attachedObject != null)
        {
            y_r = attachedObject.transform.eulerAngles.y;
        }     

        if(player.leftHand.currentAttachedObject != null)
        {
            attachedObject = player.leftHand.currentAttachedObject;
            //myObject = attachedObject;
        }
        else if (player.rightHand.currentAttachedObject != null)
        {
            attachedObject = player.rightHand.currentAttachedObject;
            //myObject = attachedObject;
        }
        else
        {
            if (attachedObject != null&& attachedObject==stickyObject)//reset to default X rotation
            {
                attachedObject.GetComponent<Transform>().eulerAngles = new Vector3(x_r, y_r, 0);
                attachedObject = null;//clear object
            }
        }

        if (transform.position.y < 0 || transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (attachedObject == stickyObject)
        {
            //attachedObject.transform.Rotate(0, y_r, 0, Space.Self);
            attachedObject.transform.eulerAngles = new Vector3(0, y_r, 0);
        }
    }
}
