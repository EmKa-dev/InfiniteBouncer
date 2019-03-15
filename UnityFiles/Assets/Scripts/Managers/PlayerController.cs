using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static Action<int> ChangeLifeAction;

    internal Rigidbody rb;

    float MoveSpeed = 10;
    const float BounceHeight = 8;
    const float BoostedBounceHeight = 14;

    public bool Speedmode;
    bool IsPaused;

    int PlayerLives = 2;

    public Collider RespawnPlatform;

    internal IPowerUp HeldPowerUp;


    //Cached vectors
    Vector3 NormalBounceVector = new Vector3(0, BounceHeight, 0);
    Vector3 SpecialBounceVector = new Vector3(0, BoostedBounceHeight, 0);

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        ChangeLifeAction += AddorSubLife;

        if (Speedmode)
        {
            rb.isKinematic = true;
            MoveSpeed = 80;
            transform.position = new Vector3(0, 2, 0);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangeLifeAction(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HeldPowerUp != null)
            {
                HeldPowerUp.Use();
                HeldPowerUp = null;
                UIManager.UpdateUI_ClearPowerUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0f;
            }
            else if (IsPaused)
            {

                IsPaused = false;
                Time.timeScale = 1f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")) * MoveSpeed;

        rb.MovePosition(transform.position + Movement * Time.fixedDeltaTime);
    }

    //Give bouncing movement
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.layer)
        {
            //Normal platforms
            case 9:
                rb.velocity = NormalBounceVector;
                break;
            //UpBoost platforms
            case 10:
                rb.velocity = SpecialBounceVector;
                break;
            default:
                break;
        }
    }

    public void AddorSubLife(int amount)
    {
        if (amount == 1)
        {
            if (PlayerLives < 3)
            {
                PlayerLives++;
            }
        }
        else if (amount == -1)
        {
            PlayerLives--;

            if (PlayerLives > 0)
            {
                RespawnPlayer();
            }
            else if (PlayerLives <= 0)
            {
                UIManager.ShowGameOverScreen();
            }
        }
    }

    public void ChangeSpawnPoint(Vector3 point)
    {
        RespawnPlatform.transform.position = new Vector3(point.x, 5, point.z);
    }

    private void RespawnPlayer()
    {
        Vector3 re = RespawnPlatform.transform.position;
        transform.position = new Vector3(re.x, re.y + 0.5f, re.z);
        StartCoroutine(SpawnFreeze());
    }

    private IEnumerator SpawnFreeze()
    {
        bool t = true;
        Vector3 pos = transform.position;
        float timer = 0;
        while (t)
        {
            timer += Time.deltaTime;
            transform.position = pos;

            if (timer > 1.5f)
                t = false;
            else
                yield return null;
        }
    }

    private void OnDestroy()
    {
        ChangeLifeAction -= AddorSubLife;

    }
}

/*if (BounceTimer <= 0)
{
    BounceTimer = Time.time;
}
else
{
    //Debug.Log(Time.time - BounceTimer);
    BounceTimer = Time.time;
}*/