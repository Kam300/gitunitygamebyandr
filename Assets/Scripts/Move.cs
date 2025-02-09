using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using YandexMobileAds.Base;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb; // Добавил Rigidbody2D

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool isFacingRight = false;

    // Добавьте эти переменные в начало класса
    [SerializeField] private ParticleSystem deathEffect; // Система частиц для эффекта смерти
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer
    private bool isDying = false; // Флаг процесса смерти
    private float deathRotationSpeed = 540f; // Скорость вращения при смерти
    private float deathFadeSpeed = 2f; // Скорость исчезновения спрайта

    public int curHp;
    int maxHp = 3;
    bool isHit = false;
    bool canHit = true;
    public Joystick joystick;
    public GameObject Overgame;
    public GameObject Overgame2;

    // Стрельба
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float shootingForce = 10f;
    public float shootingCooldown = 0.5f;
    private bool canShoot = true;
    bool fire = false;
    public GameObject button;
    public Text Count;
    public Bullet Butt;
    public bool tt = false;
    private bool isInvincible = false; // Флаг для защиты от урона

    public int maxBulletCount = 3; // Максимальное количество пуль
    public int bulletCount;


    public bool aaa = true;
    public Vector2 startPosition; // Начальная позиция игрок

    public OpenPanel openPanel;

    public GameObject zzzachit;
    public GameObject ovrgame1;
    public GameObject ovrgame2;
    public GameObject cont;
    public Text timerText; // Добавляем ссылку на текст таймера
                           // Добавьте эти переменные в класс Move
    private Vector2 lastMoveDirection = Vector2.right; // Сохраняем последнее направление движения
    public bool bull = false;

    public GameObject ADbutton;
    public GameObject ADbutton2;
    public bool ADButtonHP = false;
    public bool ADButtonBl = false;

    [SerializeField]
    private AudioSource shootSound; // Компонент для проигрывания звука
    [SerializeField]
    private AudioClip shootClip; // Звуковой файл выстрела
    [SerializeField]
    private AudioSource hitSound; // Компонент для проигрывания звука урона
    [SerializeField]
    private AudioClip hitClip; // Звуковой файл получения урона

    private Vector2 lastValidDirection = Vector2.right; // Добавьте это поле

    [SerializeField] private Button FistButtonRewordlife;
    [SerializeField] private Button FistButtonReword1life;
    [SerializeField] private Button SecondButtonRewordball;

    [SerializeField] private Button SecondButtonRewordtime;
    void Start()
    {
        isFacingRight = transform.localScale.x < 0; // Устанавливаем начальное направление
        // Добавьте в существующий Start()
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (deathEffect != null)
        {
            deathEffect.Stop(); // Останавливаем систему частиц при старте
        }
        if (YandexGame.SDKEnabled)
        {
            // Реклама инициализирована
            Debug.Log("SDK initialized");
        }

        ADButtonHP = true;
        ADButtonBl = true;
        // Butt = GameObject.FindGameObjectWithTag("Player").GetComponent<Bullet>();
        // joystick = GameObject.FindGameObjectWithTag("JOISTIC").GetComponent<Joystick>();

        if (bulletCount < 1)
        {

            bull = true;
            bulletCount = maxBulletCount;
        }


        openPanel = FindObjectOfType<OpenPanel>(); // Находим объект OpenPanel в сцене
        if (openPanel == null)
        {
            Debug.LogError("OpenPanel не найден в сцене!");
        }
        if (shootSound == null)
        {
            shootSound = GetComponent<AudioSource>();
        }
        if (hitSound == null)
        {
            // Создаем новый AudioSource для звука урона
            hitSound = gameObject.AddComponent<AudioSource>();
            hitSound.playOnAwake = false;
        }

        FistButtonRewordlife.onClick.AddListener(call: delegate { ExampleOpenRewardAdC(id: 1); });
        FistButtonReword1life.onClick.AddListener(call: delegate { ExampleOpenRewardAdC(id: 1 ); });
        SecondButtonRewordball.onClick.AddListener(call: delegate { ExampleOpenRewardAdC(id: 3); });
        SecondButtonRewordtime.onClick.AddListener(call: delegate { ExampleOpenRewardAdC(id: 2); });

    }
    private void CheckWallSliding()
    {
        float wallCheckDistance = 0.1f;
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance);

        if (hitRight.collider != null || hitLeft.collider != null)
        {
            // Если касаемся стены, уменьшаем вертикальную скорость
            Vector2 currentVelocity = rb.velocity;
            rb.velocity = new Vector2(currentVelocity.x, currentVelocity.y * 0.8f);
        }
    }
    public void UPdownbutton()
    {
        fire = true;
    }

    void Update()
    {


        Count.text = bulletCount.ToString();
        
            joystick.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            // Стрельба
            if (Input.GetButtonDown("Fire2") && bulletCount >= 1)
            {



                bulletCount--;
                Shoot();


            }
        
       
            joystick.gameObject.SetActive(true);
            button.gameObject.SetActive(true);

            // Стрельба
            if (fire == true)
            {
                if (bulletCount >= 1)
                {
                    bulletCount--;
                    Shoot();
                    print("выстрел");
                   
                }
                fire = false;
            }
        



            joystick.gameObject.SetActive(true);

        

        if (!enabled) return; // Добавьте эту проверку в начале Update

        

       
       
            animator.SetFloat("Run", Mathf.Abs(joystick.Horizontal));

            // Изменённая логика поворота для мобильных устройств
            if (joystick.Horizontal > 0.2f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFacingRight = true;
            }
            else if (joystick.Horizontal < -0.2f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFacingRight = false;
            }
            if (Time.timeScale == 0f)
            {
                if (joystick != null)
                {
                    ResetJoystick();
                    enabled = false;
                }
            }
            else
            {
                if (joystick != null)
                {
                    enabled = true;
                }
            }
        
    }
    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        
            movement = new Vector2(joystick.Horizontal * speed, joystick.Vertical * speed);
        

        // Применяем движение
        rb.velocity = movement;

        // Сохраняем направление движения, если оно не нулевое
        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement.normalized;
        }
        CheckWallSliding();
    }
    private void ResetJoystick()
    {
        if (joystick != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            joystick.OnPointerUp(pointerEventData);
        }
    }

    //private void Flip()
    //{
    //    isFacingRight = !isFacingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x = Mathf.Abs(theScale.x) * (isFacingRight ? 1 : -1);
    //    transform.localScale = theScale;
    //}


    public void RecountHp(int deltaHp)  // Здоровье
    {
        print(curHp);
        if (!canHit || isInvincible) return; // Если игрок не может получить урон или защита активна, выходим из метода

        // Проигрываем звук получения урона, если урон отрицательный (получение урона)
        if (deltaHp < 0 && hitSound != null && hitClip != null)
        {
            hitSound.PlayOneShot(hitClip);
        }

        curHp += deltaHp; // Обновляем текущее значение здоровья игрока
        curHp = Mathf.Clamp(curHp, 0, maxHp); // Ограничиваем текущее здоровье в пределах от 0 до максимального значения

        // Выводим текущее значение здоровья в консоль

        if (curHp <= 0 && !isDying)
        {
            startPosition = transform.position;
            canHit = false;
            GetComponent<Collider2D>().enabled = false;
            ADButtonHP = false;
            ADButtonBl = false;

            // Запускаем эффект смерти вместо мгновенного вызова Lose
            StartCoroutine(DeathEffect());
        }
        else
        {
            ADButtonHP = true;
            ADButtonBl = true;
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(OnHit());
            }
            else
            {
                StopCoroutine(OnHit());
                StartCoroutine(OnHit());
            }
        }
    }
    // Добавьте новый корутин для эффекта смерти
    private IEnumerator DeathEffect()
    {
        isDying = true;
        rb.velocity = Vector2.zero; // Останавливаем движение

        // Запускаем систему частиц
        if (deathEffect != null)
        {
            deathEffect.transform.position = transform.position;
            deathEffect.Play();
        }

        // Анимация смерти
        float alpha = 1f;
        Vector3 originalScale = transform.localScale;

        while (alpha > 0)
        {
            // Вращение
            transform.Rotate(Vector3.forward, deathRotationSpeed * Time.deltaTime);

            // Уменьшение размера
            transform.localScale = originalScale * alpha;

            // Прозрачность
            alpha -= Time.deltaTime * deathFadeSpeed;
            Color currentColor = spriteRenderer.color;
            currentColor.a = alpha;
            spriteRenderer.color = currentColor;

            yield return null;
        }

        // Ждем немного, чтобы частицы успели проиграться
        yield return new WaitForSeconds(0.5f);

        // Вызываем Lose после завершения анимации
        Lose();

        // Сбрасываем состояние объекта
        transform.rotation = Quaternion.identity;
        transform.localScale = originalScale;
        Color resetColor = spriteRenderer.color;
        resetColor.a = 1f;
        spriteRenderer.color = resetColor;
        isDying = false;
    }


    IEnumerator OnHit()
    {
        GetComponent<SpriteRenderer>().color = Color.red; // Изменяем цвет игрока на красный

        yield return new WaitForSeconds(1f); // Время, в течение которого игрок будет красным

        GetComponent<SpriteRenderer>().color = Color.white; // Возвращаем обычный цвет игрока
        isHit = false; // Восстанавливаем возможность игрока получать урон
    }


    public void Lose() // 
    {
        ADButtonHP = false;
        ADButtonBl = false;

        Time.timeScale = 0f;
        if (curHp <= 0)
        {

            Overgame.gameObject.SetActive(true);
        }
        else
        {

            Overgame2.gameObject.SetActive(true);

        }

    }

    public int GetHp()
    {
        return curHp;
    }


    private void Shoot()
    {

        // Проигрываем звук выстрела
        if (shootSound != null && shootClip != null)
        {
            shootSound.PlayOneShot(shootClip);
        }

        // Проверяем наличие пуль
        if (bulletCount < 0)
        {
            ADButtonBl = true;
            // Можно добавить звук или эффект отсутствия пуль
            return;
        }
        Vector2 direction;

       
            // Прямое использование направления джойстика
            direction = new Vector2(joystick.Horizontal, joystick.Vertical);

            // Если джойстик в нейтральной позиции, используем направление персонажа
            if (direction.magnitude < 0.1f)
            {
                direction = isFacingRight ? Vector2.right : Vector2.left;
            }
            else
            {
                direction = direction.normalized;
            }
        

        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);

            // Устанавливаем поворот пули
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void Flip(GameObject bullet)
    {
        bool isFacingRight = transform.right.x > 0;
        Vector3 theScale = bullet.transform.localScale;
        theScale.x *= -1;
        bullet.transform.localScale = theScale;
    }
    public void StopAllMovement()
    {
        // Обнулите все переменные движения
        rb.velocity = Vector2.zero; // если используется Rigidbody
                                    // Или другие переменные, отвечающие за движение в вашем скрипте
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }
    private IEnumerator InvincibilityCoroutine()
    {
        zzzachit.SetActive(true);
        isInvincible = true; // Включаем защиту
        Debug.Log("Invincibility activated!");


        yield return new WaitForSeconds(5f); // Ждём 5 секунд
        zzzachit.SetActive(false);
        isInvincible = false; // Выключаем защиту
        Debug.Log("Invincibility deactivated!");
    }


    public int OneGetHp()
    {
        return bulletCount;
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        // Добавляем обработчик ошибок рекламы
        
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
       
    }

    void Rewarded(int id)
    {
        // Возобновляем игру после просмотра рекламы
        Time.timeScale = 1f;

        if (id == 1)
        {
            AddLife();
        }
        else if (id == 2)
        {
            AddTime();
        }
        else if (id == 3)
        {
            RewardBullets();
        }
    }
    void ExampleOpenRewardAdC(int id)
    {
        // Ставим игру на паузу перед показом рекламы
        Time.timeScale = 0f;

        if (id == 1)
        {
            YandexGame.RewVideoShow(id);
        }
        else if (id == 2)
        {
            YandexGame.RewVideoShow(id);
        }
        else if (id == 3)
        {
            YandexGame.RewVideoShow(id);
        }
    }

    public void AddBullets(int amount)
    {
        bulletCount = Mathf.Min(bulletCount + amount, 50);
        UpdateBulletCountUI();
    }

    // Метод для обновления UI счетчика пуль
    private void UpdateBulletCountUI()
    {
        if (Count != null)
        {
            Count.text = bulletCount.ToString();
        }
    }
    public void RewardBullets()
    {
        AddBullets(5); // Добавляем 5 пуль за просмотр рекламы
        StartCoroutine(ShowBulletRewardEffect());
    }
    private IEnumerator ShowBulletRewardEffect()
    {
        // Если у вас есть текст для отображения количества пуль
        if (Count != null)
        {
            Color originalColor = Count.color;
            Count.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            Count.color = originalColor;
        }
    }

    public void AddLife()
    {

        if (curHp < maxHp) // Проверяем, что текущее количество жизней меньше максимального
        {


            curHp += 1; // Увеличиваем количество жизней на 1
            Debug.Log("Жизнь добавлена. Текущее количество жизней: " + curHp);

            // Запускаем корутину для защиты
            StartCoroutine(InvincibilityCoroutine());

            // Если игрок был в состоянии поражения, восстанавливаем управление
            if (curHp > 0 && !canHit)
            {
                // Восстанавливаем позицию игрока
                transform.position = startPosition;
                canHit = true; // Восстанавливаем возможность получать урон
                GetComponent<Collider2D>().enabled = true; // Включаем коллайдер
                rb.gravityScale = 0; // Восстанавливаем гравитацию (если она была изменена)
                GetComponent<SpriteRenderer>().color = Color.white; // Возвращаем обычный цвет
                isHit = false; // Сбрасываем состояние получения урона
            }
        }
        else
        {
            Debug.Log("У игрока максимальное количество жизней.");
        }
        ovrgame1.SetActive(false);
        ovrgame2.SetActive(false);
        cont.SetActive(true);
    }

    // Метод для добавления времени
    public void AddTime()
    {

        Debug.Log("AddTime method called");
        tt = true;



        if (openPanel != null)
        {
            // openPanel.timer = 120f; // Устанавливаем таймер на 120 секунд
            openPanel.timeWork = TimeWork.Timer; // Убедимся, что таймер активен
            openPanel.isTimerPaused = false; // Сбрасываем состояние паузы таймера
            Debug.Log($"Current time: Adding 120 seconds, New time: {openPanel.timer}");
            StartCoroutine(InvincibilityCoroutine());



            // Восстанавливаем состояние игрока
            transform.position = startPosition;
            canHit = true;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            isHit = false;

            // Скрываем панели поражения если они активны
            if (Overgame != null && Overgame.activeSelf)
            {
                Overgame.SetActive(false);
            }
            if (Overgame2 != null && Overgame2.activeSelf)
            {
                Overgame2.SetActive(false);
            }

            Debug.Log("Player state restored and game panels updated");
        }
        else
        {
            Debug.LogError("OpenPanel reference is null in AddTime method!");
        }
        ovrgame1.SetActive(false);
        ovrgame2.SetActive(false);
        cont.SetActive(true);
    }
    public void buttonad()
    {
        ADButtonHP = true;
        ADButtonBl = true;
    }

}