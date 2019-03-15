using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Designer Designer;
  
    internal Dictionary<string, List<GameObject>> Objectpools = new Dictionary<string, List<GameObject>>
    {
        {"Platforms" , new List<GameObject>()},
        {"Platforms_Special", new List<GameObject>()},
        {"Obstacles", new List<GameObject>()},
    };

    internal GameObject Bullet;
    internal GameObject EnvPlane;

    const int AmountToPool = 200;
    const int AmountToPool_Special = 30;
    const int AmountToPool_Obstacle = 20;

    void Awake ()
    {

        //Instantiate bullet
        if (Designer.BulletPrefab != null)
        {
            Bullet = Instantiate(Designer.BulletPrefab);
        }
        //Fill platforms-pool
        for (int i = 0; i < AmountToPool; i++)
        {
            GameObject obj = Instantiate(Designer.PlatformPrefab);
            obj.SetActive(false);

            Objectpools["Platforms"].Add(obj);
        }

        //Fill special platforms-pool
        if (Designer.SpecialPlatformPrefabs.Length != 0)
        {
            int index = 0;
            for (int i = 0; i < AmountToPool_Special; i++)
            {
                GameObject obj = Instantiate(Designer.SpecialPlatformPrefabs[index]);
                obj.SetActive(false);

                Objectpools["Platforms_Special"].Add(obj);

                index = (index == Designer.SpecialPlatformPrefabs.Length-1) ? 0 : +1;

            }
        }

        //Fill obstacles-pool
        if (Designer.ObstaclePrefabs.Length != 0)
        {
            int index = 0;
            for (int i = 0; i < AmountToPool_Obstacle; i++)
            {
                GameObject obj = Instantiate(Designer.ObstaclePrefabs[index]);
                obj.SetActive(false);

                Objectpools["Obstacles"].Add(obj);

                index = (index == Designer.ObstaclePrefabs.Length-1) ? 0 : +1;
            }
        }

        //Instantiate backgroundplane
        EnvPlane = Instantiate(Designer.EnvironmentPlane);
        EnvPlane.SetActive(false);

    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < Objectpools[tag].Count; i++)
        {
            if (!Objectpools[tag][i].activeInHierarchy)
            {
                return Objectpools[tag][i];
            }
        }
        return null;
    }


    public void DeactivateObjects()
    {
        foreach (string s in Objectpools.Keys)
        {
            for (int i = Objectpools[s].Count - 1; i >= 0; i--)
            {
                Objectpools[s][i].SetActive(false);
            }
        }
    }
}
