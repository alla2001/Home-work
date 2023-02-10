using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingelton : MonoBehaviour
{
    public static CameraSingelton instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
}
