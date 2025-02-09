
using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [Header("�������� ���������")]
    [SerializeField] private GameObject drop;
    [SerializeField] private float health = 1f;
    [SerializeField] private float deathDuration = 0.5f;
    [SerializeField] private int numberOfPieces = 8;

    [Header("��������� ��������")]
    [SerializeField] private float moveSpeed = 3f; // �������� ��������
    [SerializeField] private float detectionRange = 5f; // ������ ����������� ������
    [SerializeField] private float minPlayerDistance = 1f; // ����������� ��������� �� ������

    private bool isHit = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform player;
    [Header("��������� ������������")]
    [SerializeField] private float pushForce = 5f; // ���� ������������
    [SerializeField] private float pushDuration = 0.2f; // ������������ ������������


    [Header("��������� ������ �����")]
    [SerializeField] private float retreatForce = 5f; // ���� ������ �����
    [SerializeField] private float retreatDuration = 0.3f; // ������������ ������
    [SerializeField] private float attackCooldown = 1f; // �������� ����� �������
    private bool isRetreating = false;
    private bool canAttack = true;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!isHit && !isRetreating && player != null)
        {
            // �������� ��������� �� ������
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ���� ����� � ������� �����������
            if (distanceToPlayer < detectionRange)
            {
                // ��������� ����������� � ������
                Vector2 direction = (player.position - transform.position).normalized;

                // ���� ���� �� ������� ������ � ������ � �� � ������, ��������� � ����
                if (distanceToPlayer > minPlayerDistance && canAttack)
                {
                    rb.velocity = direction * moveSpeed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }

                // ������������ ������ � ������� ��������
                if (direction.x != 0)
                {
                    spriteRenderer.flipX = direction.x < 0;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minPlayerDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit && canAttack)
        {
            Move playerMove = collision.gameObject.GetComponent<Move>();
            if (playerMove != null)
            {
                // ������� ����
                playerMove.RecountHp(-1);

                // �������� ����������� �� ������ � ����� ��� ������
                Vector2 retreatDirection = (transform.position - collision.transform.position).normalized;

                // �������� ����������� ������������ ��� ������
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                // ��������� ��������
                StartCoroutine(PushPlayer(collision.gameObject, pushDirection));
                StartCoroutine(RetreatAfterAttack(retreatDirection));
                StartCoroutine(AttackCooldown());
            }
        }
    }
    private IEnumerator RetreatAfterAttack(Vector2 retreatDirection)
    {
        isRetreating = true;
        float elapsedTime = 0f;

        // ���������, ��� �� ����������� �����
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            retreatDirection,
            1f,
            LayerMask.GetMask("Default") // �������� �� ��� ���� ��� ����
        );

        // ���� ���� �����������, ������ ����������� ������
        if (hit.collider != null)
        {
            retreatDirection = Vector2.Reflect(retreatDirection, hit.normal);
        }

        while (elapsedTime < retreatDuration)
        {
            float currentForce = Mathf.Lerp(retreatForce, 0, elapsedTime / retreatDuration);
            rb.velocity = retreatDirection * currentForce;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isRetreating = false;
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private IEnumerator PushPlayer(GameObject player, Vector2 pushDirection)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Move playerMove = player.GetComponent<Move>();

        if (playerRb != null && playerMove != null)
        {
            // �������� ��������� �������� ������
            playerMove.enabled = false;

            // ��������� ���� ������������
            float elapsedTime = 0f;

            while (elapsedTime < pushDuration)
            {
                // ������ ��������� ���� ������������
                float currentForce = Mathf.Lerp(pushForce, 0, elapsedTime / pushDuration);
                playerRb.velocity = pushDirection * currentForce;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ��������������� �������� ������
            playerRb.velocity = Vector2.zero;
            playerMove.enabled = true;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isHit)
        {
            StartCoroutine(Deart());
        }
    }

    private IEnumerator Deart()
    {
        isHit = true;
        GetComponent<Collider2D>().enabled = false;

        // ������� �������
        GameObject flash = CreateFlash();

        // ������� ������������� �����
        CreateDeathPieces();

        // �������� �������� ������
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(deathDuration);

        // �������� �������� �����
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private GameObject CreateFlash()
    {
        // ������� ������ �������
        GameObject flash = new GameObject("Flash");
        flash.transform.position = transform.position;

        // ��������� ������ ��������
        SpriteRenderer flashRenderer = flash.AddComponent<SpriteRenderer>();
        flashRenderer.sprite = spriteRenderer.sprite;
        flashRenderer.color = Color.white;

        // �������� �������
        StartCoroutine(FlashAnimation(flash));

        return flash;
    }

    private IEnumerator FlashAnimation(GameObject flash)
    {
        SpriteRenderer flashRenderer = flash.GetComponent<SpriteRenderer>();
        float elapsed = 0;
        float flashDuration = 0.2f;

        // ����������� ������ � ������������
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float scale = 1 + elapsed * 2;
            flash.transform.localScale = new Vector3(scale, scale, 1);
            flashRenderer.color = new Color(1, 1, 1, 1 - (elapsed / flashDuration));
            yield return null;
        }

        Destroy(flash);
    }

    private void CreateDeathPieces()
    {
        Sprite originalSprite = spriteRenderer.sprite;

        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject piece = new GameObject("Piece");
            piece.transform.position = transform.position;

            // ��������� ����������
            SpriteRenderer pieceRenderer = piece.AddComponent<SpriteRenderer>();
            pieceRenderer.sprite = originalSprite;
            pieceRenderer.color = spriteRenderer.color;

            // ��������� ������ ������
            piece.transform.localScale = transform.localScale * 0.5f;

            // ��������� ������
            Rigidbody2D rb = piece.AddComponent<Rigidbody2D>();

            // ��������� ����������� �������
            float angle = i * (360f / numberOfPieces);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            float force = Random.Range(3f, 5f);
            rb.AddForce(direction * force, ForceMode2D.Impulse);

            // ��������� ��������
            rb.angularVelocity = Random.Range(-360f, 360f);

            // �������� ������������ �����
            StartCoroutine(PieceAnimation(piece));
        }
    }

    private IEnumerator PieceAnimation(GameObject piece)
    {
        SpriteRenderer pieceRenderer = piece.GetComponent<SpriteRenderer>();
        float elapsed = 0;

        while (elapsed < deathDuration)
        {
            elapsed += Time.deltaTime;
            pieceRenderer.color = new Color(
                pieceRenderer.color.r,
                pieceRenderer.color.g,
                pieceRenderer.color.b,
                1 - (elapsed / deathDuration)
            );
            yield return null;
        }

        Destroy(piece);
    }

    public void startDeart()
    {
        StartCoroutine(Deart());
    }

}