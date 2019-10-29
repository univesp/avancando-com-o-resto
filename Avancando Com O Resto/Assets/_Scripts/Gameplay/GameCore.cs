using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCore : MonoBehaviour
{
    [Header("Variáveis dos jogadores")]
    [SerializeField] private GameObject[] jogadoresAnimacao;
    [SerializeField] private Jogador[] jogadoresPile;
    [SerializeField] private List<Jogador> jogadores;
    private int jogadorIndice = -1;

    [Header("Variáveis dos dados")]
    [SerializeField] private GameObject dadoJanela;
    [SerializeField] private Dado dado;
    [SerializeField] private GameObject botaoDado;
    private int resultadoDado;
    private int tempResultado;
    private int quantidadeDados = 0;

    [Header("Variáveis dos restos")]
    [SerializeField] private TMP_InputField inputResto;
    private int resultadoResto;

    [Header("Variáveis da pergunta")]
    [SerializeField] private TextMeshProUGUI valor1, valor2;
    [SerializeField] private GameObject perguntaJanela;
    [SerializeField] private Animator perguntaJanelaAnimator;
    [SerializeField] private GameObject perguntaOKBotao;
    [SerializeField] private PerguntaBotao perguntaBotaoCheck;
    [SerializeField] public bool podeCalcular;

    [Header("Variáveis da pedra")]
    [SerializeField] private GameObject pedraApagada;
    [SerializeField] private GameObject pedraVerdadeira;

    [Header("Variáveis da névoa negra")]
    public Animator animator;
    public int levelReached = 1;

    [Header("Variáveis da apresentação do jogador")]
    [SerializeField] private GameObject vezDoJogador;
    [SerializeField] private TextMeshProUGUI nomeDoJogador;

    [Header("Variáveis do final")]
    [SerializeField] private GameObject telaFinal;
    [SerializeField] private GameObject telaVitoria;
    [SerializeField] private GameObject telaDerrota;
    [SerializeField] private TextMeshProUGUI finalNomeDoJogador;

    //Random
    private System.Random random = new System.Random();
    private int randomCPU;

    [Header("Variável dos sons")]
    [SerializeField] private AudioClip vezJogadorSFX;
    [SerializeField] private AudioClip dadoSFX;
    [SerializeField] private AudioClip pergaminhoSFX;
    [SerializeField] private AudioClip acertoSFX;
    [SerializeField] private AudioClip erroSFX;
    [SerializeField] private AudioClip pedraSFX;
    [SerializeField] private AudioClip cristalSFX;
    [SerializeField] private AudioClip vitoriaBGM;
    [SerializeField] private AudioClip derrotaBGM;    

    private void Start()
    {        
        if(PlayerPrefs.GetInt("Player1") == 1)
        {
            AtivarJogador(PlayerPrefs.GetInt("Aparencia1"));
            if (PlayerPrefs.GetInt("IsPC1") == 1)
            {
                jogadores[jogadores.Count - 1].isPC = true;
            }
            else
            {
                jogadores[jogadores.Count - 1].isPC = false;
            }
            jogadores[jogadores.Count - 1].SetNome(PlayerPrefs.GetString("Nome1"));
        }
        if (PlayerPrefs.GetInt("Player2") == 1)
        {
            AtivarJogador(PlayerPrefs.GetInt("Aparencia2"));
            if (PlayerPrefs.GetInt("IsPC2") == 1)
            {
                jogadores[jogadores.Count - 1].isPC = true;
            }
            else
            {
                jogadores[jogadores.Count - 1].isPC = false;
            }
            jogadores[jogadores.Count - 1].SetNome(PlayerPrefs.GetString("Nome2"));
        }
        if (PlayerPrefs.GetInt("Player3") == 1)
        {
            AtivarJogador(PlayerPrefs.GetInt("Aparencia3"));
            if (PlayerPrefs.GetInt("IsPC3") == 1)
            {
                jogadores[jogadores.Count - 1].isPC = true;
            }
            else
            {
                jogadores[jogadores.Count - 1].isPC = false;
            }
            jogadores[jogadores.Count - 1].SetNome(PlayerPrefs.GetString("Nome3"));
        }
        if (PlayerPrefs.GetInt("Player4",0) == 1)
        {
            AtivarJogador(PlayerPrefs.GetInt("Aparencia4"));
            if (PlayerPrefs.GetInt("IsPC4") == 1)
            {
                jogadores[jogadores.Count - 1].isPC = true;
            }
            else
            {
                jogadores[jogadores.Count - 1].isPC = false;
            }
            jogadores[jogadores.Count - 1].SetNome(PlayerPrefs.GetString("Nome4"));
        }
        StartCoroutine(ProximoJogador());
    }

    private void AtivarJogador(int _aparencia)
    {        
        jogadoresPile[_aparencia].gameObject.SetActive(true);
        jogadoresAnimacao[_aparencia].SetActive(true);
        jogadores.Add(jogadoresPile[_aparencia]);        
    }

    public IEnumerator ProximoJogador()
    {
        yield return new WaitForSeconds(2.0f);
        for(int i = 0; i < jogadores.Count; i++)
        {
            jogadorIndice++;
            if(jogadorIndice == jogadores.Count)
            {
                jogadorIndice = 0;
            }

            if(jogadores[jogadorIndice].gameObject.activeSelf)
            {
                //Inicia a animação do nome do jogador na tela
                nomeDoJogador.text = jogadores[jogadorIndice].nome;
                vezDoJogador.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                AudioPlayer.instance.PlaySFX(vezJogadorSFX);
                //No final dessa animação, ele libera a hud com os dados para o jogador jogar                
                yield return new WaitForSeconds(1.5f);

                AudioPlayer.instance.PlayLoopSFX(dadoSFX);

                //IA
                inputResto.text = "";
                if(jogadores[jogadorIndice].isPC)
                {
                    //Faz aparecer o dado
                    dado.dadosAnimator[0].enabled = true;
                    dado.dados[0].SetActive(true);
                    FazJogada();
                }
                else
                {
                    //Faz aparecer o primeiro dado
                    dado.dadosAnimator[0].enabled = true;
                    dado.dados[0].SetActive(true);
                    dadoJanela.SetActive(true);
                }
                break;
            }
        }
    }

    public void SetQuantidadeDados(int _quantidadeDados)
    {
        quantidadeDados = _quantidadeDados;
    }

    public void FazJogada()
    {
        StartCoroutine(Jogada());
    }

    private IEnumerator Jogada()
    {
        //IA
        if(jogadores[jogadorIndice].isPC)
        {

            if(random.Next(0, 3) == 0)
            {                
                quantidadeDados = 1;
                //Faz o segundo dado aparecer
                dado.dadosAnimator[1].enabled = true;
                dado.dados[1].SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
            AudioPlayer.instance.StopLoopSFX();
        }

        do
        {
            //Joga o resultado do dado       
            tempResultado = dado.RolaDado();
            resultadoDado += tempResultado;
            //Mostra face do dado
            dado.PararFace(quantidadeDados, tempResultado);
            quantidadeDados--;
        }
        while (quantidadeDados >= 0);

        yield return new WaitForSeconds(1.5f);
        dado.dados[0].SetActive(false);
        dado.dados[1].SetActive(false);

        if (quantidadeDados <= 0)
        {
            //Muda os valores da pergunta e abre caixa pro jogador colocar o resultado
            perguntaOKBotao.SetActive(false);
            inputResto.interactable = true;
            perguntaBotaoCheck.podeChecar = true;
            valor1.text = jogadores[jogadorIndice].GetCasa().valor.ToString("00");
            valor2.text = resultadoDado.ToString("00");
            perguntaJanela.SetActive(true);
            AudioPlayer.instance.PlaySFX(pergaminhoSFX);
            yield return new WaitForSeconds(0.5f);

            //IA
            if (jogadores[jogadorIndice].isPC)
            {
                inputResto.interactable = false;
                perguntaBotaoCheck.podeChecar = false;
                yield return new WaitForSeconds(1.0f);
                //Fazer aqui o resultado de acordo com a dificuldade  
                randomCPU = random.Next(0, 100);
                if (randomCPU < 50)
                {
                    inputResto.text = (jogadores[jogadorIndice].GetCasa().valor % resultadoDado).ToString();
                }
                else
                {
                    inputResto.text = random.Next(0, 8).ToString();
                }
                yield return new WaitForSeconds(2.0f);
                CalculaResultado();
            }
        }
    }

    public void CalculaResultado()
    {
        if (podeCalcular)
        {
            perguntaJanelaAnimator.Play("pergaminho_sai");
            AudioPlayer.instance.PlaySFX(pergaminhoSFX);            
            resultadoResto = jogadores[jogadorIndice].GetCasa().valor % resultadoDado;
            if (resultadoResto == int.Parse(inputResto.text))
            {
                StartCoroutine(Acerto());
            }
            else
            {
                StartCoroutine(Erro());
            }
            //Reinicia os valores dos dados para a próxima jogada
            resultadoDado = 0;
            quantidadeDados = 0;
            botaoDado.SetActive(true);
            podeCalcular = true;
        }
    }

    private IEnumerator Acerto()
    {
        AudioPlayer.instance.PlaySFX(acertoSFX);
        yield return new WaitForSeconds(0.5f);
        perguntaOKBotao.SetActive(false);

        //Toca animação do jogador feliz
        jogadores[jogadorIndice].TocaAnimacaoVitoria();
        yield return new WaitForSeconds(1.0f);

        //Toca animação do jogador correndo
        jogadores[jogadorIndice].TocaAnimacaoCorrendo();

        //Move jogador
        jogadores[jogadorIndice].IniciaMovimento(resultadoResto + jogadores[jogadorIndice].GetCasa().valorBonus);
    }

    private IEnumerator Erro()
    {
        AudioPlayer.instance.PlaySFX(erroSFX);
        yield return new WaitForSeconds(0.5f);
        perguntaOKBotao.SetActive(false);

        //Toca animação do jogador triste
        jogadores[jogadorIndice].TocaAnimacaoDerrota();

        yield return new WaitForSeconds(2.5f);
        jogadores[jogadorIndice].TocaAnimacaoParado();

        //Chama o próximo jogador
        StartCoroutine(ProximoJogador());
    }

    public void ChamaCaiPedra()
    {
        StartCoroutine(CaiPedra());
    }

    private IEnumerator CaiPedra()
    {
        AudioPlayer.instance.PlaySFX(pedraSFX);
        pedraApagada.SetActive(false);
        pedraVerdadeira.SetActive(true);
        yield return new WaitForSeconds(2.0f);

        jogadores[jogadorIndice].ResetaPersonagem();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ProximoJogador());
        pedraApagada.SetActive(true);
        pedraVerdadeira.SetActive(false);
    }

    public IEnumerator ChamaFim(bool _isPC, string _playerName, int _playerIndex)
    {
        for(int i = 0; i < jogadoresPile.Length; i++)
        {
            if(i != _playerIndex)
            {
                jogadoresPile[i].TocaAnimacaoDerrota();
            }
        }
        AudioPlayer.instance.PlaySFX(cristalSFX);
        yield return new WaitForSeconds(1.5f);        
        telaFinal.SetActive(true);
        if(!_isPC)
        {
            AudioPlayer.instance.PlayBGM(vitoriaBGM);
            finalNomeDoJogador.text = _playerName;
            telaVitoria.SetActive(true);
        }
        else
        {
            AudioPlayer.instance.PlayBGM(derrotaBGM);
            telaDerrota.SetActive(true);
        }
    }

    public Jogador GetJogador()
    {
        return jogadores[jogadorIndice];
    }
}