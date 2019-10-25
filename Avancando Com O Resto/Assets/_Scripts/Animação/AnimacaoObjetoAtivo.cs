using UnityEngine;

public class AnimacaoObjetoAtivo : MonoBehaviour
{
    [SerializeField] private GameObject objeto;

    public void AtivaObjeto()
    {
        objeto.SetActive(true);
    }

    public void DesativaObjeto()
    {
        objeto.SetActive(false);
    }
}