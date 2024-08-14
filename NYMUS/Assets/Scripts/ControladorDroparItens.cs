using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDroparItens : MonoBehaviour
{
    public GameObject[] drops;
    private int indexDrop;
    public float duracaoDrop;

    public static ControladorDroparItens Instancia { get; private set; }
    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void instanciarDrop(float posicaoParaInstanciarX, float posicaoParaInstanciarY, float posicaoParaInstanciarZ)
    {
        GameObject drop = Instantiate(drops[escolherDrop()]);
        drop.transform.position = new Vector3(posicaoParaInstanciarX, posicaoParaInstanciarY, posicaoParaInstanciarZ);
        Destroy(drop.gameObject, duracaoDrop);
    }

    public int escolherDrop()
    {
        indexDrop = Random.Range(0, drops.Length-1);
        return indexDrop;
    }
}
