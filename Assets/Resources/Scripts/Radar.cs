using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Transform playerPos;
    public Camera MapCamera;
    private float mapScale=2.0f;
    public static List<RadarObject> radObjects=new List<RadarObject>();

    public static void RegisterRadarObject(GameObject obj, Image img)
    {
        Image image = Instantiate(img);
        radObjects.Add(new RadarObject(){owner= obj,icon=image});
    }
    
    public static void RemoveRadarObject(GameObject obj)
    {
        List<RadarObject> newList=new List<RadarObject>();
        for (int i = 0; i < radObjects.Count; i++)
        {
            if (radObjects[i].owner==obj)
            {
                Destroy(radObjects[i].icon);
                continue;
            }
            else
            {
                newList.Add(radObjects[i]);
            }
        }
        radObjects.RemoveRange(0,radObjects.Count);
        radObjects.AddRange(newList);
    }

    public void DrawRadarDots()
    {
        if (PlayerManager.Instance.player!=null)
        {
            playerPos = PlayerManager.Instance.player.transform;
            MapCamera = PlayerManager.Instance.player.transform.GetChild(2).GetComponent<Camera>();
            foreach (RadarObject ro in radObjects)
            {
                /*Vector3 radarPos=MapCamera.WorldToViewportPoint(ro.owner.transform.position);
                ro.icon.transform.SetParent(transform);
                RectTransform rt = GetComponent<RectTransform>();
                Vector3[] corners=new Vector3[4];
                rt.GetWorldCorners(corners);
                radarPos.x = Mathf.Clamp(radarPos.x * rt.rect.width+corners[0].x,corners[0].x,corners[2].x);
                radarPos.y = Mathf.Clamp(radarPos.y * rt.rect.height+corners[0].y,corners[0].y,corners[1].y);
                //radarPos.x = Mathf.Clamp(radarPos.x * rt.rect.width-1,-1,1);
                //radarPos.y = Mathf.Clamp(radarPos.y * rt.rect.height-1,-1,1);

                radarPos.z = 1;
                ro.icon.transform.position = radarPos;*/
                Vector3 radarPos= ro.owner.transform.position-playerPos.position;
                float distToObject = Vector3.Distance(playerPos.position, ro.owner.transform.position) * mapScale;
                float dy = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;
                if (distToObject <25)
                {
                    radarPos.x = distToObject * Mathf.Cos(dy * Mathf.Deg2Rad) * -1;
                    radarPos.z = distToObject * Mathf.Sin(dy * Mathf.Deg2Rad);
                }
                else
                {
                    radarPos.x = 25 * Mathf.Cos(dy * Mathf.Deg2Rad) * -1;
                    radarPos.z = 25 * Mathf.Sin(dy * Mathf.Deg2Rad);
                }
                ro.icon.transform.SetParent(transform);
                ro.icon.transform.position=new Vector3(radarPos.x,radarPos.z,0)+this.transform.position;
                //Debug.Log(ro.owner+": layer "+(Mathf.Pow(2,ro.owner.layer))+" vs "+LayerMask.GetMask("Potion"));
                if (((int)Mathf.Pow(2,ro.owner.layer)) == LayerMask.GetMask("Potion"))
                    ro.icon.transform.SetSiblingIndex(transform.childCount-2);
            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        DrawRadarDots();
    }
}

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}