using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public float lifeTime = 1f;
    public float speed = 10f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // ѕр€мое перемещение в заданном направлении
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet"))
            return;

        if (collision.CompareTag("Enemy"))
        {
            var enemyComponent = collision.GetComponent<enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}