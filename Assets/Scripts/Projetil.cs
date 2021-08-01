using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float destroyTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerControl>().shield.activeInHierarchy)
            {
                collision.GetComponentInParent<PlayerControl>().shield.SetActive(false);
            }
            else
            {
                collision.GetComponent<PlayerControl>().TomouDano();

            }

            Destroy(gameObject);
        }
    }
}
