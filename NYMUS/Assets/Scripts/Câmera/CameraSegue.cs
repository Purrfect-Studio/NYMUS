using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Jogador")]
    public Transform jogador; // Posi��o do jogador

    [Header("Posi��o M�nima do X")]
    public float minX; // Movimenta��o m�xima para a esquerda
    [Header("Posi��o M�xima do X")]
    public float maxX; // Movimenta��o m�xima para a direita
    [Header("Delay para alcan�ar o Jogador")]
    public float delayTempo; // Delay da c�mera chegar no jogador
    public float velocidadeDeIrPraPontos;

    private Vector3 destinoCamera; // Posi��o de destino da c�mera quando n�o estiver seguindo o jogador
    private bool seguindoJogador = true; // Flag para indicar se a c�mera est� seguindo o jogador

    [Header("Fase Internet")]
    public bool TravarY;

    private void FixedUpdate()
    {
        if (seguindoJogador)
        {
            Vector3 novaPosicao = jogador.position + new Vector3(0, 0, -10); // Afasta a c�mera do cen�rio
            novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a c�mera chegar no jogador
            if (!TravarY)
            {
                transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), novaPosicao.y, novaPosicao.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), transform.position.y, novaPosicao.z);
            }
            // Limita o posicionamento horizontal da c�mera
        }
        else
        {
            // Se n�o estiver seguindo o jogador, move para a posi��o de destino
            transform.position = Vector3.Lerp(transform.position, destinoCamera, velocidadeDeIrPraPontos * Time.deltaTime);
        }
    }

    // M�todo para fazer a c�mera parar de seguir o jogador e ir para uma posi��o espec�fica
    public void MoverCamera(GameObject ponto)
    {
            Vector3 destino = ponto.transform.position;
            seguindoJogador = false;
            destinoCamera = destino + new Vector3(0, 0, -10); // Afasta a c�mera do cen�rio
    }

    // M�todo para fazer a c�mera voltar a seguir o jogador
    public void VoltarASeguirJogador()
    {
        seguindoJogador = true;
    }
    public void DestravarY()
    {
        TravarY = false;
    }
}