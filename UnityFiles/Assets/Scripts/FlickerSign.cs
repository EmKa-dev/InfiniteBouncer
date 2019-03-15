using UnityEngine;

public class FlickerSign : MonoBehaviour
{

    TextMesh sign;

    bool IsLit;

    [SerializeField]
    Color LitColor;

    Color UnlitColor = Color.black;

    void Start ()
    {
       sign = GetComponent<TextMesh>();
       sign.color = LitColor;
	}
	
    void Update()
    {
        if (Random.value > 0.92) //a random chance
        {
            if (IsLit) //if the light is on...
            {
                sign.color = UnlitColor; //turn it off
                IsLit = false;
            }
            else
            {
                sign.color = LitColor; //turn it on
                IsLit = true;
            }
        }
    }
}
