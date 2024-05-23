using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarEmVariosPontos : MonoBehaviour
{
    [Header("Projetil do Prefab")]
    public GameObject balaProjetil;

    [Header("GameObject da Arma")]
    public Transform[] arma; // Posi��o de onde o proj�til ser� disparado

    [Header("Sobre o Tiro")]
    public float velocidadeTiro; // For�a do tiro
    [SerializeField] public static float velocidadeTiroX; // For�a do tiro
    public float duracaoDoTiro; // Tempo que o tiro fica no ar at� ser destru�do 
    public float delayParaInstanciarProjetil;
    public void AtirarProj�til()
    {
        StartCoroutine("atirar");
    }

    IEnumerator atirar()
    {
        for (int i = 0; i < arma.Length; i++)
        {
            velocidadeTiroX = velocidadeTiro;
            GameObject temp = Instantiate(balaProjetil);
            temp.transform.position = arma[i].position;
            // Define uma velocidade pro projetil
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiroX, 0);
            // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
            Destroy(temp.gameObject, duracaoDoTiro);
            // Espera um tempo antes de criar o proximo projetil
            yield return new WaitForSeconds(delayParaInstanciarProjetil);
        }
    }
}