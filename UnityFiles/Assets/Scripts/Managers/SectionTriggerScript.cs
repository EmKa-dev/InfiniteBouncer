using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//What should happen on sectionenter:
//Change design of bullet
//Despawn/spawn new pickups
//Place new player respawn

public class SectionTriggerScript : MonoBehaviour
{
    BulletTimer BulletScript;
    PlayerController PlayerCon;

    Vector3 NextSectionStartPoint;
    GameObject NextBulletDesign;
    GameObject[] LifePickups;
    GameObject[] PowerUps;

    Collider DeathTrigger;

    List<Vector3> PickupsSpawnPositions = new List<Vector3>(5);
    List<Vector3> PowerUpSpawnPositions = new List<Vector3>(2);

    void Start ()
    {
        LifePickups = SinglePool.PickupsArr;

        PowerUps = SinglePool.PowerUpsArr;
        for (int i = 0; i < 2; i++)
        {
            PowerUps[i].transform.position = new Vector3(0, 2, 100*(i+1));
        }

        DeathTrigger = SinglePool.DeathTriggerInstance;
        PlayerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        BulletScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BulletTimer>();
        transform.position = new Vector3(0, 0, 350);
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 8)
        {
            PlayerCon.ChangeSpawnPoint(transform.position);
            gameObject.transform.position = NextSectionStartPoint;
            SpawnPickups();
            ChangeBulletDesign();
            DeathTrigger.transform.position = new Vector3(0, -3, DeathTrigger.transform.position.z + 350);
        }
    }

    public void SetNextSectionTrigger(Vector3 point)
    {
        NextSectionStartPoint = point;
    }

    public void SetNextBulletDesign(GameObject bull)
    {
        NextBulletDesign = bull;
    }

    public void SetNextPickupsSpawns(List<Vector3> lifeuppos, List<Vector3> poweruppos)
    {
        PickupsSpawnPositions = lifeuppos;
        PowerUpSpawnPositions = poweruppos;
    }

    private void SpawnPickups()
    {
        for (int i = 0; i < PickupsSpawnPositions.Count; i++)
        {
            LifePickups[i].transform.position = PickupsSpawnPositions[i];
            LifePickups[i].SetActive(true);
        }

        for (int i = 0; i < PowerUpSpawnPositions.Count; i++)
        {
            PowerUps[i].transform.position = PowerUpSpawnPositions[i];
            PowerUps[i].SetActive(true);
        }
    }

    private void ChangeBulletDesign()
    {
        BulletScript.Bullet = this.NextBulletDesign;
    }
}
