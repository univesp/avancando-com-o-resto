using UnityEngine;
using GAF.Core;

public class Jogador : MonoBehaviour
{
    //Posição inicial
    private Vector2 posInicial;

    //Variável do nome
    public string nome;

    public bool isPC;
    public int jogadorIndice;

    //Variáveis das casas
    [SerializeField] private Casa[] casas;
    public int indice = -1;
    public int casasAndar = 0;

    //Variáveis do movimento
    private float velocidade = 100;
    private bool podeMover;

    //Variáveis dos sprites
    [SerializeField] private GameObject spritePivot;
    [SerializeField] private GameObject animationPile;
    [SerializeField] private GAFMovieClip paradoClip;
    [SerializeField] private GAFMovieClip correndoClip;
    [SerializeField] private GAFMovieClip vitoriaClip;
    [SerializeField] private GAFMovieClip derrotaClip;
    [SerializeField] private int sortingLayer = 1;
    [SerializeField] private Vector3 normal;
    [SerializeField] private Vector3 flip;

    //Variáveis da animação
    [SerializeField] private GameObject parado;
    [SerializeField] private GameObject correndo;
    [SerializeField] private GameObject vitoria;
    [SerializeField] private GameObject derrota;

    //GameCore
    [SerializeField] private GameCore gameCore;

    [SerializeField] private AudioClip passosSFX;
    private void Start()
    {
        posInicial = transform.localPosition;
    }

    private void Update()
    {
        Movimento();
    }

    private void Movimento()
    {
        if (podeMover)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, casas[indice].transform.localPosition, velocidade * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, casas[indice].transform.localPosition) <= 0.01f)
            {
                if (casas[indice].isPortal)
                {
                    transform.localPosition = casas[indice + 1].transform.localPosition;
                    indice += 2;
                    return;
                }                

                if(casas[indice].changeLevel)
                {
                    sortingLayer++;
                    paradoClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
                    correndoClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
                    vitoriaClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
                    derrotaClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
                    correndoClip.reload();
                }

                if (casas[indice].newLevel)
                {
                    if (gameCore.levelReached < casas[indice].level)
                    {
                        gameCore.levelReached = casas[indice].level;
                        gameCore.animator.Play(string.Format("Nevoa_Level{0}", gameCore.levelReached.ToString()));
                    }
                }

                if (casas[indice].flipSprite)
                {                    
                    animationPile.transform.localScale = new Vector3(animationPile.transform.localScale.x * -1.0f, animationPile.transform.localScale.y, animationPile.transform.localScale.z);
                    if(animationPile.transform.localScale.x > 0)
                    {
                        spritePivot.transform.localPosition = normal;
                    }
                    else
                    {
                        spritePivot.transform.localPosition = flip;
                    }
                }

                if (indice + 1 == casas.Length)
                {
                    //Chama fim
                    TocaAnimacaoVitoria();
                    StartCoroutine(gameCore.ChamaFim(isPC, nome, jogadorIndice));
                    podeMover = false;
                    AudioPlayer.instance.StopLoopSFX();
                }

                if (casasAndar > 0)
                {
                    casasAndar--;
                    indice++;
                }
                else
                {
                    TocaAnimacaoParado();
                    if (casas[indice].valor == 0)
                    {
                        gameCore.ChamaCaiPedra();
                    }
                    else
                    {
                        //Chama próximo jogador
                        StartCoroutine(gameCore.ProximoJogador());                        
                        podeMover = false;
                        AudioPlayer.instance.StopLoopSFX();
                    }                                        
                }
            }
        }
    }

    public void ResetaPersonagem()
    {
        transform.localPosition = posInicial;
        sortingLayer = 1;
        paradoClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
        correndoClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
        vitoriaClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
        derrotaClip.settings.spriteLayerName = string.Format("Level{0}", sortingLayer);
        correndoClip.reload();
        podeMover = false;
        AudioPlayer.instance.StopLoopSFX();
    }

    public void SetNome(string _nome)
    {
        nome = _nome;
    }

    //Recebe a quantidade de casas que o jogador vai andar
    public void IniciaMovimento(int _casasQtd)
    {
        if (_casasQtd != 0)
        {
            AudioPlayer.instance.PlayLoopSFX(passosSFX);
            casasAndar = _casasQtd;
            podeMover = true;
            indice++;
            casasAndar--;
        }
        else
        {
            TocaAnimacaoParado();
            StartCoroutine(gameCore.ProximoJogador());
        }
    }

    public Casa GetCasa()
    {
        return casas[indice];
    }

    public void TocaAnimacaoParado()
    {
        parado.SetActive(true);
        correndo.SetActive(false);
        vitoria.SetActive(false);
        derrota.SetActive(false);
    }

    public void TocaAnimacaoCorrendo()
    {
        parado.SetActive(false);
        correndo.SetActive(true);
        vitoria.SetActive(false);
        derrota.SetActive(false);
    }

    public void TocaAnimacaoVitoria()
    {
        parado.SetActive(false);
        correndo.SetActive(false);
        vitoria.SetActive(true);
        derrota.SetActive(false);
    }

    public void TocaAnimacaoDerrota()
    {
        parado.SetActive(false);
        correndo.SetActive(false);
        vitoria.SetActive(false);
        derrota.SetActive(true);
    }
}