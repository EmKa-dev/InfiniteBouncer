using UnityEngine;


/// <summary>
/// Class responsible for holding references to objects/prefabs that other scripts can reach into to grab a required object.
/// </summary>
public class SinglePool : MonoBehaviour
{

    public GameObject TransPlanePrefab;
    public BoxCollider GenerationTriggerPrefab;
    public BoxCollider NewSectionTriggerPrefab;
    public ParticleSystem SandsplashPrefab;
    public GameObject PickUpPrefab;
    public Texture CracksTex;
    public Collider DeathTriggerPrefab;
    public GameObject[] PowerUpsPrefabs;
    public GameObject FinishPlanePrefab;
    public GameObject SafePlatformPrefab;

    internal static Texture CracksTexInstance;

    internal static GameObject TransPlaneInstance;
    internal static BoxCollider TriggerInstance;
    internal static BoxCollider SectionTriggerInstance;
    internal static ParticleSystem SandsplashInstance;
    internal static GameObject[] PickupsArr;
    internal static GameObject[] PowerUpsArr;
    internal static Collider DeathTriggerInstance;
    internal static GameObject FinishPlanInstance;
    internal static GameObject SafePlatformInstance;

    private void Awake()
    {
        //These instance are placed immedietly upon start, no need to set unactive
        TransPlaneInstance = Instantiate(TransPlanePrefab);
        TriggerInstance = Instantiate(GenerationTriggerPrefab);
        SectionTriggerInstance = Instantiate(NewSectionTriggerPrefab);
        DeathTriggerInstance = Instantiate(DeathTriggerPrefab);

        PowerUpsArr = new GameObject[PowerUpsPrefabs.Length];
        for (int i = 0; i < PowerUpsPrefabs.Length; i++)
        {
            PowerUpsArr[i] = Instantiate(PowerUpsPrefabs[i]);
        }

        PickupsArr = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            PickupsArr[i] = Instantiate(PickUpPrefab);
            PickupsArr[i].SetActive(false);
        }

        CracksTexInstance = CracksTex;
        SandsplashInstance = Instantiate(SandsplashPrefab);
        FinishPlanInstance = Instantiate(FinishPlanePrefab);
        FinishPlanInstance.SetActive(false);
        SafePlatformInstance = Instantiate(SafePlatformPrefab);
        SafePlatformInstance.SetActive(false);
    }
}
