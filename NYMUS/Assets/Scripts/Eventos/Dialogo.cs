using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public Sprite foto;
    public string[] dialogoTexto;
    public string nomePersonagem = "NYMUS";

    public LayerMask layerPlayer;
    public float raioInteracao;
    public bool estaNoRaio;
    public bool travado;

    public DialogoControlador dc;

    private void Start()
    {
        travado = false;
        dc = FindObjectOfType<DialogoControlador>();
    }
    private void FixedUpdate()
    {
        InteragirDialogo();
    }
    private void Update()
    {
        if (estaNoRaio && travado == false)
        {
            dc.Fala(foto, dialogoTexto, nomePersonagem);
            travarDialogo();
        }
    }
    public void InteragirDialogo()
    {
        Collider2D areaInteracao = Physics2D.OverlapCircle(transform.position, raioInteracao, layerPlayer); // Cria a �rea que faz o di�logo aparecer
        if (areaInteracao != null) //encostou no player
        {
            estaNoRaio = true;
        }
        else
        {
            estaNoRaio = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
        //Este c�digo so serve para a esfera de intera��o ser vis�vel na cena.
    }
    public void travarDialogo()
    {
        travado = true;
    }
}
