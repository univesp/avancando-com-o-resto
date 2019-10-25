using UnityEngine;
using TMPro;

public class PerguntaBotao : MonoBehaviour
{
    [SerializeField] private TMP_InputField respostaInput;
    [SerializeField] private GameObject botao;
    public bool podeChecar;

    public void CheckInput()
    {
        if (podeChecar)
        {
            if (respostaInput.text != null || respostaInput.text != "")
            {
                botao.SetActive(true);
            }
            else
            {
                botao.SetActive(false);
            }
        }
    }
}
