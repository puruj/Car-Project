using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public WheelCollider[] WCs;
    public GameObject[] Wheels;

    public float torque = 200;
    public float maxSteerAngle = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Go(float acceleration, float steer)
    {
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        var thurstTorque = acceleration * torque;

        for(int i = 0; i < WCs.Length; i++)
        {
            WCs[i].motorTorque = thurstTorque;
            if(i < 2)
            {
                WCs[i].steerAngle = steer;
            }
            Quaternion quat;
            Vector3 pos;
            WCs[i].GetWorldPose(out pos, out quat);
            Wheels[i].transform.position = pos;
            Wheels[i].transform.rotation = quat;
        }


    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        Go(a,s);
    }
}
