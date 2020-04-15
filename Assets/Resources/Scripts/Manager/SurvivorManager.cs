using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorManager  : Flow
{
    public Transform[] waypoints;
    private GameObject medea;
    public Survivor survivor;
    private bool canEscape;
    private float distanceToSave=6;
    
    #region Singleton
    static private SurvivorManager instance = null;

    static public SurvivorManager Instance
    {
        get
        {
            return instance ?? (instance = new SurvivorManager());
        }
    }

    #endregion
    override public void PreInitialize()
    {
       
    }

    override public void Initialize()
    {

        SetPoints();
        medea=GameObject.Instantiate(Resources.Load("Prefabs/Survivor/eve"))as GameObject;
        survivor = medea.GetComponent<Survivor>();
        survivor.Initialize();
    }

    override public void Refresh()
    {
        if (Vector3.Distance(PlayerManager.Instance.player.transform.position, medea.transform.position) < distanceToSave)
            canEscape = true;
        if (canEscape)
        {
            if (!survivor.isDead)
            {
                survivor.Refresh();
            }
        }
    }

    override public void PhysicsRefresh()
    {
        if (canEscape)
        {
            if (!survivor.isDead)
            {
                survivor.PhysicsRefresh();
            }
        }
        
    }
    
    override public void EndFlow()
    {
        instance = null;
    }
    public void SurvivorDegats(float damage)
    {
        survivor.TakeDamage(damage);
    }
    public void SetPoints()
    {
        waypoints = Waypoints.points;

        /*for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }*/
    }
    
}
