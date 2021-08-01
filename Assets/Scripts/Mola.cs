﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mola : MonoBehaviour
{
    public float impulseForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Rigidbody2D rb2d = collision.GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * impulseForce, ForceMode2D.Impulse);


        }
    }
}
