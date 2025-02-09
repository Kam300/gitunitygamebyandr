using UnityEngine;

public class BoarMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private bool isMovingRight = true;
    private Rigidbody2D rb;
    private Vector3 defaultScale; // начальный масштаб по оси x

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    private float lerpTime = 0.1f; // ¬рем€ дл€ плавного изменени€ направлени€
    private float t = 0f; // “екущее врем€ интерпол€ции

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
            t = 0f; // —брасываем врем€ интерпол€ции
        }
    }

    void Update()
    {
        if (isMovingRight)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(moveSpeed, rb.velocity.y), t);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(-moveSpeed, rb.velocity.y), t);
        }

        t += Time.deltaTime / lerpTime; // ”величиваем врем€ интерпол€ции
    }

}
