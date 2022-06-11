using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform linkedPortal;
    private gameManager gm;
    private void Start()
    {
        gm = GameObject.FindObjectOfType<gameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.Teleport(linkedPortal, collision.gameObject.transform);
    }
}
