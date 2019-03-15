using UnityEngine;

public class GenerateNewChunkTriggerScript : MonoBehaviour
{
    GenerateCourseScript GenScript;
    DifficultyManager DiffMan;
    PlayerController PlayerCon;

    private void Start()
    {
        GenScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenerateCourseScript>();
        DiffMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DifficultyManager>();
        PlayerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 8)
        {
            PlayerCon.ChangeSpawnPoint(transform.position);
            DiffMan.IncrementAndUpdateDifficulties();         
            GenScript.DeactivatePreviousObjects();
            GenScript.Generate();
        }
    }
}
