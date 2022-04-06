using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;

    private void Start()
    {
        //player = FindObjectOfType<Player_controller>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //OnEnemyAttack();
    }

    public void TakeDamage(int damage)
    {
        health =- damage;
    }

    private void OnCollisionEnter2D(Collision2D en)
    {
        if(en.gameObject.tag == "Stop")
        {
            Destroy(gameObject);
        }
    }

    /*public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            OnEnemyAttack();
        }
    }*/

    /*public void OnEnemyAttack()
    {
        player.health -= damage;
    }*/


}
