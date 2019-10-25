using UnityEngine;

public class InGameOpcoes : MonoBehaviour
{
    [SerializeField] private GameObject opcoesTela;

    public void AbreOpcoes()
    {
        opcoesTela.SetActive(true);
        Time.timeScale = 0;
    }

    public void FechaOpcoes()
    {
        Time.timeScale = 1;
    }
}