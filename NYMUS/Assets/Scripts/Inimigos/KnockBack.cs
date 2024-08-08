using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float forca = 10, delay = 0.15f;
    [SerializeField] private GameObject jogador;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void PlayKnockback()
    {
        VidaInimigo.podeMover = false;
        StopAllCoroutines();
        Vector2 direction = (transform.position - jogador.transform.position).normalized;
        rb2d.AddForce(direction*forca, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        VidaInimigo.podeMover = true;
    }
}
