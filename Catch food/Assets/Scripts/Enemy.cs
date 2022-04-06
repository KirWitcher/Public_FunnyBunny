using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);     
    }


    private void OnCollisionEnter2D(Collision2D en)
    {
        if(en.gameObject.tag == "Stop")
        {
            Destroy(gameObject);//Уничтожение врагов в специальных местах
        }
    }



}
