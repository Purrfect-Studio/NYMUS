using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminarCutscene : MonoBehaviour
{
    DialogoControlador terminadorDialogo;
    public UnityEvent eventoTerminar;

    // Start is called before the first frame update
    void Start()
    {
        terminadorDialogo = GetComponent<DialogoControlador>();
        StartCoroutine(verificarTerminacao());
    }

    IEnumerator verificarTerminacao()
    {
        yield return new WaitForSeconds(1f);
        if (terminadorDialogo.acabouDialogo == true)
        {
            eventoTerminar.Invoke();
        }
        StartCoroutine(verificarTerminacao());
    }


}
