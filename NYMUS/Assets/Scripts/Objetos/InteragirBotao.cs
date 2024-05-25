using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirBotao : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private PlayerControlador jogador;
    [SerializeField] private UnityEvent botaoApertado;

    public bool podeExecutar;

    [Header("Tecla")]
    public GameObject tecla;
    private SpriteRenderer spriteTecla;

    private void Start()
    {
        podeExecutar = false;
        if (tecla != null)
        {
            spriteTecla = tecla.GetComponent<SpriteRenderer>();
            spriteTecla.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (jogador.estaInteragindo && podeExecutar)
        {
            botaoApertado.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            if(tecla != null)
            {
                spriteTecla.enabled = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //Entrou no range de interação
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            podeExecutar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Saiu do range de interação
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            podeExecutar = false;
            if (tecla != null)
            {
                spriteTecla.enabled = false;
            }
        }
    }
}
