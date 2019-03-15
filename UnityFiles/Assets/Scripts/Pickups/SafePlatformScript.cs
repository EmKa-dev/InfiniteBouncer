using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlatformScript : MonoBehaviour
{
    Transform PlayerT;



    private void OnEnable()
    {
        if (PlayerT == null)
        {
            PlayerT = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void StartFollowPlayer()
    {
        gameObject.SetActive(true);
        StartCoroutine(SafePlatformFollowPlayer());

    }

    private IEnumerator SafePlatformFollowPlayer()
    {
        bool t = true;
        float timer = 0;
        while (t)
        {
            timer += Time.deltaTime;
            this.transform.position = new Vector3(PlayerT.position.x, 0, PlayerT.position.z);

            if (timer > 5f)
                t = false;
            else
                yield return null;
        }
        gameObject.SetActive(false);
    }
}
