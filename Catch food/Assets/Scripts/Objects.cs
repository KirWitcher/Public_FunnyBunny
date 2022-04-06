using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimeToDie());
    }
    //Уничтожение объектов после прикосновения игрока
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
    //Объекты сами уничтожаются после 4с
    IEnumerator TimeToDie()
    {
        while(true)
        {
            yield return new WaitForSeconds(4);
            Destroy(gameObject);
        }
    }
}
