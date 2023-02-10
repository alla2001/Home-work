using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUI : MonoBehaviour
{
    public GameObject linePrefab;
    GameObject tempObject;
    DoorUI targetPoint;
    bool connected;
    bool connecting;
    public void onClick()
    {
        if (connected) 
        {
            Destroy(tempObject);
        }
        tempObject=Instantiate(linePrefab,transform);
        connecting = true;
    }
    private void Update()
    {
        if (connected)
        {
            tempObject.GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position,targetPoint.transform.position});
        }
        if (connecting)
        {
            tempObject.GetComponent<LineRenderer>().SetPositions(new Vector3[] { Vector3.zero, 
                Input.mousePosition - transform.parent.position});
        }
    }
       
}
