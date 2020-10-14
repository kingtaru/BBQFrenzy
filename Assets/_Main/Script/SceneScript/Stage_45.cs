﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TaruClass;
using TMPro;

public class Stage_45 : MonoBehaviour
{
	
	public enum LevelSelection{
		TwoSide,ThreeSideLeftRight,ThreeSideLeftBottom
	}
	[Header("Stage Setting Need To Assign")]
	public float setOffsetL;
	public float setOffsetR;
	public LevelSelection levelSelection;

	[Header("Atribute No Need To Assign")]
	[Space(25)]
	public GameManager gameManager;
	[Header("Setting Level")]
	public int currentLevel;
	public QuestionsDetails currentQuestionsDetails;
	public List<QuestionsDetails> questionsDetails;

	//[Header("Stage Setting")]
	public List<Transform> main;
	Transform handIn;
	Transform grill;
	public Transform bbq;
	Transform bbqPos;
	[SerializeField]
	public List<Button> rightListButton, leftListButton,middleButton;
	public CanvasGroup winBg;
	public Image correct;
	public Image wrong;
	public Transform cw_Parent;
	Transform animated;
	public List<BaseControllers45> baseControllers;
	Vector2 defaultBbqPos;
	public TextMeshProUGUI title;
	float widthScreen;
	float heightScreen;
	Vector2 defaultPosRight, defaultPosLeft, defaultGrillPos,defaultGrillSize;

	float setSizeGrill;
	float handInOffeset = 200;
	// Start is called before the first frame update
	void Start()
	{
		//gameManager = transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<GameManager>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		widthScreen = Screen.height + handInOffeset;
		heightScreen = Screen.width + handInOffeset;

		main.Add(transform.GetChild(0));
		winBg = main[0].GetChild(0).GetComponent<CanvasGroup>();
		cw_Parent = main[0].GetChild(6);
		correct = cw_Parent.GetChild(0).GetComponent<Image>();
		wrong = cw_Parent.GetChild(1).GetComponent<Image>();
		title = main[0].GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
		bbq = main[0].GetChild(5).GetChild(0);

		handIn = main[0].GetChild(2).transform;
		grill = main[0].GetChild(4).transform;
		bbqPos = handIn.GetChild(0).GetChild(0).transform;
		defaultPosLeft = grill.GetChild(1).transform.localPosition;
		defaultPosRight = grill.GetChild(2).transform.localPosition;
		defaultGrillPos = grill.transform.localPosition;
		currentLevel = 1;
		winBg.alpha = 0;
		correct.transform.DOScale(Vector2.zero, 0);
		wrong.transform.DOScale(Vector2.zero, 0);
		ResetLevel();
		AssignButton();
		defaultBbqPos = bbqPos.transform.position;
		//defaultBbqPos = bbq.transform.position;
		rightListButton = new List<Button>();
		leftListButton = new List<Button>();


		if (transform.name == "stage4")
		{
			setSizeGrill = 1.36f;
		}
		else if (transform.name == "stage5")
		{
			setSizeGrill = 1.36f;
		}
		defaultGrillSize = grill.transform.localScale;
	}

	public void ResetLevel()
	{
		grill.DOScale(defaultGrillSize, 0f);
		handIn.DOLocalMoveY(heightScreen, 0);
		grill.DOLocalMoveX(widthScreen, 0);
		bbq.SetParent(main[0].GetChild(5));
		bbq.DOScale(Vector2.one, .5f);
		bbq.DOLocalMove(new Vector2(0, heightScreen), 0);
		winBg.DOFade(0, .15f);
		correct.transform.DOScale(Vector2.zero, .15f);
		wrong.transform.DOScale(Vector2.zero, .15f);
		//reset grill
		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		cw_Parent.SetParent(grill.parent);
		cw_Parent.DOLocalMoveX(0, 0);
	}

	public void OnChangeTo(int curlev)
	{
		currentLevel = curlev;
		transform.GetComponent<CanvasGroup>().DOKill();
		transform.GetComponent<CanvasGroup>().DOFade(1, .5f).OnComplete(delegate
		{
			transform.GetComponent<CanvasGroup>().interactable = true;
			transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
			//transform.GetComponent<CanvasGroup>().ignoreParentGroups = true;
			OnCreateLevel();
		});
	}

	public int indexOfLevel;
	bool readyToGo;

	public void OnCreateLevel()
	{
		indexOfLevel = (currentLevel % 15)-1;
		if (indexOfLevel==0 && currentQuestionsDetails.level == 15)
		{
			indexOfLevel = currentLevel % 15;
			string a = transform.name;
			int nextt = int.Parse(a.Substring(a.Length - 1));
			nextt += 1;
			gameManager.OnChangeStatus("stage" + nextt);
		}
		else
		{
			if (indexOfLevel == -1) {
				indexOfLevel = 14;
			}
			currentQuestionsDetails = questionsDetails[indexOfLevel];
			ResetLevel();
			AssignButton();
			AssignImage();
			SettingBase();
			title.text = "Level-" + currentQuestionsDetails.stage;
			//reset grill pos
			grill.transform.DOLocalMoveX(defaultGrillPos.x, .6f).SetEase(Ease.OutBounce).OnComplete(delegate { 
				handIn.DOLocalMoveY(0, .5f).OnComplete(delegate { 
					bbq.DOMoveY(defaultBbqPos.y, .3f).OnComplete(delegate { 
						grill.GetComponent<CanvasGroup>().interactable = true;
					}); 
				});  
			});
		}
		
	}

	// everytime need to assign image to new level
	public void AssignImage()
	{
		foreach (Button btn in leftListButton)
		{
			btn.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0);
			//btn.interactable = false;
		}
		foreach (Button btn in rightListButton)
		{
			btn.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0);
			//btn.interactable = false;
		}
		for (int i = 0; i < currentQuestionsDetails.imagePlaces.Count; i++)
		{
			#region LeftSide
			for (int j = 0; j < leftListButton.Count; j++)
			{
				Transform transforms = leftListButton[j].transform;
				if (transforms.name == currentQuestionsDetails.imagePlaces[i].pos)
				{

					foreach (ImageAssets imageAssets in gameManager.imageAssets)
					{
						if (currentQuestionsDetails.imagePlaces[i].names.ToString() == imageAssets.name)
						{
							Sprite sprite = imageAssets.image;
							transforms.GetChild(1).GetComponent<Image>().sprite = sprite;
							transforms.GetChild(1).GetComponent<Image>().DOFade(1, 0);
						}
					}

				}
			}
			#endregion Leftside
			#region RightSide
			for (int j = 0; j < rightListButton.Count; j++)
			{
				Transform transforms = rightListButton[j].transform;
				if (transforms.name == currentQuestionsDetails.imagePlaces[i].pos)
				{
					foreach (ImageAssets imageAssets in gameManager.imageAssets)
					{
						if (currentQuestionsDetails.imagePlaces[i].names.ToString() == imageAssets.name)
						{
							Sprite sprite = imageAssets.image;
							transforms.GetChild(1).GetComponent<Image>().sprite = sprite;
							transforms.GetChild(1).GetComponent<Image>().DOFade(1, 0);
						}
					}

				}
			}
			#endregion RightSide

			if (i == currentQuestionsDetails.imagePlaces.Count - 1)
			{
				//AssignButton();
			}
		}
	}

	public void AssignButton()
	{
		foreach (Button btn in leftListButton)
		{
			//btn.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0);
			btn.interactable = false;
		}
		foreach (Button btn in rightListButton)
		{
			btn.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0);
			//btn.interactable = false;
		}
		int countLeft = grill.GetChild(1).childCount-1;
		int countRight = grill.GetChild(2).childCount-1;
		leftListButton.Clear();
		rightListButton.Clear();
		for (int i = 1; i < countLeft; i++)
		{
			Button newbtn = grill.GetChild(1).GetChild(i).GetComponent<Button>();
			newbtn.transform.DOKill();
			newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0);
			bool status = false;

			foreach (string cur in currentQuestionsDetails.correctAnswer)
			{
				if (newbtn.name == cur)
				{
					status = true;
				}
				else if(status != true)
				{
					status = false;
				}
			}
			newbtn.onClick.RemoveAllListeners();
			newbtn.onClick.AddListener(delegate { OnButtonCliked(newbtn.transform, status); });
			leftListButton.Add(newbtn);
			foreach (string names in currentQuestionsDetails.availableButton)
			{
				if (newbtn.name == names)
				{
					newbtn.interactable = true;
					newbtn.transform.GetChild(0).DOComplete();
					newbtn.transform.GetChild(0).DOKill();
					newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(1, .5F).SetLoops(-1, LoopType.Yoyo);
				}
				else if (newbtn.name != names && newbtn.interactable == false)
				{
					newbtn.interactable = false;
				}
			}
		}
		for (int i = 1; i < countRight - 1; i++)
		{
			Button newbtn = grill.GetChild(2).GetChild(i).GetComponent<Button>();
			newbtn.transform.DOKill();
			newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0);
			bool status = false;
			foreach (string cur in currentQuestionsDetails.correctAnswer)
			{
				if (newbtn.name == cur)
				{
					status = true;
				}
				else if(status != true)
				{
					status = false;
				}
			}
			newbtn.interactable = status;
			newbtn.onClick.RemoveAllListeners();
			newbtn.onClick.AddListener(delegate { OnButtonCliked(newbtn.transform, status); });
			rightListButton.Add(newbtn);
			foreach (string names in currentQuestionsDetails.availableButton)
			{
				if (newbtn.name == names)
				{
					newbtn.interactable = true;
					newbtn.transform.GetChild(0).DOComplete();
					newbtn.transform.GetChild(0).DOKill();
					newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
				}
				else if (newbtn.name != names && newbtn.interactable == false)
				{
					newbtn.interactable = false;
				}
			}
		}
	}

	float newLastpos;
	public void CloseGrill()
	{
		grill.GetChild(1).DOLocalMoveX(setOffsetL, .3f);
		grill.GetChild(1).GetChild(0).GetComponent<Image>().DOFade(0, .2f);
		grill.GetChild(1).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(1, .1f);
		grill.GetChild(1).DORotate(new Vector2(0, 180), .3f).OnComplete(delegate
		{

			//if (currentLevel < 16)
			//{
			//	newLastpos = 140;
			//}
			//else if (currentLevel < 31 && currentLevel > 15)
			//{
			//	newLastpos = main[0].GetChild(4).GetChild(0).transform.localPosition.x / 1.5f;
			//}
			//else if (currentLevel < 45 && currentLevel > 30)
			//{

			//	newLastpos = main[0].GetChild(4).GetChild(0).transform.localPosition.x / 2;
			//}
			//newLastpos = Mathf.Abs(newLastpos);
			//Debug.Log(newLastpos);
			//grill.DOLocalMoveX(0, .3f).OnComplete(delegate {
			//	grill.DOScale(setSizeGrill, .3f).SetEase(Ease.OutBounce);
			//});
			Invoke("CloseGrillSecond", .3f);
		});


		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(0, .2f).SetDelay(.3f);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(1, .2f).SetDelay(.3f);
		if (levelSelection == LevelSelection.ThreeSideLeftRight)
		{
			grill.GetChild(2).DORotate(new Vector2(0, 180), .3f).SetDelay(.3f);
			grill.GetChild(2).DOLocalMoveX(setOffsetR, .3f).SetDelay(.3f);
		}
		else if (levelSelection == LevelSelection.ThreeSideLeftBottom)
		{
			grill.GetChild(2).DORotate(new Vector2(180, 0), .3f).SetDelay(.3f);
			grill.GetChild(2).DOLocalMoveY(setOffsetR, .3f).SetDelay(.3f);
		}


		//grill.GetChild(2).DOLocalMoveX(setOffsetR, .3f);



	}
	public void CloseGrillSecond()
	{
		if (currentLevel < 16)
		{
			newLastpos = 140;
		}
		else if (currentLevel < 31 && currentLevel > 15)
		{
			newLastpos = main[0].GetChild(4).GetChild(0).transform.localPosition.x / 1.5f;
		}
		else if (currentLevel < 45 && currentLevel > 30)
		{

			newLastpos = main[0].GetChild(4).GetChild(0).transform.localPosition.x / 2;
		}
		newLastpos = Mathf.Abs(newLastpos);
		Debug.Log(newLastpos);
		grill.DOLocalMoveX(0, .3f).OnComplete(delegate {
			grill.DOScale(setSizeGrill, .3f).SetEase(Ease.OutBounce);
		});

	}
	public void OpenGrill()
	{
		grill.GetChild(2).DOLocalMoveX(defaultPosRight.x, 0);
		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		grill.GetChild(2).DORotate(new Vector2(0, 0), 0).OnComplete(delegate
		{
			//grill.DOLocalMoveX(0, 0);
		});

		grill.GetChild(1).DOLocalMoveX(defaultPosLeft.x, 0);
		grill.GetChild(1).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		grill.GetChild(1).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		grill.GetChild(1).DORotate(new Vector2(0, 0), 0).OnComplete(delegate
		{
			grill.DOLocalMoveX(0, 0);
		});
	}

	public void OnButtonCliked(Transform transformBtn, bool answerStatus)
	{
		grill.GetComponent<CanvasGroup>().interactable = false;
		foreach (Button btn in leftListButton)
		{
			btn.interactable = false;
		}
		foreach (Button btn in rightListButton)
		{
			btn.interactable = false;
		}
		bbq.SetParent(transformBtn);
		//without fall
		bbq.DOKill();
		
		//bbq.DOScale(new Vector2(0.592925f, 0.592925f), .2f);
		bbq.DOScale(Vector2.one, .2f);
		bbq.DOMove(transformBtn.position, .2f).OnComplete(delegate
		{
			bool gotForbiden = false;
			foreach (string inForbidenBtn in currentQuestionsDetails.forbidenButton) {
				if (transformBtn.name == inForbidenBtn) {
					gotForbiden = true;
				}
			}
			if (gotForbiden)
			{
				//with fall
				bbq.SetParent(main[0].GetChild(5));
				ForbidenBtnAction();
			}
			else {
				//bbq.SetParent(transformBtn);
			}
			Invoke("CloseGrill", .25f);
		});
		float delayset = 1.75f;
		Debug.Log(answerStatus);
		winBg.DOFade(1, .3f).SetDelay(delayset);

		if (answerStatus == true) { 
			animated = correct.transform;
			gameManager.totalCorrectAnswer++;
		}

		else if (answerStatus == false) animated = wrong.transform;

		foreach (Button button in leftListButton)
		{
			button.transform.GetChild(0).GetComponent<Image>().DOKill();
			button.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0f);
		}
		foreach (Button button in rightListButton)
		{
			button.transform.GetChild(0).GetComponent<Image>().DOKill();
			button.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0f);
		}
		//
		animated.transform.DOScale(Vector2.one, .3f).SetDelay(delayset).OnComplete(delegate
		{
			cw_Parent.SetParent(grill);
			handIn.DOLocalMoveY(heightScreen, .6f);
			winBg.DOFade(0, .3f).SetDelay(.85f);
			animated.transform.DOScale(Vector2.zero, .3f).SetDelay(.5f);
			currentLevel += 1;
			grill.DOLocalMoveX(-widthScreen, 1.5f).SetDelay(0).OnComplete(delegate
			{
				Debug.Log("up");
				
				Invoke("OnCreateLevel", .1f);
			});
		});
	}

	public void SettingBase()
	{
		OpenGrill();
		int indexs = currentQuestionsDetails.level - 1;
		grill.GetChild(1).GetChild(0).GetComponent<Image>().sprite = baseControllers[indexs].left;
		grill.GetChild(2).GetChild(0).GetComponent<Image>().sprite = baseControllers[indexs].right;
		int secondIndex = grill.GetChild(2).childCount;
		grill.GetChild(2).GetChild(secondIndex-1).GetComponent<Image>().sprite = baseControllers[indexs].right;
	}

	public void ForbidenBtnAction()
	{
		Vector2 newDestination = new Vector2(bbq.localPosition.x, -(heightScreen));
		bbq.DOLocalMove(newDestination, 1f).SetDelay(0f);
	}
}