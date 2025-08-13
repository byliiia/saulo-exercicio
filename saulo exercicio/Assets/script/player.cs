using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcaDoPulo = 4;

    private bool noChao = false;
    private bool andando = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        andando = false;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = true;
            andando = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = false;
            andando = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(new Vector2(0, forcaDoPulo), ForceMode2D.Impulse);
        }

        // Aqui trocamos "Andando" por "Parado"
        animator.SetBool("Parado", !andando);

        // Mantém o parâmetro "Pulo" como estava
        animator.SetBool("Pulo", !noChao);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }
}