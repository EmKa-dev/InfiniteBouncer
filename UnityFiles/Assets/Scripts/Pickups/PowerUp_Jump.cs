using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Jump : MonoBehaviour , IPowerUp
{
    Rigidbody playerRB;

    public int JumpHeight;

    public TypeOfPowerUp Type { get { return TypeOfPowerUp.Jump; } }

    public void Use()
    {
        playerRB.velocity = new Vector3(0,JumpHeight,0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 8)
        {
            PlayerController playerobj = col.GetComponent<PlayerController>();

            playerRB = playerobj.rb;
            playerobj.HeldPowerUp = this;

            gameObject.SetActive(false);
            UIManager.UpdateUI_PowerUp(Type);
        }
    }
}
