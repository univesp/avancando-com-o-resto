using UnityEngine;
using TMPro;

public class FontSizeController : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int minFontSize;
    public int maxFontSize;

    //Inicia o tamanho da fonte se ele for menor que o mínimo ou maior que o máximo
    private void Start()
    {
        ApplyChange();

        if (text.fontSize < minFontSize)
        {
            text.fontSize = minFontSize;
        }

        if (text.fontSize > maxFontSize)
        {
            text.fontSize = maxFontSize;
        }
    }

    //Aplica o tamanho no texto
    public void ApplyChange()
    {
        if (text != null)
        {
            text.fontSize = PlayerPrefs.GetInt("fontSize");
        }
    }
}
