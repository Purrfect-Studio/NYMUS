using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarEmOnda : MonoBehaviour
{
    [Header("Projetil do Prefab")]
    public GameObject balaProjetil;

    [Header("GameObject da Arma")]
    public Transform arma; // Posição de onde o projétil será disparado

    [Header("Velocidade do Tiro")]
    public float velocidadeTiro;
    [SerializeField] public static float velocidadeTiroX; // Força do tiro
    public void AtirarProjétil()
    {
        velocidadeTiroX = velocidadeTiro;
        // Instancia o projétil e define sua posição e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiroX, 0);
        // Destroi o projétil depois de um tempo para evitar vazamento de memória
        Destroy(temp.gameObject, 3f);
    }
}
