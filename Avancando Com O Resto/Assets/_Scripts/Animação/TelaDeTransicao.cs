using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TelaDeTransicao : MonoBehaviour
{
    //Variáveis da animação
    [SerializeField] private Animator animator;
    [SerializeField] private string entraTransicao;
    [SerializeField] private string saiTransicao;

    //Evento da ação
    [SerializeField] private UnityEvent acao;

    //Chamado nos momentos de transição
    public void ChamaTransicao(float _delay)
    {
        StartCoroutine(Transicao(_delay));
    }

    private IEnumerator Transicao(float _delay)
    {
        animator.Play(entraTransicao);

        yield return new WaitForEndOfFrame();
                
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        acao.Invoke();

        yield return new WaitForSeconds(_delay);

        animator.Play(saiTransicao);
    }
}
