using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().TomouDano();

        }
        else if(collision.CompareTag("Ataque"))
        {
            collision.GetComponentInParent<PlayerControl>().shield.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
