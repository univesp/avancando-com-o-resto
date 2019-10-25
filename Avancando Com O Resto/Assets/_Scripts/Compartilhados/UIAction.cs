using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent acoes;
    [SerializeField] private Color selecionado;
    private Color linhaDesselecionado;
    private Color textoDesselecionado;

    [SerializeField] private Image linha;
    [SerializeField] private TextMeshProUGUI texto;

    //Variáveis dos sons
    [SerializeField] private AudioClip mouseOver;
    [SerializeField] private AudioClip mouseClick;

    private void Start()
    {
        linhaDesselecionado = linha.color;
        textoDesselecionado = texto.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        linha.color = selecionado;
        texto.color = selecionado;
        AudioPlayer.instance.PlaySFX(mouseOver);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        linha.color = linhaDesselecionado;
        texto.color = textoDesselecionado;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioPlayer.instance.PlaySFX(mouseClick);
        acoes.Invoke();
    }
}
