using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    [Header("Dialogo")]
    public List<Fala> falas;

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
        if (!travado)
        {
            dialogoControlador.IniciarDialogo(falas);
            travarDialogo();
        }
    }
}
