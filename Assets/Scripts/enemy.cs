
using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [Header("Основные параметры")]
    [SerializeField] private GameObject drop;
    [SerializeField] private float health = 1f;
    [SerializeField] private float deathDuration = 0.5f;
    [SerializeField] private int numberOfPieces = 8;

    [Header("Параметры движения")]
    [SerializeField] private float moveSpeed = 3f; // Скорость движения
    [SerializeField] private float detectionRange = 5f; // Радиус обнаружения игрока
    [SerializeField] private float minPlayerDistance = 1f; // Минимальная дистанция до игрока

    private bool isHit = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform player;
    [Header("Параметры отталкивания")]
    [SerializeField] private float pushForce = 5f; // Сила отталкивания
    [SerializeField] private float pushDuration = 0.2f; // Длительность отталкивания


    [Header("Параметры отхода врага")]
    [SerializeField] private float retreatForce = 5f; // Сила отхода врага
    [SerializeField] private float retreatDuration = 0.3f; // Длительность отхода
    [SerializeField] private float attackCooldown = 1f; // Задержка между атаками
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
            // Получаем дистанцию до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Если игрок в радиусе обнаружения
            if (distanceToPlayer < detectionRange)
            {
                // Вычисляем направление к игроку
                Vector2 direction = (player.position - transform.position).normalized;

                // Если враг не слишком близко к игроку и не в откате, двигаемся к нему
                if (distanceToPlayer > minPlayerDistance && canAttack)
                {
                    rb.velocity = direction * moveSpeed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }

                // Поворачиваем спрайт в сторону движения
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
                // Наносим урон
                playerMove.RecountHp(-1);

                // Получаем направление от игрока к врагу для отхода
                Vector2 retreatDirection = (transform.position - collision.transform.position).normalized;

                // Получаем направление отталкивания для игрока
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                // Запускаем корутины
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

        // Проверяем, нет ли препятствий сзади
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            retreatDirection,
            1f,
            LayerMask.GetMask("Default") // Замените на ваш слой для стен
        );

        // Если есть препятствие, меняем направление отхода
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
            // Временно отключаем контроль игрока
            playerMove.enabled = false;

            // Применяем силу отталкивания
            float elapsedTime = 0f;

            while (elapsedTime < pushDuration)
            {
                // Плавно уменьшаем силу отталкивания
                float currentForce = Mathf.Lerp(pushForce, 0, elapsedTime / pushDuration);
                playerRb.velocity = pushDirection * currentForce;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Восстанавливаем контроль игрока
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

        // Создаем вспышку
        GameObject flash = CreateFlash();

        // Создаем разлетающиеся части
        CreateDeathPieces();

        // Скрываем основной спрайт
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(deathDuration);

        // Создание предмета дропа
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private GameObject CreateFlash()
    {
        // Создаем объект вспышки
        GameObject flash = new GameObject("Flash");
        flash.transform.position = transform.position;

        // Добавляем спрайт рендерер
        SpriteRenderer flashRenderer = flash.AddComponent<SpriteRenderer>();
        flashRenderer.sprite = spriteRenderer.sprite;
        flashRenderer.color = Color.white;

        // Анимация вспышки
        StartCoroutine(FlashAnimation(flash));

        return flash;
    }

    private IEnumerator FlashAnimation(GameObject flash)
    {
        SpriteRenderer flashRenderer = flash.GetComponent<SpriteRenderer>();
        float elapsed = 0;
        float flashDuration = 0.2f;

        // Увеличиваем размер и прозрачность
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

            // Добавляем компоненты
            SpriteRenderer pieceRenderer = piece.AddComponent<SpriteRenderer>();
            pieceRenderer.sprite = originalSprite;
            pieceRenderer.color = spriteRenderer.color;

            // Уменьшаем размер частей
            piece.transform.localScale = transform.localScale * 0.5f;

            // Добавляем физику
            Rigidbody2D rb = piece.AddComponent<Rigidbody2D>();

            // Случайное направление разлета
            float angle = i * (360f / numberOfPieces);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            float force = Random.Range(3f, 5f);
            rb.AddForce(direction * force, ForceMode2D.Impulse);

            // Добавляем вращение
            rb.angularVelocity = Random.Range(-360f, 360f);

            // Анимация исчезновения части
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