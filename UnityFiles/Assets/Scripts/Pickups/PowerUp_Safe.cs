using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Safe : MonoBehaviour, IPowerUp
{
    SafePlatformScript SafePlatform;

    public TypeOfPowerUp Type {get { return TypeOfPowerUp.Safe; } }

    private void Start()
    {
        SafePlatform = SinglePool.SafePlatformInstance.GetComponent<SafePlatformScript>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 8)
        {
            PlayerController playerobj = col.GetComponent<PlayerController>();
            playerobj.HeldPowerUp = this;

            gameObject.SetActive(false);
            //gameObject.transform.position = Vector3.zero;

            UIManager.UpdateUI_PowerUp(Type);
        }
    }

    public void Use()
    {
        SafePlatform.StartFollowPlayer();
    }
}
