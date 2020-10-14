using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TaruClass;
using UnityEngine.UI;
using System;

public class Setting : MonoBehaviour
{
    GameManager gameManager;
    public StatusPage statusPage;
    public CanvasGroup cheatParent;
    [Header("Cheat Button")]
    public List<Button> buttons;
    public CanvasGroup cheatLayer;
    bool cheatShow;
    [Header("Cheat Status")]
    public bool allowCheat;
    // Start is called before the first frame update
    void Start()
    {
			cheatParent.interactable = allowCheat;
			cheatParent.blocksRaycasts = allowCheat;
	}

    // Update is called once per frame
    void LateUpdate()
    {
		cheatParent.interactable = allowCheat;
		cheatParent.blocksRaycasts = allowCheat;
	}
    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }
    public void ForcingStage(string pagesName) {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.OnChangeStatus(pagesName);
    }

	#region Cheat Manager
	public void CheatButton(string layer) {
        switch (layer) {
            case ("L1"):
                offbtn();
                buttons[0].interactable = false;
                buttons[0].GetComponent<Image>().raycastTarget = false;
                break;
            case ("L3"):
                offbtn();
                buttons[2].interactable = false;
                buttons[2].GetComponent<Image>().raycastTarget = false;
                break;
            case ("L2"):
                offbtn();
                buttons[1].interactable = false;
                buttons[1].GetComponent<Image>().raycastTarget = false;
                break;
            case ("L4"):
                cheatLayer.alpha = 1;
                OnOffLayer();
                break;

        }
    }
    public void offbtn() {
        //buttons[0].interactable = false;
        //buttons[0].GetComponent<Image>().raycastTarget = false;
        Invoke("onbtn", 1f);
    }
    public void onbtn() {
        foreach (Button b in buttons) {
            b.interactable = true;
            b.GetComponent<Image>().raycastTarget = true;
        }
       // buttons[0].interactable = true;
        //buttons[0].GetComponent<Image>().raycastTarget = true;
    }
    public void OnOffLayer() {
        if (cheatShow == false) {
            cheatShow = true;
            cheatLayer.alpha = 1;
            cheatLayer.interactable = true;
            cheatLayer.blocksRaycasts = true;
            cheatLayer.ignoreParentGroups = true;
        }
        else if (cheatShow == true)
        {
            cheatShow = false;
            cheatLayer.alpha = 0;
            cheatLayer.interactable = false;
            cheatLayer.blocksRaycasts = false;
            cheatLayer.ignoreParentGroups = false;
        }
    }
	#endregion Cheat Manager
}
