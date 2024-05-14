using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptBala : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(VidaJogador.invulneravel == false)
            {
                InimigoAtira.acertouJogador = true;
            }
        }
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }
}
