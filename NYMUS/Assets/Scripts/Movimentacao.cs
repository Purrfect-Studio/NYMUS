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
        // O primeiro par�metro da Vector recebe o valor de for�a aplicada no vetor. A dire��o pega se o valor � positivo ou negativo (esquerda ou direita) e aplica a velocidade
       

    }
}
