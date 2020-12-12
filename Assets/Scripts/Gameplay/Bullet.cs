using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Target { get; set;}
    public float Speed { get; set; } = 2;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate((Target - new Vector2(startPosition.x, startPosition.y)).normalized * Speed * Time.deltaTime);
    }
}
