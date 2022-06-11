using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Objects/New Enemy")]
public class EnemyObject : ScriptableObject
{
    public float speed;
    public float jumpTime;
    public float jumpForce;
}