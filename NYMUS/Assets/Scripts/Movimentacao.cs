using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public Rigidbody2D rb; //rb = rigidbody
    public int velocidade;
    private float direcao; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        direcao = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direcao*velocidade, rb.velocity.y); 
        // O primeiro parâmetro da Vector recebe o valor de força aplicada no vetor. A direção pega se o valor é positivo ou negativo (esquerda ou direita) e aplica a velocidade
       

    }
}
