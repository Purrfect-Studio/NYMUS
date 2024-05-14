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
    public bool travado;

    [Header("Controladores do Dialogo")]
    public DialogoControlador dialogoControlador;



    private void Start()
    {
        travado = false;
        dialogoControlador = FindObjectOfType<DialogoControlador>();
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
