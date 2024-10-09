using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirComCooldown : MonoBehaviour
{

    public void DestruirDepoisDeUmTempo(float tempo)
    {
        StartCoroutine(Destruir(tempo));
    }

    IEnumerator Destruir(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Destroy(gameObject);
    }
}
