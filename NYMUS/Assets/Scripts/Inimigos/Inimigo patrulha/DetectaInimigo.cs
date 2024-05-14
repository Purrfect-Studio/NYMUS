using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaInimigo : MonoBehaviour
{
    [Header("Box Collider")]
    public BoxCollider2D bcInimigo;
    [Header("Layer do Inimigo")]
    [SerializeField] private LayerMask layerInimigo;
    [Header("Bool de apoio")]
    public static bool encontrouInimigo;

    // Update is called once per frame
    void Update()
    {
        if(inimigo() == true)
        {
            encontrouInimigo = true;
        }
    }

    private bool inimigo()
    {
        RaycastHit2D inimigo = Physics2D.BoxCast(bcInimigo.bounds.center, bcInimigo.bounds.size, 0, Vector2.down, 0.05f, layerInimigo); // Cria um segundo box collider para reconhecer o chao
        return inimigo.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }
}
