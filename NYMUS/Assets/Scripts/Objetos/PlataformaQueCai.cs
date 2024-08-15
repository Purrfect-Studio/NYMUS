using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaQueCai : MonoBehaviour
{
    public float tempoParaCair;
    public float tempoParaVoltar;
    public Rigidbody2D rigidbody2d;
    public Vector2 posicaoInicial;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            StartCoroutine("PlataformaCai");
        }
    }

    IEnumerator PlataformaCai()
    {
        yield return new WaitForSeconds(tempoParaCair);
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(tempoParaVoltar);
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        transform.position = posicaoInicial;
        rigidbody2d.velocity = Vector2.zero;
    }
}
