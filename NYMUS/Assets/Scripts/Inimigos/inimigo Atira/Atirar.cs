using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    // Variáveis para configurar o tiro
    public GameObject balaProjetil; // Prefab do projétil
    public Transform arma; // Posição de onde o projétil será disparado
    public float forcaTiro; // Força do tiro

    public void AtirarProjétil()
    {
        // Instancia o projétil e define sua posição e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);
        // Destroi o projétil depois de um tempo para evitar vazamento de memória
        Destroy(temp.gameObject, 3f);
    }
}
