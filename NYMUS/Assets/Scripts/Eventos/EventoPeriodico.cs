using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventoPeriodico : MonoBehaviour
{
    [Header("Intervalo entre Eventos")]
    public int tempoSegundos;

    [Header("Evento")]
    public UnityEvent evento;
    public bool ligado = true;

    // Start is called before the first frame update
    void Start()
    {
        // Inicia a Coroutine que aciona o evento periodicamente
        StartCoroutine(AcionarEventoPeriodicamente());
    }

    // Coroutine que aciona o evento a cada "tempoSegundos"
    private IEnumerator AcionarEventoPeriodicamente()
    {
        while (ligado)
        {
            yield return new WaitForSeconds(tempoSegundos);
            evento.Invoke();
        }
    }

    public void ligar()
    {
        ligado = true;
        StartCoroutine(AcionarEventoPeriodicamente());

    }
    public void desligar()
    {
        ligado = false;
    }
}
