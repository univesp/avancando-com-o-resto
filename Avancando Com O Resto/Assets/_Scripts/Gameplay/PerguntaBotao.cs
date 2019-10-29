using UnityEngine;
using TMPro;

public class PerguntaBotao : MonoBehaviour
{
    [SerializeField] private GameCore gameCore;
    [SerializeField] private TMP_InputField respostaInput;
    [SerializeField] private GameObject botao;
    public bool podeChecar;

    public void CheckInput()
    {
        if (podeChecar && !gameCore.GetJogador().isPC)
        {
            if (respostaInput.text != null || respostaInput.text != "")
            {
                botao.SetActive(true);
                gameCore.podeCalcular = true;
            }

            if (respostaInput.text == null || respostaInput.text == "")
            {
                Debug.Log("Entrou");
                botao.SetActive(false);
                gameCore.podeCalcular = false;
            }
        }
    }
}
