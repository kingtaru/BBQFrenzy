using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaruClass;
using UnityEngine.UI;
using UnityEngine.Experimental.PlayerLoop;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public StatusPage statusPage;
    public List<CanvasGroup> canvasGroupsPages;
    public CanvasGroup settingPage;
    public CanvasGroup currentPage;
    [HideInInspector]
    public CanvasGroup previousPage;
    public List<ImageAssets> imageAssets;
    //[HideInInspector]
    public List<ImageAssetsToStore> imageAssetsToStore;
    public List<QuestionsDetails> questionsDetails;
    [Header("Time")]
    public TextMeshProUGUI timeText;
    public float timeWork;
    public float timeFinish;
    [Header("Setting Script")]
    public Setting setting;
    public bool allowCheat;
    public int totalCorrectAnswer;
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "GameManager";
        foreach (CanvasGroup canvasGroup in canvasGroupsPages) {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.ignoreParentGroups = false;
            canvasGroup.interactable = false;
        }
        statusPage = StatusPage.mainPage;
        currentPage = canvasGroupsPages[0];
        currentPage.alpha = 1;
        currentPage.blocksRaycasts = true;
        //currentPage.ignoreParentGroups = true;
        currentPage.interactable = true;
        foreach (ImageAssets assets in imageAssets) {
            ImageAssetsToStore imageAssetsToStores = new ImageAssetsToStore();
            imageAssetsToStores.name = assets.name;
            imageAssetsToStores.image = loadTex(assets.image);
            imageAssetsToStore.Add(imageAssetsToStores);
        }
    }
	void Update()
	{
        Timecount();
        setting.allowCheat = allowCheat;
    }
	Texture2D loadTex(Sprite sprite) {
        Texture2D texture;
        texture = sprite.texture;
        return texture;
    
    }
    public void OnChangeStatus(string status) {
        //CancelInvoke("Timecount");
        StatusPage newStatus = (StatusPage)System.Enum.Parse(typeof(StatusPage), status);
        statusPage = newStatus;
        switch (statusPage) {
            case StatusPage.mainPage:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[0];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<MainPage>().OnChangeTo();
                break;
            case StatusPage.tutorial:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[1];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<TutorialScript>().OnChangeTo();
                break;
            case StatusPage.stage1:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[2];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<Stage_1>().OnChangeTo(1);
                break;

            case StatusPage.stage2:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[3];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<Stage_1>().OnChangeTo(16);
                break;
            case StatusPage.stage3:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[4];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<Stage_1>().OnChangeTo(31);
                break;
            case StatusPage.stage4:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[5];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<Stage_45>().OnChangeTo(46);
                break;
            case StatusPage.stage5:
                previousPage = currentPage;
                currentPage = canvasGroupsPages[6];
                PageTransition(currentPage, previousPage);
                currentPage.GetComponent<Stage_45>().OnChangeTo(61);
                break;
            case StatusPage.setting:
                OCSettingPage();
                break;
            default:
                break;
        }

    }

    public void Timecount() {
        if (statusPage != StatusPage.mainPage && statusPage != StatusPage.tutorial && statusPage != StatusPage.setting) {
            
            timeWork = timeWork + Time.deltaTime;
            double roundedTIme = Math.Round((double)timeWork, 0);
            timeText.text = roundedTIme.ToString();
        }
        
        
    }
    public void PageTransition(CanvasGroup onPage,CanvasGroup offPage) {
        offPage.interactable = false;
        offPage.blocksRaycasts = false;
        offPage.DOFade(0, .3f).SetDelay(.2f) ;

        onPage.DOFade(1, .2f).OnComplete(delegate {
            onPage.interactable = true;
            onPage.blocksRaycasts = true;
        });
    }
    bool settingStatus;
    public void OCSettingPage() {
        if (statusPage == StatusPage.setting)
        {
            settingPage.DOFade(1, .5f).OnComplete(delegate {
                settingPage.interactable = true;
                settingPage.blocksRaycasts = true;
                settingPage.ignoreParentGroups = true;
            });
        }
        else {
            settingPage.interactable = false;
            settingPage.blocksRaycasts = false;
            settingPage.ignoreParentGroups = false;
            settingPage.DOFade(0, .5f);
        }
        
    }

}
