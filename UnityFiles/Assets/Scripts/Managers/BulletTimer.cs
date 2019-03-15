using UnityEngine;

public class BulletTimer : MonoBehaviour
{

    Transform PlayerTransform;
    internal GameObject Bullet;

    public float FireTimer = 5;

    float Timer;

    bool ActiveBullet = false;

	void Start ()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Bullet.SetActive(false);
	}
	
	void Update ()
    {
        Timer += Time.deltaTime;

        if (ActiveBullet)
        {
            if (Bullet.transform.position.z < PlayerTransform.position.z -10)
            {
                Bullet.SetActive(false);
                ActiveBullet = false;
                Timer = 0;

            }
        }
        else if (!ActiveBullet && Timer >= FireTimer)
        {
            ActiveBullet = true;
            FireBullet();
        }
	}

    void FireBullet()
    {
        Vector3 bulletstartpos;

        bulletstartpos.x = PlayerTransform.position.x;
        bulletstartpos.z = PlayerTransform.position.z + 35;
        bulletstartpos.y = 2;

        Bullet.transform.position = bulletstartpos;
        Bullet.SetActive(true);
    }
}
