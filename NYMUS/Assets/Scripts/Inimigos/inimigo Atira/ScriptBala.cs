using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptBala : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            InimigoAtiraControlador.acertouJogador = true;
            //Debug.Log("projetil acertou o jogador");
        }
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }
}
