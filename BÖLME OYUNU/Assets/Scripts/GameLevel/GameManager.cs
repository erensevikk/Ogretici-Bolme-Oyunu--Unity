using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]

    private Transform karelerPaneli;

    [SerializeField]
    private Text soruText;

    private GameObject[] karelerDizisi = new GameObject[21];

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Sprite[] kareSprites;

    

    List<int> bolumDegerleriListesi = new List<int>();

    int bolunenSayi, bolenSayi;
    int kacincisoru;
    int dogruSonuc;
    int butonDegeri;

    bool butonabasilsinmi;

    string sorununzorlukseviyesi;

    int kalanHak;

    KalanHaklarManager kalanHaklarManager;

    puanManager puantablosu;

    GameObject gecerliKare;

    [SerializeField]
    private GameObject sonucPaneli;

    [SerializeField]
    AudioSource Ses;

    public AudioClip buttonsesleri;
    public AudioClip yanliscevap;

    private void Awake()
    {
        kalanHak = 3;

        Ses = GetComponent<AudioSource>();
 

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();

        puantablosu = Object.FindAnyObjectByType<puanManager>();

        kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
    }


    void Start()
    {
        butonabasilsinmi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kareleriOlustur();
        Invoke("SorupaneliniAc",2.2f);
    }

    public void kareleriOlustur()
    {
        for(int i=0;i<21;i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0,kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        BolumDegerleriniTexteYazdir();
        StartCoroutine(DoFadeRoutine());
    }
  
    private void ButonaBasildi()
    {
        if(butonabasilsinmi)
        {
          butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

          SonucuKontrolEt();
        }
        
    }
    void SonucuKontrolEt()
    {
        if (butonDegeri == dogruSonuc)
        {
            Ses.PlayOneShot(buttonsesleri);
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<Text>().enabled = false;
            gecerliKare.transform.GetComponent<Button>().interactable = false;


            puantablosu.PuaniArtir(sorununzorlukseviyesi);

            bolumDegerleriListesi.RemoveAt(kacincisoru);

            if (bolumDegerleriListesi.Count > 0)
            {
                SorupaneliniAc();
            }
            else
            {
                OyunBitti();
            }

            SorupaneliniAc();
            Debug.Log(bolumDegerleriListesi.Count);
        }

        else
        {
            Ses.PlayOneShot(yanliscevap);
            kalanHak--;
            kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
        }

        if (kalanHak <= 0)
        {

            OyunBitti();
        }
    }
        void OyunBitti()
        {
        butonabasilsinmi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        }
    
    IEnumerator DoFadeRoutine()                                             
    {
        foreach(var kare in karelerDizisi)                                              
        {
            kare.GetComponent<CanvasGroup>().DOFade(1,0.2f);
            yield return new WaitForSeconds(0.07f);
        }
    }
    void BolumDegerleriniTexteYazdir()
    {
        foreach(var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerleriListesi.Add(rastgeleDeger);

            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();
        }
        
    }
    
    void SorupaneliniAc()
    {
        SoruyuSor();
        butonabasilsinmi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        
    }

    void SoruyuSor()
    {
        bolenSayi = Random.Range(2, 11);

        kacincisoru = Random.Range(0, bolumDegerleriListesi.Count);

        dogruSonuc = bolumDegerleriListesi[kacincisoru];

        bolunenSayi = bolenSayi * dogruSonuc;

        if (bolunenSayi <= 30)
            sorununzorlukseviyesi = "kolay";
        else if (bolunenSayi>30 && bolunenSayi<= 77)
            sorununzorlukseviyesi = "orta";
        else if (bolunenSayi > 77 && bolunenSayi <= 105)
            sorununzorlukseviyesi = "zor";

       
        soruText.text = bolunenSayi.ToString() + " : " + bolenSayi.ToString();
    }
}
