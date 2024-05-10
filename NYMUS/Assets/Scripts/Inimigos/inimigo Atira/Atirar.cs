using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    // Vari�veis para configurar o tiro
    public GameObject balaProjetil; // Prefab do proj�til
    public Transform arma; // Posi��o de onde o proj�til ser� disparado
    public float forcaTiro; // For�a do tiro

    public void AtirarProj�til()
    {
        // Instancia o proj�til e define sua posi��o e velocidade
        GameObject temp = Instantiate(balaProjetil);
        temp.transform.position = arma.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);
        // Destroi o proj�til depois de um tempo para evitar vazamento de mem�ria
        Destroy(temp.gameObject, 3f);
    }
}
