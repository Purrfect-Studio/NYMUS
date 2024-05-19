    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    [Header("Projetil do Prefab")]
    public GameObject balaProjetil;

    [Header("GameObject da Arma")]
    public Transform arma; // Posição de onde o projétil será disparado

    [Header("Sobre o Tiro")]
    public float velocidadeTiro; // Força do tiro
    public int tempoTiro = 3; // Tempo que o tiro fica no ar até ser destruído 

    public void AtirarProjétil()
    {
        // Instancia o projétil e define sua posição e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiro, 0);
        // Destroi o projétil depois de um tempo para evitar vazamento de memória
        Destroy(temp.gameObject, tempoTiro);
    }
}
