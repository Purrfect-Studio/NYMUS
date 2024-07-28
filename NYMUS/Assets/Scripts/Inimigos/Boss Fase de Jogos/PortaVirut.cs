using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaVirut : MonoBehaviour
{
    [Header("Mecanica de causar dano no Boss")]
    public GameObject[] portas;
    public float tempoParaDesativarPorta;
    public float cooldownParaInvocarPorta;
    private float cooldownRestanteParaInvocarPorta;
    private bool portaInvocada;
    private bool jogadorFechouPorta;
    private Collider2D collider2DPortas;
    private Animator animatorPortas;


    // Start is called before the first frame update
    void Start()
    {
        cooldownRestanteParaInvocarPorta = cooldownParaInvocarPorta;

        for (int i = 0; i < portas.Length; i++)
        {
            collider2DPortas = portas[i].GetComponent<BoxCollider2D>();
            collider2DPortas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestanteParaInvocarPorta -= Time.deltaTime;

        if(MovimentacaoBoss.podeMover == false)
        {
            cooldownRestanteParaInvocarPorta = cooldownParaInvocarPorta;
        }
        if (cooldownRestanteParaInvocarPorta <= 0 && portaInvocada == false && BossControlador.podeExecutarAcoes == true)
        {
            AtivarPorta(EscolherPorta());
            cooldownRestanteParaInvocarPorta = cooldownParaInvocarPorta;
            portaInvocada = true;
        }
    }

    public int EscolherPorta()
    {
        return Random.Range(0, portas.Length);
    }

    public void AtivarPorta(int indexPorta)
    {
        jogadorFechouPorta = false;
        animatorPortas = portas[indexPorta].GetComponent<Animator>();
        animatorPortas.SetTrigger("AbrirPorta");
        collider2DPortas = portas[indexPorta].GetComponent<BoxCollider2D>();
        collider2DPortas.enabled = true;
        StartCoroutine(DesativarPorta(indexPorta));
    }

    IEnumerator DesativarPorta(int indexPorta)
    {
        yield return new WaitForSeconds(tempoParaDesativarPorta);
        if (jogadorFechouPorta == false)
        {
            desativaPorta(indexPorta);
        } 
    }

    public void desativaPorta(int indexPorta)
    {
        animatorPortas = portas[indexPorta].GetComponent<Animator>();
        animatorPortas.SetTrigger("FecharPorta");
        collider2DPortas = portas[indexPorta].GetComponent<BoxCollider2D>();
        collider2DPortas.enabled = false;
        portaInvocada = false;
    }

    public void jogadorDesativaPorta(int indexPorta)
    {
        jogadorFechouPorta = true;
        animatorPortas = portas[indexPorta].GetComponent<Animator>();
        animatorPortas.SetTrigger("FecharPorta");
        collider2DPortas = portas[indexPorta].GetComponent<BoxCollider2D>();
        collider2DPortas.enabled = false;
        portaInvocada = false;
    }
}
