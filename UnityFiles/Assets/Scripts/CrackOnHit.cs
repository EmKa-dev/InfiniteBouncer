using UnityEngine;

public class CrackOnHit : MonoBehaviour
{

    int hit = 0;

    public GameObject BrokenPlatform;

    Renderer rend;
    Texture Cracks;

    private void Start()
    {
        Cracks = SinglePool.CracksTexInstance;
        rend = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision c)
    {
        hit++;
        if (hit == 1)
        {
            rend.material.SetTexture("_SecondTex", Cracks);
        }
        else if (hit == 2)
        {
            Instantiate(BrokenPlatform, gameObject.transform.position, Quaternion.identity);

            //Deactivate and reset material
            gameObject.SetActive(false);
            hit = 0;
            rend.material.SetTexture("_SecondTex", null);
        }      
    }
}
