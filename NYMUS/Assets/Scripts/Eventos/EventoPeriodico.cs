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

    [Header("Aleatório")]
    public bool aleatorio = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicia a Coroutine que aciona o evento periodicamente
        StartCoroutine(AcionarEventoPeriodicamente());
    }

    // Coroutine que aciona o evento periodicamente
    private IEnumerator AcionarEventoPeriodicamente()
    {
        while (ligado)
        {
            float intervalo = aleatorio ? Random.Range(0, tempoSegundos) : tempoSegundos;
            yield return new WaitForSeconds(intervalo);
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
