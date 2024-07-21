using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Glitchs : MonoBehaviour
{
    // Referência ao SpriteRenderer do objeto
    private SpriteRenderer spriteRenderer;

    // Referência ao Tilemap do objeto
    private Tilemap tileMap;

    // Referência ao Transform do objeto
    private Transform objetoTransform;

    // Tempo de mudança dos tiles no Tilemap
    [Header("Configurações de Mudança de Tilemap")]
    public float tempoMudancaTilemap = 5.0f;

    // Método chamado ao iniciar o script
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileMap = GetComponent<Tilemap>();
        objetoTransform = GetComponent<Transform>();
    }

    // Método para flipar o eixo X do SpriteRenderer
    public void FlipX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // Método para flipar o eixo Y do SpriteRenderer
    public void FlipY()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    // Método para mudar a cor do sprite para uma aleatória
    public void MudarCorAleatoria()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // Coroutine para mudar as cores dos tiles individualmente em tempos aleatórios
    private IEnumerator MudarCoresTiles()
    {
        while (true)
        {
            foreach (var pos in tileMap.cellBounds.allPositionsWithin)
            {
                if (tileMap.HasTile(pos))
                {
                    StartCoroutine(MudarCorTile(pos));
                }
            }
            yield return new WaitForSeconds(1f); // Tempo entre verificações globais, pode ser ajustado conforme necessário
        }
    }

    // Coroutine para mudar a cor de um tile específico
    private IEnumerator MudarCorTile(Vector3Int pos)
    {
        float tempoAleatorio = Random.Range(0, tempoMudancaTilemap); // Tempo aleatório entre 0 e tempoMudancaTilemap
        yield return new WaitForSeconds(tempoAleatorio);

        Color corAleatoria = new Color(Random.value, Random.value, Random.value);
        tileMap.SetTileFlags(pos, TileFlags.None); // Permite modificar a cor do tile
        tileMap.SetColor(pos, corAleatoria);
    }

    // Método para mudar a posição do objeto para um lugar próximo
    public void MudarPosicaoAleatoria()
    {
        if (objetoTransform != null)
        {
            Vector3 novaPosicao = objetoTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            objetoTransform.position = novaPosicao;
        }
    }

    // Método para iniciar a coroutine de piscar o sprite
    public void Piscar()
    {
        StartCoroutine(PiscarCoroutine());
    }

    // Coroutine para fazer o sprite piscar
    private IEnumerator PiscarCoroutine()
    {
        if (spriteRenderer != null)
        {
            for (float i = 0f; i < 1f; i += 0.1f)
            {
                spriteRenderer.enabled = false; // Desativa o sprite
                yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
                spriteRenderer.enabled = true; // Ativa o sprite
                yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos
            }
        }
    }

    // Método para ligar a mudança de cores dos tiles do Tilemap
    public void LigarGlitchTileMap()
    {
        StartCoroutine(MudarCoresTiles());
    }
}
