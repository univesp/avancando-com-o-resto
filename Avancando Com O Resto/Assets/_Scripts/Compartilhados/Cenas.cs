using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{
    public void CarregarCena(int _indexCena)
    {
        SceneManager.LoadScene(_indexCena);
    }
}
