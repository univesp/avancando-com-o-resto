using UnityEngine;
using UnityEngine.UI;

public class ScrollbarFix : MonoBehaviour
{
    [SerializeField] private Scrollbar sb;

    private void Awake()
    {
        sb.value = 1;
        sb.size = 0;
    }

    public void ResetSize()
    {
        sb.size = 0;
    }
}
