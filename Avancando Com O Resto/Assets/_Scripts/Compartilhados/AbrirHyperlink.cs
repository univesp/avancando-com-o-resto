using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AbrirHyperlink : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private Camera mainCamera;
    private int linkIndex;
    private TMP_LinkInfo linkInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        

        if (linkIndex != -1)
        {
            linkInfo = texto.textInfo.linkInfo[linkIndex];

            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
