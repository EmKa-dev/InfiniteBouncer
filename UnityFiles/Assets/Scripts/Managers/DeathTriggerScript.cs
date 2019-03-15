using UnityEngine;

public class DeathTriggerScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        PlayerController.ChangeLifeAction(-1);
    }
}
