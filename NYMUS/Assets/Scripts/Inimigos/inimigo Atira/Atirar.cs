using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    [Header("Projetil do Prefab")]
    public GameObject balaProjetil;

    [Header("GameObject da Arma")]
    public Transform arma; // Posi��o de onde o proj�til ser� disparado

    [Header("Sobre o Tiro")]
    public float velocidadeTiro; // For�a do tiro
    [SerializeField] public static float velocidadeTiroX; // For�a do tiro
    public float duracaoDoTiro; // Tempo que o tiro fica no ar at� ser destru�do 
    public void AtirarProj�til()
    {
        velocidadeTiroX = velocidadeTiro;
        // Instancia o proj�til e define sua posi��o e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiroX, 0);
        // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
        Destroy(temp.gameObject, duracaoDoTiro);
    }
}
