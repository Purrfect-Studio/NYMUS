using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    [Header("Dialogo")]
    public List<Fala> falas;

    [Header("Interacoes")]
    public bool travado;

    [Header("Controladores do Dialogo")]
    public DialogoControlador dialogoControlador;

    private void Start()
    {
        travado = false;
        dialogoControlador = FindObjectOfType<DialogoControlador>();
    }

    public void TravarDialogo()
    {
        travado = true;
    }

    public void Falar()
    {
        if (!travado)
        {
            dialogoControlador.IniciarDialogo(falas);
            TravarDialogo();
        }
    }
}
