using System;
using UnityEngine;


public class Dado : MonoBehaviour
{
    public GameObject[] dados;
    public Animator[] dadosAnimator;
    private System.Random random = new System.Random();

    public void PararFace(int indice, int resultado)
    {
        dadosAnimator[indice].enabled = false;
        dadosAnimator[indice].gameObject.transform.rotation = Quaternion.identity;
        switch(resultado)
        {
            case 1:
                dados[indice].transform.Rotate(0.0f, 0.0f, 0.0f, Space.World);
                break;
            case 2:
                dados[indice].transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
                break;
            case 3:
                dados[indice].transform.Rotate(0.0f, -90.0f, -90.0f, Space.World);
                break;
            case 4:
                dados[indice].transform.Rotate(0.0f, -90.0f, -180.0f, Space.World);
                break;
            case 5:
                dados[indice].transform.Rotate(0.0f, -180.0f, -180.0f, Space.World);
                break;
            case 6:
                dados[indice].transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
                break;
        }
    }

    public int RolaDado()
    {
        return random.Next(1, 7);
    }
}