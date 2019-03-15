using UnityEngine;

public class SandsplashOnHit : MonoBehaviour
{

    ParticleSystem sys;

	void Start()
    {
        sys = SinglePool.SandsplashInstance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Vector3 newpos = new Vector3();

        newpos.x = collision.transform.position.x;
        newpos.z = collision.transform.position.z;
        newpos.y = 0.5f;

        sys.transform.position = newpos;

        if (!sys.isPlaying)
        {
            sys.Emit(25);
        }
    }
}
