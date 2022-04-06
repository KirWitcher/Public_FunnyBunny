using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_controller : MonoBehaviour
{
    //Переменные для перемещения
    public float speed;
    public float jumpForce;
    private float moveImput;
    private bool facingRight = true;

    //Переменные прыжка
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Здоровье
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody2D rb;
    private Animator anim;
    public Joystick joystick;

    //Счет звезд
    public Text _moneyText;
    public Text menuScore;
    public int star_score;

    //Пауза
    public static bool GameIsPaused = false;
    public GameObject _pauseMenu;
    public GameObject panel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;//Движение в обычном времени
    }

    private void FixedUpdate()
    {
        //moveImput = Input.GetAxis("Horizontal"); //Версия для ПК
        //Управление с джойстика
        moveImput = joystick.Horizontal;
        rb.velocity = new Vector2(moveImput * speed, rb.velocity.y);

        //Проверка в какую сторону мы смотрим и идем, чтобы повернуться
        if(facingRight == false && moveImput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveImput < 0)
        {
            Flip();
        }

        //Проверка стоит игрок или бежит для анимации
        if(moveImput == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        //Здоровье игрока
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        //Когда уменьшается здоровье спрайт сердца меняется на пустое, когда здоровье не меняется, то спрайт полного сердечка так и остается
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void Update()
    {

        float verticalMove = joystick.Vertical;//Прыжок через джойстик

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);//Указываем что у нас будет полом, для проверки

        //Если джойстик вверх, то прыжок и анимация
        if(isGrounded == true && verticalMove >= 0.5f)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("TakeOff");
        }
        //Отключаем анимацию прыжка наземле
        if(isGrounded == true)
        {
            anim.SetBool("IsJump", false);
        }
        else
        {
            anim.SetBool("IsJump", true);
        }

        //Панель Game Over
        if(health <= 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }

        //Меню паузы
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //Счет игрока во время игры и когда игра окончена
        menuScore.text = "Your score: " + star_score;
        _moneyText.text = "Score: " + star_score;
        
    }
    //Разворот игрока
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if(moveImput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(moveImput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    //Все колизии: отнимание xp от врагов, хилл от еды и увеличение счета игрока
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            health--;
        }
        if(other.gameObject.tag == "Star")
        {
            star_score++;
        }
        if(other.gameObject.tag == "Food")
        {
            health++;
            if(health >= numOfHearts)
            {
                health = numOfHearts;
            }
        }
    }

    //Поуза и продолжение игры
    void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;//остановка времени
        GameIsPaused = true;
    }

}
