using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Target { get; set;}
    public float Speed { get; set; } = 2;

    private bool bHit;
    private Vector2 debug_StartPos;

    void Start()
    {
        bHit = false;
        debug_StartPos = transform.position;
    }

    void OnCollisionEnter()
    {
        bHit = true;
    }

    void Update()
    {
        if (!bHit){
            transform.Translate((Target - new Vector2(debug_StartPos.x, debug_StartPos.y)).normalized * Speed * Time.deltaTime);
        } else {
            Destroy(this);
        }
    }
}
