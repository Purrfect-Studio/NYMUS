using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fechadura : MonoBehaviour
{

    public bool travado = true;
    public UnityEvent eventoDestravado;

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void destravar ()
    {
        if (travado && Inventario.chavesAtual>0)
        {
            travado = false;
            eventoDestravado.Invoke();
        }
    }

    public void travar ()
    {
        travado = true;
    }
}
