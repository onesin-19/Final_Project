using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PanelMenu : MonoBehaviour
{
    public GameObject ownDescription;
    public GameObject otherDescription;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject==gameObject)
        {
            ownDescription.SetActive(true);
            otherDescription.SetActive(false);
        }
        else
        {
            ownDescription.SetActive(false);
            otherDescription.SetActive(true);
        }
    }
}
