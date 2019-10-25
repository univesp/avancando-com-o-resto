using UnityEngine;
using UnityEngine.UI;

public class FontSizeSettings : MonoBehaviour
{
    public FontSizeController[] fontSizeController;

    public Slider fontSizeSlider;
    public int minFontSize;
    public int maxFontSize;

    //Inicia o slider na última posição deixada pelo jogador
    private void OnEnable()
    {
        fontSizeSlider.value = PlayerPrefs.GetInt("fontSize");
    }

    //Ao iniciar o script, ele seta o tamanho da fonte pra um tamanho inicial se ele for menor que o mínimo ou maior que o máximo e delega essa função ao slider
    private void Start()
    {
        InitialFontSize();
    }

    public void ChangeFontSize()
    {
        PlayerPrefs.SetInt("fontSize", (int)fontSizeSlider.value);

        for (int i = 0; i < fontSizeController.Length; i++)
        {
            fontSizeController[i].ApplyChange();
        }
    }

    private void InitialFontSize()
    {
        if (PlayerPrefs.GetInt("fontSize") < minFontSize)
        {
            PlayerPrefs.SetInt("fontSize", minFontSize);
        }

        if (PlayerPrefs.GetInt("fontSize") > maxFontSize)
        {
            PlayerPrefs.SetInt("fontSize", maxFontSize);
        }
    }
}
