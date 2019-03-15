using UnityEngine;

//A temporary solution for tweaking values in the editor.
public class BackAndForthScript : MonoBehaviour
{
    float Speed;

    float SidewaysMin;
    float SidewaysMax;

    private void OnEnable()
    {
        Speed = DifficultyManager.MovingPlatformSpeed;
        SidewaysMin = DifficultyManager.MinSidewaysOffset;
        SidewaysMax = DifficultyManager.MaxSidewaysOffset;
    }

    void Update()
    {
        transform.position = new Vector3(PingPong(Time.time * Speed, SidewaysMin, SidewaysMax), 0, transform.position.z);
    }

    float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
}
