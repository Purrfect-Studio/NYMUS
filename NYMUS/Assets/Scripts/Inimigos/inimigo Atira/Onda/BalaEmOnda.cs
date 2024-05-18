using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaEmOnda : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocidadeX;

    private void Start()
    {
        velocidadeX = AtirarEmOnda.velocidadeTiroX;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            InimigoAtiraControlador.acertouJogador = true;
        }
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        rb.velocity = new Vector2(velocidadeX, Mathf.Cos(transform.position.x) * 10);
    }
}
