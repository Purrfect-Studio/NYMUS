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

    [Header("Controladores do Dialogo")]
    public DialogoControlador dialogoControlador;
    public EscolherDiálogo condicao; 


    private void Start()
    {
        travado = false;
        dialogoControlador = FindObjectOfType<DialogoControlador>();
        condicao = FindObjectOfType<EscolherDiálogo>();
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

    public void falar()
    {
        if (travado == false)
        {
            dialogoControlador.Fala(sprite, dialogoTexto, nomePersonagem);
            travarDialogo();
        }         
    }
}
