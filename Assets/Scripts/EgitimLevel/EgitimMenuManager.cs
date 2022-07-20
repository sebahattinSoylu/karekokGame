using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EgitimMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startBtn, geriDonbtn;

    [SerializeField]
    private GameObject fadePanel;

    [SerializeField]
    private GameObject kokiciPrefab;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private Sprite[] kokiciResimler;

    [SerializeField]
    private Sprite[] kokDisiResimler;

    [SerializeField]
    private Image kokDisiImage;

    [SerializeField]
    private Text aciklamaText;


    [SerializeField]
    private AudioClip alistirmaClip;

    void Start()
    {
        aciklamaText.text = "";
        fadePanel.SetActive(true);
        
        fadePanel.GetComponent<CanvasGroup>().alpha = 1;

        fadePanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(IlkAyariYap);

        kokDisiImage.sprite = kokDisiResimler[0];

        if (startBtn!=null)
        {
            startBtn.GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        if (geriDonbtn != null)
        {
            geriDonbtn.GetComponent<RectTransform>().localScale = Vector3.zero;
        }


       
    }

    void IlkAyariYap()
    {
        fadePanel.SetActive(false);

        aciklamaText.text = "Alttaki panelden resimleri sürekleyerek istediğiniz resme tıklayıp kök değerini öğrenebilirsiniz. ";

        SesiCikar(alistirmaClip);


        ButonlariAc();
        KokiciResimleriOlustur();
    }

    void ButonlariAc()
    {
               
        startBtn.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
        geriDonbtn.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
    }


   void KokiciResimleriOlustur()
    {
        for(int i=0;i<kokiciResimler.Length;i++)
        {
            GameObject kokiciItem = Instantiate(kokiciPrefab, content);

            kokiciItem.GetComponent<KokiciButonManager>().butonNo = i;

            kokiciItem.transform.GetChild(3).GetComponent<Image>().sprite = kokiciResimler[i];
            
            
                
        }

        
      
    }

    public void KokDisiResimiGoster(int butonNo)
    {
        kokDisiImage.sprite = kokDisiResimler[butonNo];
    }


    public void MenuLevelineDon()
    {
        SceneManager.LoadScene("MenuLevel");
    }
   

   public void OyunLevelineGit()
    {
        SceneManager.LoadScene("GameLevel");
    }


    void SesiCikar(AudioClip clip)
    {
        if(clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
    }

   
    
}
