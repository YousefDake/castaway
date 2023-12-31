using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject aim;

    private float cd = 2f;
    public static PlayerAttack Instance { get; private set; }

    private void Awake()
    {
        aim = GameObject.FindWithTag("Aim");
    }

    

  

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(8);
        }
    }
}
