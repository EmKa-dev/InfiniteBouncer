using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public Transform target;

    public Vector3 Velocity = Vector3.zero;
    public float smoothtime = 0.3f;
    public int CameraOffsetz = -4;
    public int CameraOffsety = 10;
    public bool smoothing;
	
	void FixedUpdate ()
    {
        //Camera pan for testing
        //transform.position += new Vector3(0,0,5) * Time.smoothDeltaTime;
        
        Vector3 targetpos;
        targetpos.z = target.transform.position.z + CameraOffsetz;
        targetpos.x = target.transform.position.x;
        targetpos.y = CameraOffsety;


        if (smoothing)
        {
            //transform.position = Vector3.Lerp(transform.position, newpos, cameraspeed * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref Velocity, smoothtime);
        }
        else
        {
            transform.position = targetpos;
        }
    }
}
