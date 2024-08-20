using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choque : MonoBehaviour
{
    private BoxCollider2D boxCollider2d;
    void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    public void ativarCollider()
    {
        boxCollider2d.enabled = true;
    }

    public void desativarCollider()
    {
        boxCollider2d.enabled = false;
    }

    public void desativarGameObject()
    {
        StartCoroutine("DesativarDepoisDeTempo");
    }

    IEnumerator DesativarDepoisDeTempo()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            VidaBoss vidaBoss = collision.gameObject.GetComponent<VidaBoss>();
            if(vidaBoss != null)
            {
                vidaBoss.derrubarTrojan();
            }
        }
    }
}
