using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float MAX_TTL = 4f;
    public int damage = 1;
    private Vector2 _startPosition;
    private float _timeLived;
    public Vector2 Target { get; set; }
    public float Speed { get; set; } = 2;

    private void Start()
    {
        _startPosition = transform.position;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageableObject = collision.gameObject.GetComponent<IDamageable>();
        damageableObject?.TakeDamage(damage);
        Destroy(gameObject);
    }
}