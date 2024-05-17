using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroTeste : MonoBehaviour
{
    [Header("Projetil do Prefab")]
    public GameObject balaProjetil;

    [Header("GameObject da Arma")]
    public Transform arma; // Posi��o de onde o proj�til ser� disparado

    [Header("Velocidade do Tiro")]
    public float velocidadeTiro; // For�a do tiro

    public void AtirarProj�til()
    {
        // Instancia o proj�til e define sua posi��o e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiro, 0);
        // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
        Destroy(temp.gameObject, 3f);
    }
}
