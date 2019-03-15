using UnityEngine;

public class BrokenPlatformScript : MonoBehaviour
{
    public float Fallspeed = 1;

	void Update ()
    {
        gameObject.transform.position += Vector3.down * Fallspeed;

        if (gameObject.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
	}
}
