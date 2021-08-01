using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool jump = false;
    private float contador;

    public float moveForce = 20;
    public float maxSpeed = 15;
    public float jumpForce = 800;
    public Transform groundCheck;

    private bool grounded = false;
    private float hforce = 1;
    private bool spinDash = false;
    private Rigidbody2D rbd2d;
    private bool estaVivo = true;
    private Animator anim;

    public Rigidbody2D moedasPrefabs;
    public Transform moedasSpawner;
    private bool tomouDano = false;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        rbd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("OnGround", grounded);
        if (grounded)
        {
            anim.SetBool("Jump", false);
        }

        anim.SetBool("SpinDash", spinDash);
        if (spinDash && !tomouDano)
        {
            hforce += 0.2f;
            contador += Time.deltaTime;
            if ( contador > 1.0)
            {
                spinDash = false;
                hforce = 1;
                contador = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if (estaVivo)
        {
            anim.SetFloat("Speed", rbd2d.velocity.x);
            rbd2d.AddForce(Vector2.right * hforce * moveForce);
            if(rbd2d.velocity.x> maxSpeed)
            {
                rbd2d.velocity = new Vector2(Mathf.Sign(rbd2d.velocity.x) * maxSpeed, rbd2d.velocity.y);
            }

            if (jump && !tomouDano)
            {
                anim.SetBool("Jump", true);
                rbd2d.AddForce(new Vector2(0, jumpForce));
                jump = false;
            }
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            jump = true;
        }
    }
    public void SpinDash()
    {
        if (grounded)
        {
            spinDash = true;
        }
    }
    public void TerminouDano()
    {
        tomouDano = false;
        hforce = 1;
    }
    public void TomouDano()
    {
        if (tomouDano)
        {
            return;

        }
        else if( estaVivo && !tomouDano)
        {
            if(LevelManager.levelManager.getMoedas() > 0)
            {
                tomouDano = true;
                spinDash = false;
                jump = false;
                anim.SetBool("Jump", false);
                rbd2d.velocity = Vector2.zero;
                anim.SetTrigger("Dano");
                rbd2d.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
                hforce = 0;

                int totalDeMoedas = LevelManager.levelManager.getMoedas();
                if(totalDeMoedas >= 10)
                {
                    totalDeMoedas = 10;
                }

                LevelManager.levelManager.ResetMoedas();
                for (int i = 0; i < totalDeMoedas; i++)
                {
                    Rigidbody2D tempMoedas = Instantiate(moedasPrefabs, moedasSpawner.position, Quaternion.identity) as Rigidbody2D;
                    int ramdoForceX = Random.Range(-20, 5);
                    int ramdoForceY = Random.Range(1, 10);

                    tempMoedas.AddForce(new Vector2(ramdoForceX, ramdoForceY), ForceMode2D.Impulse);

                }

            }
            else
            {
                Morreu();
            }
        }
 
    }
    public void Morreu()
    {
        if (estaVivo)
        {
            estaVivo = false;
            spinDash = false;
            jump = false;
            anim.SetBool("Jump", false);
            rbd2d.velocity = Vector2.zero;
            rbd2d.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            anim.SetBool("Morreu", true);
            Invoke("GameOver", 2f);
        }
    }
    public void GameOver()
    {
        LevelManager.levelManager.GameOver();
    }
}
