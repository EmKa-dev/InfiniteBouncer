using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    Rigidbody rb;

    Vector3 BulletMovement = new Vector3(0, 0, -5);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + BulletMovement * Time.deltaTime);
    }
}
