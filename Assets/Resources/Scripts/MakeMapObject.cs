using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMapObject : MonoBehaviour
{

    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        Radar.RegisterRadarObject(gameObject,image);
    }
    
    private void OnDestroy()
    {
        Radar.RemoveRadarObject(gameObject);
    }
}
