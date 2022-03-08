using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject Object1;
    // Can assign objects not to be destroyed on scene change. Currently only one object is assigned here, namely the scenehandler.

    private void Awake()
    {
        DontDestroyOnLoad(Object1);
    }

}
