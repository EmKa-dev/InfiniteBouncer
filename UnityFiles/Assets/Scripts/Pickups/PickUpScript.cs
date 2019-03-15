using UnityEngine;

public class PickUpScript : MonoBehaviour
{


    static int PickedUpString;

    private void OnEnable()
    {
        PickedUpString = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            gameObject.SetActive(false);
            PickedUpString++;

            if (PickedUpString == 1)
            {
                //Start fade
            }
            else if (PickedUpString == 5)   //Player must pick up all 5 to gain extra life
            {
                //Add life to UI and player
                PlayerController.ChangeLifeAction(1);
            }
        }
    }
}
