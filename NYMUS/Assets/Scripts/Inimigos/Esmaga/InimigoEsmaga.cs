using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoEsmaga : MonoBehaviour
{
    public GameObject inimigoEsmaga;
    public Vector2 posicaoInicial;
    private bool estaMovendo;
    public float velocidadeVoltar;
    public float velocidadeDescer = 7;

    [Header("Armadilha ou Inimigo")]
    public bool armadilha;
    public bool inimigo;

    private void Start()
    {
        posicaoInicial = transform.position;
        inimigoEsmaga = gameObject;
    }

    void Update()
    {
        if (estaMovendo == true && inimigo)
        {
            inimigoEsmaga.transform.position = Vector2.MoveTowards(inimigoEsmaga.transform.position, posicaoInicial, velocidadeVoltar * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            inimigoEsmaga.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            inimigoEsmaga.GetComponent<Rigidbody2D>().gravityScale = velocidadeDescer;
            inimigoEsmaga.GetComponent<Rigidbody2D>().mass = 400;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            inimigoEsmaga.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            inimigoEsmaga.GetComponent<Rigidbody2D>().gravityScale = 0;
            inimigoEsmaga.GetComponent<Rigidbody2D>().mass = 0;

            if (inimigo)
            {
                estaMovendo = true;
            }
            if(armadilha)
            {
                StartCoroutine("Desativar");
            }
        }
    }

    IEnumerator Desativar()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = posicaoInicial;
        gameObject.SetActive(false);
    }
}
