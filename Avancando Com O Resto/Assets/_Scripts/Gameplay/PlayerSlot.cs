using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int modoJogador; //-1 = não existe | 0 = jogador | 1 = IA
    [SerializeField] private GameObject objetosAtivos;

    [SerializeField] private int slotIndex;
    [SerializeField] private TMP_InputField campoNome;
    [SerializeField] private TextMeshProUGUI inputPlaceholder;

    [SerializeField] private Image avatar;
    [SerializeField] private Sprite[] avatarSprites;
    public static bool[] avatarEmUso = new bool[4];
    [SerializeField] private int avatarIndex;
    [SerializeField] private int ultimoAvatarIndex;
    private bool primeraEscolha = true;

    private void Start()
    {
        SetJogador();
        if (modoJogador != -1)
        {
            SetAvatar(1);
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetJogador();
    }

    public void SetJogador()
    {
        modoJogador++;
        if(modoJogador > 1)
        {
            modoJogador = -1;
        }

        switch(modoJogador)
        {
            case -1: //Não existe
                PlayerPrefs.SetInt(string.Format("Player{0}", slotIndex.ToString()), 0);
                objetosAtivos.SetActive(false);
                avatarEmUso[ultimoAvatarIndex] = false;
                primeraEscolha = true;
                break;

            case 0: //Player
                if(!objetosAtivos.activeSelf)
                {
                    SetAvatar(1);
                }
                PlayerPrefs.SetInt(string.Format("Player{0}", slotIndex.ToString()), 1);
                PlayerPrefs.SetInt(string.Format("IsPC{0}", slotIndex.ToString()), 0);
                campoNome.text = string.Format("JOGADOR {0}", slotIndex.ToString());
                inputPlaceholder.text = campoNome.text;
                SetNome(campoNome);
                objetosAtivos.SetActive(true);
                break;

            case 1: //CPU
                if(!objetosAtivos.activeSelf)
                {
                    SetAvatar(1);
                }
                PlayerPrefs.SetInt(string.Format("Player{0}", slotIndex.ToString()), 1);
                PlayerPrefs.SetInt(string.Format("IsPC{0}", slotIndex.ToString()), 1);
                campoNome.text = string.Format("CPU {0}", slotIndex.ToString());
                inputPlaceholder.text = campoNome.text;
                SetNome(campoNome);
                objetosAtivos.SetActive(true);
                break;
        }
    }

    public void SetNome(TMP_InputField _campoNome)
    {
        PlayerPrefs.SetString(string.Format("Nome{0}", slotIndex.ToString()), _campoNome.text);
    }

    public void SetAvatar(int _sentido)
    {
        if (ChecarAvatarDisponível())
        {
            avatarIndex += _sentido;
            if (avatarIndex < 0)
            {
                avatarIndex = avatarSprites.Length - 1;
            }
            else if (avatarIndex == avatarSprites.Length)
            {
                avatarIndex = 0;
            }

            if (!avatarEmUso[avatarIndex])
            {
                avatar.sprite = avatarSprites[avatarIndex];
                avatarEmUso[avatarIndex] = true;
                if (primeraEscolha)
                {
                    primeraEscolha = false;
                }
                else
                {
                    avatarEmUso[ultimoAvatarIndex] = false;
                }
                ultimoAvatarIndex = avatarIndex;
                PlayerPrefs.SetInt(string.Format("Aparencia{0}", slotIndex), avatarIndex);
            }
            else
            {
                SetAvatar(_sentido);
            }
        }
    }

    private bool ChecarAvatarDisponível()
    {
        for(int i = 0; i < avatarEmUso.Length; i++)
        {
            if(!avatarEmUso[i])
            {
                return true;
            }
        }
        return false;
    }
}
