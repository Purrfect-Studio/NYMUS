using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Glitchs : MonoBehaviour
{
    // Refer�ncia ao SpriteRenderer do objeto
    private SpriteRenderer spriteRenderer;

    // Refer�ncia ao Tilemap do objeto
    private Tilemap tileMap;

    // Refer�ncia ao Transform do objeto
    private Transform objetoTransform;

    // Tempo de mudan�a dos tiles no Tilemap
    [Header("Configura��es de Mudan�a de Tilemap")]
    public float tempoMudancaTilemap = 5.0f;

    // M�todo chamado ao iniciar o script
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileMap = GetComponent<Tilemap>();
        objetoTransform = GetComponent<Transform>();
    }

    // M�todo para flipar o eixo X do SpriteRenderer
    public void FlipX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // M�todo para flipar o eixo Y do SpriteRenderer
    public void FlipY()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    // M�todo para mudar a cor do sprite para uma aleat�ria
    public void MudarCorAleatoria()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // Coroutine para mudar as cores dos tiles individualmente em tempos aleat�rios
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
            yield return new WaitForSeconds(1f); // Tempo entre verifica��es globais, pode ser ajustado conforme necess�rio
        }
    }

    // Coroutine para mudar a cor de um tile espec�fico
    private IEnumerator MudarCorTile(Vector3Int pos)
    {
        float tempoAleatorio = Random.Range(0, tempoMudancaTilemap); // Tempo aleat�rio entre 0 e tempoMudancaTilemap
        yield return new WaitForSeconds(tempoAleatorio);

        Color corAleatoria = new Color(Random.value, Random.value, Random.value);
        tileMap.SetTileFlags(pos, TileFlags.None); // Permite modificar a cor do tile
        tileMap.SetColor(pos, corAleatoria);
    }

    // M�todo para mudar a posi��o do objeto para um lugar pr�ximo
    public void MudarPosicaoAleatoria()
    {
        if (objetoTransform != null)
        {
            Vector3 novaPosicao = objetoTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            objetoTransform.position = novaPosicao;
        }
    }

    // M�todo para iniciar a coroutine de piscar o sprite
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

    // M�todo para ligar a mudan�a de cores dos tiles do Tilemap
    public void LigarGlitchTileMap()
    {
        StartCoroutine(MudarCoresTiles());
    }
}
