using UnityEngine;

public class WindScript : MonoBehaviour
{

    internal int Strenght;
    internal Vector3 direction;

    Transform ParentTransform;

    private void Awake()
    {
        ParentTransform = transform.parent.GetComponent<Transform>();
    }

    private void OnEnable()
    {

        Strenght = DifficultyManager.Windstrenght;

        //Randomize direction of wind
        int dirchoice = Random.Range(0, 2);

        if (dirchoice == 1)
        {
            Vector3 rightpos = ParentTransform.position;
            rightpos.x += -2;
            direction = Vector3.right;
            ParentTransform.localEulerAngles = new Vector3(0, 90, 0);
            ParentTransform.position = rightpos;
        }
        else if (dirchoice == 0)
        {
            Vector3 leftpos = ParentTransform.position;
            leftpos.x += 2;
            direction = Vector3.left;
            ParentTransform.localEulerAngles = new Vector3(0, -90, 0);
            ParentTransform.position = leftpos;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.GetComponent<Rigidbody>().AddForce(direction * Strenght, ForceMode.Impulse);
        }
        else
        {
            other.GetComponent<Rigidbody>().AddForce(direction * Strenght, ForceMode.VelocityChange);
        }
    }
}
