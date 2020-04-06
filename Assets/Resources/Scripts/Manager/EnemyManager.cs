using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    PARASITE, SIMPLE_1, SIMPLE_2, SLOW, BOSSPUG, BOSSCOW
}
public class EnemyManager : Flow
{
    #region Singleton
    static private EnemyManager instance = null;

    static public EnemyManager Instance
    {
        get
        {
            return instance ?? (instance = new EnemyManager());
        }
    }

    #endregion


    private GameObject enemyParent;
    public List<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;
    //AmbianceManager ambiance;
    
    public Dictionary<EnemyType, GameObject> enemyPrefabDict;

    override public void PreInitialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new List<Enemy>();
        enemyPrefabDict = new Dictionary<EnemyType, GameObject>();
        //ambiance = AmbianceManager.Instance;
    }

    override public void Initialize()
    {

        //SetPoints(MapVariables.instance.enemyParentPoint.transform);
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("ennemi");

        foreach (GameObject en in ennemis)
        {
            Enemy e = en.GetComponent<Enemy>();
            e.Initialize();
            toAdd.Push(e);
        }
    }

    override public void Refresh()
    {
        SpawnEnemy(enemyPrefabDict[EnemyType.PARASITE]);
        foreach (Enemy e in enemies)
            if (!e.isDead)
            {
                e.Refresh();
            }
            
    }

    override public void PhysicsRefresh()
    {

        while (toRemove.Count > 0)
        {
            try
            {
                Enemy e = toRemove.Pop();
                enemies.Remove(e);
                GameObject.Destroy(e.gameObject, 10f);
                //WaveManager.Instance.EnemyInWaveLeft--;
                

            }
            catch
            {
                Debug.Log("hey this happened");
            }
        }

        while (toAdd.Count > 0)
            enemies.Add(toAdd.Pop());


        foreach (Enemy enemy in enemies)
            enemy.PhysicsRefresh();
    }

    public void EnemyDied(Enemy enemyDied)
    {
        //ambiance.deadEnemy();
        toRemove.Push(enemyDied);
    }

    public void SpawnEnemy(GameObject enemy)
    {
        //ambiance.playSpawnSounds();
        foreach (Spawner spw in LevelVariables.instance.Spawn)
        {
            Transform enemyStart = spw.position;
            float distance = Vector3.Distance(PlayerManager.Instance.player.transform.position, enemyStart.position);
            //Debug.Log(distance);
            
            if(distance < spw.DistanceSpawn && Time.time>spw.NextSpawn)
            {
                spw.NextSpawn = Time.time + spw.SpawnRate;
                GameObject obj=GameObject.Instantiate(enemy, enemyStart.position, Quaternion.identity) as  GameObject;
                Enemy e = obj.GetComponent<Enemy>();
                e.Initialize();
                toAdd.Push(e);
            
            }
        }
        //return GameObject.Instantiate(enemy, enemyStart.position, enemyStart.rotation);
        
        
        
        
        
    }

    public Enemy FindFirstTargetInRange(Vector3 position, float range)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        Enemy enemy = null;
        if (enemyInRange.Count > 0)
            enemy = enemyInRange[0];
        return enemy;
    }

    public List<Enemy> EnemiesInRange(Vector3 position, float range)
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        for (int i = 0; i < enemies.Count; i++)
        {
            if (range >= Vector3.Distance(position, enemies[i].transform.position))
                enemiesInRange.Add(enemies[i]);
        }
        return enemiesInRange;
    }

    public void DamageEnemiesInRange(Vector3 position, float range, int damage)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        foreach (Enemy enemy in enemyInRange)
            enemy.TakeDamage(damage);
    }

    public void DamageEnemie(GameObject enemy,int damage)
    {
        
       // List<Enemy> enemyInRange = EnemiesInRange(position, range);
       Enemy ene = null;
       int i = 0;
       while (i<enemies.Count&&ene==null)
       {
           if (enemies[i].transform.gameObject==enemy)
           {
               ene = enemies[i];
           }

           i++;
       }
       if(ene) 
           ene.TakeDamage(damage);
    }

    //decimalSpeed, has to be a decimal 0.01 - 0.99
    /*public void SlowEnemiesInRange(Vector3 position, float range, float decimalSpeed, float duration)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        foreach (Enemy enemy in enemyInRange)
            enemy.Slow(decimalSpeed, duration);
    }*/

    override public void EndFlow()
    {
        instance = null;
    }

    /*public void SetPoints(Transform transform)
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }*/
}

[Serializable]
public class Spawner
{
    public float DistanceSpawn = 50f, SpawnRate = 2f;
    public float NextSpawn;
    public Transform position;

}