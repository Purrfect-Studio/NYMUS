using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite sprite;

    [Header("Texto do Dialogo")]
    public string[] dialogoTexto;
    public string nomePersonagem = "NYMUS";

    [Header("Layer do Jogador")]
    public LayerMask layerPlayer;

    [Header("Interacoes")]
    public float raioInteracao;
    public bool estaNoRaio;
    public bool travado;

    [Header("Controlador do Dialogo")]
    public DialogoControlador dialogoControlador;

    private void Start()
    {
        travado = false;
        dialogoControlador = FindObjectOfType<DialogoControlador>();
    }

    private void FixedUpdate()
    {
        InteragirDialogo();
    }

    private void Update()
    {
        if (estaNoRaio && travado == false)
        {
            dialogoControlador.Fala(sprite, dialogoTexto, nomePersonagem);
            travarDialogo();
        }
    }
    public void InteragirDialogo()
    {
        Collider2D areaInteracao = Physics2D.OverlapCircle(transform.position, raioInteracao, layerPlayer); // Cria a área que faz o diálogo aparecer
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
        //Este código so serve para a esfera de interação ser visível na cena.
    }

    public void travarDialogo()
    {
        travado = true;
    }
}
