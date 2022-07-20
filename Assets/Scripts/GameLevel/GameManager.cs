using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] kokIciResimler;

    [SerializeField]
    private Sprite[] kokDisiResimler;

    [SerializeField]
    private Image morKokDisiResim, maviKokDisiResim, griKokDisiResim;

    [SerializeField]
    private Image sariKokDisiResim, pembeKokDisiResim, yesilKokDisiResim;

    [SerializeField]
    private Image ustKokIciResim, altKokIciResim;

    [SerializeField]
    private Transform sorularDairesi;

    [SerializeField]
    private Transform solBar, sagBar;

    [SerializeField]
    private GameObject trueIcon, falseIcon,dogruYanliObje;

    [SerializeField]
    private Text dogruText, yanlisText,puanText;

    [SerializeField]
    private GameObject bonusObje;


    int hangiSoru;



    bool daireUsttemi;

    bool daireDonsunmu;


    string butondakiResim;


    Vector3 solBarBirinciYer = new Vector3(-192f, 68f, 0f);
    Vector3 solBarIkinciYer = new Vector3(-128f, 68f, 0f);
    Vector3 solBarUcuncuYer = new Vector3(-79f, 68f, 0f);

    Vector3 sagBarBirinciYer = new Vector3(202f, 68f, 0f);
    Vector3 sagBarIkinciYer = new Vector3(131f, 68f, 0f);
    Vector3 sagBarUcuncuYer = new Vector3(85f, 68f, 0f);

    int kacinciYanlis;

    public int dogruAdeti, yanlisAdeti;

    public int toplamPuan, puanArtisi;
    int bonusAdet;

    bool butonaBasilsinmi;

    [SerializeField]
    private AudioClip baslangicClip;

    [SerializeField]
    private AudioClip daireSesi, barKapanisSesi;

  

   

    void Start()
    {
        daireUsttemi = true;
        daireDonsunmu = true;

        butonaBasilsinmi = true;

        dogruAdeti = 0;
        yanlisAdeti = 0;
        kacinciYanlis = 0;

        toplamPuan = 0;
        puanArtisi = 0;
        bonusAdet = 0;


        puanText.text = toplamPuan.ToString();


        SesiCikar(baslangicClip);

        ResimleriYerlestir();
    }

    void ResimleriYerlestir()
    {
        hangiSoru = Random.Range(0, kokDisiResimler.Length - 3);


        

        int rastgeleDeger = Random.Range(0, 100);

        if (daireUsttemi)
        {
            if (rastgeleDeger <= 33)
            {
                morKokDisiResim.sprite = kokDisiResimler[hangiSoru];
                maviKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                griKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
            }
            else if (rastgeleDeger <= 66)
            {
                morKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                maviKokDisiResim.sprite = kokDisiResimler[hangiSoru];
                griKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
            }
            else
            {
                morKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                maviKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
                griKokDisiResim.sprite = kokDisiResimler[hangiSoru];
            }
        }
        else
        {
            if (rastgeleDeger <= 33)
            {
                sariKokDisiResim.sprite = kokDisiResimler[hangiSoru];
                pembeKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                yesilKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
            }
            else if (rastgeleDeger <= 66)
            {
                sariKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                pembeKokDisiResim.sprite = kokDisiResimler[hangiSoru];
                yesilKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
            }
            else
            {
                sariKokDisiResim.sprite = kokDisiResimler[hangiSoru + 1];
                pembeKokDisiResim.sprite = kokDisiResimler[hangiSoru + 2];
                yesilKokDisiResim.sprite = kokDisiResimler[hangiSoru];
            }
        }

        if (daireUsttemi)
        {
            ustKokIciResim.sprite = kokIciResimler[hangiSoru];
        }
        else
        {
            altKokIciResim.sprite = kokIciResimler[hangiSoru];
        }


        daireUsttemi = !daireUsttemi;
    }

    public void ButonaBasildi()
    {
        butondakiResim = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;

        if(butonaBasilsinmi)
        {
            butonaBasilsinmi = false;
            SonucuKontrolEt();
        }

        

        
        
    }


    void SonucuKontrolEt()
    {
        if (butondakiResim == kokDisiResimler[hangiSoru].name)
        {
            //dogrular
            dogruAdeti++;
            bonusAdet++;



            dogruText.text = dogruAdeti.ToString();
            DogruYanlisIconGoster(true);
            DaireyiCevir();

            

            if(bonusAdet>=5 && bonusAdet<=9)
            {
                puanArtisi += 30;
                BonusScaleOn();
            }
            else
            {
                
                puanArtisi += 20;
            }

            if(bonusAdet>9)
            {
                bonusAdet = 0;
                BonusScaleOff();
            }

            

           
        }
        else
        {
            BonusScaleOff();
            yanlisAdeti++;
            bonusAdet = 0;
            yanlisText.text = yanlisAdeti.ToString();
            DogruYanlisIconGoster(false);
            kacinciYanlis++;
            BarlariKapat(kacinciYanlis);
            puanArtisi -= 5;
        }

       
        toplamPuan += puanArtisi;

        if(toplamPuan<=0)
        {
            toplamPuan = 0;
        }
        puanArtisi = 0;
        puanText.text = toplamPuan.ToString();
    }


    void DaireyiCevir()
    {
        if (daireDonsunmu)
        {
            daireDonsunmu = false;
            kacinciYanlis = 0;

            solBar.DOLocalMove(solBarBirinciYer, 0.2f);
            sagBar.DOLocalMove(sagBarBirinciYer, 0.2f);

            SesiCikar(daireSesi);

            ResimleriYerlestir();

            sorularDairesi.DORotate(sorularDairesi.rotation.eulerAngles + new Vector3(0, 0, 180f), 0.5f).OnComplete(daireDonsunmuTrueYap);
        }
    }

    void daireDonsunmuTrueYap()
    {
        butonaBasilsinmi = true;
        daireDonsunmu = true;
       
    }

    void BarlariKapat(int kacinciYanlis)
    {

        SesiCikar(barKapanisSesi);
        if(kacinciYanlis==1)
        {
            butonaBasilsinmi = true;
            solBar.DOLocalMove(solBarIkinciYer, 0.2f);
            sagBar.DOLocalMove(sagBarIkinciYer, 0.2f);
        } else if(kacinciYanlis==2)
        {
            solBar.DOLocalMove(solBarUcuncuYer, 0.2f);
            sagBar.DOLocalMove(sagBarUcuncuYer, 0.2f).OnComplete(BarKapanisiniBekle);
        }

        
    }

    void BarKapanisiniBekle()
    {
        daireDonsunmu = true;
        Invoke("DaireyiCevir", 1f);
    }

    void DogruYanlisIconGoster(bool dogrumu)
    {
        dogruYanliObje.GetComponent<CanvasGroup>().alpha = 0;
        if(dogrumu)
        {
            trueIcon.SetActive(true);
            falseIcon.SetActive(false);
        } else
        {
            falseIcon.SetActive(true);
            trueIcon.SetActive(false);
        }
        dogruYanliObje.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(trueFalsIconuAlphaKapat);
    }

    void trueFalsIconuAlphaKapat()
    {
        dogruYanliObje.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
    }


    void BonusScaleOn()
    {
        bonusObje.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutElastic);
    }

    void BonusScaleOff()
    {
        bonusObje.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InElastic);
    }

    public void GeriDon()
    {
        SceneManager.LoadScene("EgitimLevel");
    }



    void SesiCikar(AudioClip clip)
    {
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
    }

}

