using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Target { get; set;}
    public float Speed { get; set; } = 2;
    public int damage = 1;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage("TakeDamage", damage);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate((Target - new Vector2(startPosition.x, startPosition.y)).normalized * Speed * Time.deltaTime);
    }
}
