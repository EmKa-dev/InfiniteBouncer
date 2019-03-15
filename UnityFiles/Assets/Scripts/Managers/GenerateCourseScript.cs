using System.Collections.Generic;
using UnityEngine;

public class GenerateCourseScript : MonoBehaviour
{

    public ObjectPooler[] ObjectPools;

    //Quantity of platforms generated is based on lenght of backgroundplane (Determined by the 3D-model)
    int BGPlane_Lenght = 250;
    int TransitionalPlaneLenght = 100;

    //Trackers used when spawning objects
    private int StartGenerateAtThisPoint;
    private int ZPos;
    private int XPos;

    //Cached references to single objects to use
    GameObject TransPlane;
    Renderer TransPlaneRenderer;
    Collider GenTrigger;
    SectionTriggerScript SectionTriggerscript;

    //Store spawnpositions for pickups to send to sectiontrigger
    List<Vector3> PowerUpPositions = new List<Vector3>(2);
    List<Vector3> LifeUpPositions = new List<Vector3>(5);

    private void Start()
    {
        TransPlane = SinglePool.TransPlaneInstance;
        TransPlaneRenderer = TransPlane.GetComponent<Renderer>();
        GenTrigger = SinglePool.TriggerInstance;
        SectionTriggerscript = SinglePool.SectionTriggerInstance.GetComponent<SectionTriggerScript>();
        GetComponent<DifficultyManager>().IncrementAndUpdateDifficulties();

        //Set starting things
        GameObject startingplatform = ObjectPools[0].GetPooledObject("Platforms");
        SpawnObject(startingplatform);
        GetComponent<BulletTimer>().Bullet = ObjectPools[0].Bullet;

        Generate();
    }

    //Keeping track of used design
    int PreviousDesign;
    int newdesign = -1;

    public void Generate()
    {
        LifeUpPositions.Clear();
        PowerUpPositions.Clear();
        #region Local variables for generation

        int maxOffsetOnZ = DifficultyManager.MinForwardOffset;
        int minOffsetOnZ = DifficultyManager.MaxForwardOffset;
        int maxOffsetOnX = DifficultyManager.MinSidewaysOffset;
        int minOffsetOnx = DifficultyManager.MaxSidewaysOffset;
        int obstacleFrequency = DifficultyManager.ObstacleFrequency;
        int specialPlatformsFrequency = DifficultyManager.SpecialPlatformFrequency;
        #endregion

        //Use the next design
        newdesign++;
        if (newdesign > 0)
        {
            PreviousDesign = newdesign - 1;

        }

        if (newdesign == ObjectPools.Length)
        {
            SpawnFinishLine();
            return;
        }

        //Move startpoint forward to where next section is supposed to begin
        if (ZPos != 0)
        {
            StartGenerateAtThisPoint += (BGPlane_Lenght + TransitionalPlaneLenght);
        }

        //Keep track of quantity o spawned objects for testing/Feedback
        //int spawnedplatforms = 0;
        //int spawnedObstacles = 0;
        //int spawnedspecial = 0;

        #region Generation loop

        int endpointofchunk = StartGenerateAtThisPoint + (BGPlane_Lenght + TransitionalPlaneLenght);
        int iteration = 1;
        while (ZPos < endpointofchunk)
        {
            int ForwardOffset = Random.Range(minOffsetOnZ, maxOffsetOnZ);
            int SideOffset = Random.Range(minOffsetOnx, maxOffsetOnX);

            //Add the offset to the tracker for the next platform-spawn
            ZPos += ForwardOffset;
            XPos = SideOffset;

            //Fetches 5 subsequent locations between platforms to use when placing pickups
            if (iteration >= 10 && iteration <= 14)
            {
                //Vector3 pickuppos = new Vector3(SideOffset, 1.5f, ZPos + (ForwardOffset/2f));
                LifeUpPositions.Add(new Vector3(SideOffset, 1.5f, ZPos + (ForwardOffset / 2f)));
            }

            if (iteration % specialPlatformsFrequency == 0)
            {
                GameObject obj = ObjectPools[newdesign].GetPooledObject("Platforms_Special");
                SpawnObject(obj);
                //spawnedspecial++;
            }
            else
            {
                GameObject obj = ObjectPools[newdesign].GetPooledObject("Platforms");
                SpawnObject(obj);
                //spawnedplatforms++;
            }

            if (iteration % obstacleFrequency == 0)
            {
                GameObject obj = ObjectPools[newdesign].GetPooledObject("Obstacles");
                SpawnObject(obj);

                //spawnedObstacles++;
            }

            iteration++;
        }

        #endregion

        //Debug.Log("Spawned platforms this chunk: " + spawnedplatforms);
        //Debug.Log("Spawned special this chunk: " + spawnedspecial);
        //Debug.Log("Spawned obstacles this chunk: " + spawnedObstacles);

        #region Transition plane setup and spawn

        //Set textures for transition plane fetched from prefabs used in previous/this chunk
        TransPlaneRenderer.material.SetTexture("_MainTex",
        ObjectPools[newdesign].Designer.EnvironmentPlane.GetComponent<Renderer>().sharedMaterial.GetTexture("_MainTex"));
        TransPlaneRenderer.material.SetTexture("_DetailTex",
        ObjectPools[PreviousDesign].Designer.EnvironmentPlane.GetComponent<Renderer>().sharedMaterial.GetTexture("_MainTex"));

        //Place transitional plane
        TransPlane.transform.position = new Vector3(0, -50, StartGenerateAtThisPoint);

        #endregion


        //Place Environment plane
        GameObject EnvironmentPlane = ObjectPools[newdesign].EnvPlane;
        if (EnvironmentPlane != null)
        {
            EnvironmentPlane.transform.position = new Vector3(0, EnvironmentPlane.transform.position.y, StartGenerateAtThisPoint + TransitionalPlaneLenght);
            EnvironmentPlane.SetActive(true);
        }


        //PowerUp set
        for (int i = 0; i < 2; i++)
        {
            PowerUpPositions.Add(new Vector3(0, 2, StartGenerateAtThisPoint + (100*(i+1))));
        }

        //Set stuff that happens on the next section-enter
        SectionTriggerscript.SetNextPickupsSpawns(LifeUpPositions, PowerUpPositions);
        SectionTriggerscript.SetNextBulletDesign(ObjectPools[newdesign].Bullet);
        SectionTriggerscript.SetNextSectionTrigger(new Vector3(0, 0, endpointofchunk));
        
        //Set position for the next GenTrigger
        GenTrigger.transform.position = new Vector3(0, 0, StartGenerateAtThisPoint + (TransitionalPlaneLenght + 20));

    }

    public void DeactivatePreviousObjects()
    {
        if (PreviousDesign != newdesign)
        {
            ObjectPools[PreviousDesign].DeactivateObjects();
        }
    }

    private void SpawnObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.position = new Vector3(XPos, obj.transform.position.y, ZPos);
            obj.SetActive(true);
        }
    }

    private void SpawnFinishLine()
    {
        GameObject finishplane = SinglePool.FinishPlanInstance;

        finishplane.SetActive(true);
        finishplane.transform.position = new Vector3(0, 0, ZPos + 8);


    }
}