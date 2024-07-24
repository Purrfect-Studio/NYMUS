using UnityEngine.Events;
using UnityEngine;

public class EventoStart : MonoBehaviour
{
    [Header("Evento")]
    public UnityEvent evento;
    // Start is called before the first frame update
    void Start()
    {
        evento.Invoke();
    }
}
