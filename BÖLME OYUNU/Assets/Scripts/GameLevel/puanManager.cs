using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puanManager : MonoBehaviour
{
    private int toplampuan;
    private int puanArtisi;

    [SerializeField]
    private Text puanText;

    void Start()
    {

        puanText.text = toplampuan.ToString();
        
    }

    public void PuaniArtir(string zorlukSeviyesi)
    {
        switch(zorlukSeviyesi)
        {
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
        }
        toplampuan += puanArtisi;
        puanText.text = toplampuan.ToString();
    }

    
}
