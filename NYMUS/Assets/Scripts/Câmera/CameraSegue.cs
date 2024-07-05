using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Jogador")]
    public Transform jogador; // Posição do jogador

    [Header("Posição Mínima do X")]
    public float minX; // Movimentação máxima para a esquerda
    [Header("Posição Máxima do X")]
    public float maxX; // Movimentação máxima para a direita
    [Header("Delay para alcançar o Jogador")]
    public float delayTempo; // Delay da câmera chegar no jogador
    public float velocidadeDeIrPraPontos;

    private Vector3 cameraDestination; // Posição de destino da câmera quando não estiver seguindo o jogador
    private bool seguindoJogador = true; // Flag para indicar se a câmera está seguindo o jogador

    private void FixedUpdate()
    {
        if (seguindoJogador)
        {
            Vector3 novaPosicao = jogador.position + new Vector3(0, 0, -10); // Afasta a câmera do cenário
            novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a câmera chegar no jogador
            transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), novaPosicao.y, novaPosicao.z);
            // Limita o posicionamento horizontal da câmera
        }
        else
        {
            // Se não estiver seguindo o jogador, move para a posição de destino
            transform.position = Vector3.Lerp(transform.position, cameraDestination, velocidadeDeIrPraPontos * Time.deltaTime);
        }
    }

    // Método para fazer a câmera parar de seguir o jogador e ir para uma posição específica
    public void MoverCamera(GameObject ponto)
    {
        if (seguindoJogador)
        {
            Vector3 destino = ponto.transform.position;
            seguindoJogador = false;
            cameraDestination = destino + new Vector3(0, 0, -10); // Afasta a câmera do cenário
        }
        else
        {
            VoltarASeguirJogador();
        }
    }

    // Método para fazer a câmera voltar a seguir o jogador
    public void VoltarASeguirJogador()
    {
        seguindoJogador = true;
    }
}