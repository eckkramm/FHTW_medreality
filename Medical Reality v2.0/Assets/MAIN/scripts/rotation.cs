using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class rotation : MonoBehaviour
{
    public GameObject rotationObject = null;
    private GameObject myObject = null;
    private GameObject attachedObject = null;
    private Player player = null;

    Transform t;
    private static float x;
    private static float y;
    private static float z;
    private static float x_r;
    private float y_r;
    private static float z_r;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        t = transform;

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        y_r = transform.rotation.y;
        x_r = transform.rotation.x;
        z_r = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedObject != null)
        {
            attachedObject.GetComponent<Transform>().position = new Vector3(x, y, z);
            y_r = attachedObject.transform.eulerAngles.y;
        }

        if (player.leftHand.currentAttachedObject != null)
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
            if (attachedObject != null && attachedObject == rotationObject)//reset to default rotation
            {
                attachedObject.GetComponent<Transform>().position = new Vector3(x, y, z);
                attachedObject.GetComponent<Transform>().eulerAngles = new Vector3(0, y_r, 0);
                attachedObject = null;//clear object
            }
        }

        if(y_r != attachedObject.GetComponent<Transform>().eulerAngles.y)
        {
            transform.eulerAngles = new Vector3(0, y_r, 0);
        }

        if (attachedObject == rotationObject)
        {
            //attachedObject.transform.Rotate(0, y_r, 0, Space.Self);
            attachedObject.transform.eulerAngles = new Vector3(0, y_r, 0);
        }
    }
}
