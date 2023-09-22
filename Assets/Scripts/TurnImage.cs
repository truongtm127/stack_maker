using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnImage : MonoBehaviour
{
    /*public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }*/
    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;

    private bool image = true;
    public void TurnImage12()
    {
        image1.SetActive(image);
        image2.SetActive(image);
        image = !image;
    }
}
