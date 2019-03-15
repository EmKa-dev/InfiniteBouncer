using UnityEngine;

[System.Serializable]
public class Designer
{

    public GameObject PlatformPrefab;
    public GameObject EnvironmentPlane;
    public GameObject BulletPrefab;

    [Header("Obstacles")]
    public GameObject[] ObstaclePrefabs;
    [Header("Special Plaforms")]
    public GameObject[] SpecialPlatformPrefabs;
}