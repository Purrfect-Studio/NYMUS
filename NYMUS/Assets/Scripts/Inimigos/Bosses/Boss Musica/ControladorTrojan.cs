using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTrojan : MonoBehaviour
{
    public enum ataquesBoss
    {
        ataque1,
        ataque2,
        ataque3,
    }

    [Header("Componentes")]
    private VidaBoss vidaBoss;
    private MovimentacaoTrojan movimentacaoTrojan;

    [Header("Variaveis de controle")]
    public static bool podeExecutarAcoes;
    public float delayParaIniciarAcoes;

    [Header("Variaver de apoio")]
    private List<ataquesBoss> ataquesDisponiveis = new List<ataquesBoss>();

    [Header("Ataques")]
    public float cooldownParaAtacar;
    public float cooldownAtaqueFrenesi;
    private float cooldownRestanteParaAtacar;
    // Start is called before the first frame update
    void Start()
    {
        vidaBoss = GetComponent<VidaBoss>();
        movimentacaoTrojan = GetComponent<MovimentacaoTrojan>();

        ataquesDisponiveis.Add(ataquesBoss.ataque1);
        ataquesDisponiveis.Add(ataquesBoss.ataque2);
        ataquesDisponiveis.Add(ataquesBoss.ataque3);

        podeExecutarAcoes = false;
        MovimentacaoTrojan.podeMover = false;

        cooldownRestanteParaAtacar = cooldownParaAtacar;
    }

    // Update is called once per frame
    void Update()
    {
        if (podeExecutarAcoes)
        {
            cooldownRestanteParaAtacar -= Time.deltaTime;

            if(cooldownRestanteParaAtacar <= 0)
            {
                ExecutarAtaque(EscolherAtaqueAtual());
                cooldownRestanteParaAtacar = cooldownParaAtacar;
            }
        }
        if (vidaBoss.frenesi)
        {
            cooldownParaAtacar = cooldownAtaqueFrenesi;
        }

        iniciarAtaque();
    }

    void iniciarAtaque()
    {
        if (MovimentacaoTrojan.podeMover == true)
        {
            if (podeExecutarAcoes == false)
            {
                StartCoroutine("DelayParaIniciarAcoes");
            }
        }
        else
        {
            podeExecutarAcoes = false;
            cooldownRestanteParaAtacar = cooldownParaAtacar;
        }
    }

    public void ExecutarAtaque(ataquesBoss ataque)
    {
        switch (ataque)
        {
            case ataquesBoss.ataque1:
                Debug.Log("Boss est� executando ataque1");
                
                break;

            case ataquesBoss.ataque2:
                Debug.Log("Boss est� executando ataque2");
                
                break;

            case ataquesBoss.ataque3:
                Debug.Log("Boss est� executando ataque3");

                break;
        }
    }

    public ataquesBoss EscolherAtaqueAtual()
    {
        return ataquesDisponiveis[Random.Range(0, ataquesDisponiveis.Count)];
    }

    public void delayParaIniciarAcao()
    {
        StartCoroutine("DelayParaIniciarAcoes");
    }

    IEnumerator DelayParaIniciarAcoes()
    {
        yield return new WaitForSeconds(delayParaIniciarAcoes);
        podeExecutarAcoes = true;
        MovimentacaoTrojan.podeMover = true;
    }
}
