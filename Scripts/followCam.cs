using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour
{
    public Transform target;
    private Vector3 cVel = Vector3.zero;
    public float smoothTimeForCam;
    public Vector3 offset = new Vector3(0, 0, -10); 
    // Update is called once per frame after all other update
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cVel, smoothTimeForCam);
    }
}
