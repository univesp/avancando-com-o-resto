using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip mouseOver;
    [SerializeField] private AudioClip mouseClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioPlayer.instance.PlaySFX(mouseOver);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioPlayer.instance.PlaySFX(mouseClick);
    }
}
