using UnityEngine;

public class BoarMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private bool isMovingRight = true;
    private Rigidbody2D rb;
    private Vector3 defaultScale; // ��������� ������� �� ��� x

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    private float lerpTime = 0.1f; // ����� ��� �������� ��������� �����������
    private float t = 0f; // ������� ����� ������������

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
            t = 0f; // ���������� ����� ������������
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

        t += Time.deltaTime / lerpTime; // ����������� ����� ������������
    }

}
