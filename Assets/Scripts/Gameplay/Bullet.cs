using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Target { get; set; }
    public float Speed { get; set; } = 2;
    public int damage = 1;
    private Vector2 _startPosition;
    private const float MAX_TTL = 4f;
    private float _timeLived;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageableObject = collision.gameObject.GetComponent<IDamageable>();
        damageableObject?.TakeDamage(damage);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_timeLived < MAX_TTL)
        {
            transform.Translate((Target - new Vector2(_startPosition.x, _startPosition.y)).normalized *
                                (Speed * Time.deltaTime));
            _timeLived += Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}