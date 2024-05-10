using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContatoTrigger : MonoBehaviour
{
    public UnityEvent evento;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        evento.Invoke();
    }
}
