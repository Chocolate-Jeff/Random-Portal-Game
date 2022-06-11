using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public playerController player;
    public float portalDelay;
    private float tempPortalDelay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<playerController>();
        tempPortalDelay = portalDelay;
        portalDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        portalDelay -= Time.deltaTime;
    }
    public void Teleport(Transform lp, Transform objectToTeleport)
    {
        if (portalDelay <= 0)
        {
            objectToTeleport.position = lp.position;
            portalDelay = tempPortalDelay;
        }
    }
}
