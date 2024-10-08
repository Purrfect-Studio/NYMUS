using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class voltandoMusicas : MonoBehaviour
{
    public static bool estaVoltando = false;
    public UnityEvent eventoVoltando;
    // Start is called before the first frame update
    void Start()
    {
       if (estaVoltando == true)
       {
            eventoVoltando.Invoke();
       } 
    }

    public void ComecouAVoltar()
    {
        estaVoltando = true;
    }


}
