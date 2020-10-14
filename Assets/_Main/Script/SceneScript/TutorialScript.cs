using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
	public List<Transform> main;
	Transform handIn;
	Transform grill;
	public Transform bbq;
	Transform bbqPos;
	public CanvasGroup currentCanvasGroup;
	[SerializeField]
	List<Button> rightListButton, leftListButton;
	public CanvasGroup winBg;
	public Image correct;
	public Image wrong;
	public Transform cw_Parent;
	public int currentLevel;
	public List<string> availableClick;
	public string correctAnswer;
	Transform animated;
	Vector2 defaultBbqPos;
	public CanvasGroup lastButton;
	public Image bbqs;
	float setSizeGrill;
	Vector2 defaultPosRight, defaultPosLeft, defaultGrillPos, defaultGrillSize;
	float widthScreen, heightScreen;
	float handInOffset = 200;

	// Start is called before the first frame update
	void Start()
	{
		widthScreen = Screen.height + handInOffset;
		heightScreen = Screen.width + handInOffset;


		handIn = main[0].GetChild(2).transform;
		grill = main[0].GetChild(4).transform;
		bbqPos = handIn.GetChild(0).GetChild(0).transform;
		defaultPosLeft = grill.GetChild(1).transform.localPosition;
		defaultPosRight = grill.GetChild(2).transform.localPosition;
		defaultGrillPos = grill.transform.localPosition;
		currentLevel = 0;
		winBg.alpha = 0;
		correct.transform.DOScale(Vector2.zero, 0);
		wrong.transform.DOScale(Vector2.zero, 0);
		ResetLevel();
		AssignButton();
		//defaultBbqPos = bbq.transform.position;
		defaultBbqPos = bbqPos.transform.position;
		rightListButton = new List<Button>();
		leftListButton = new List<Button>();
		if (transform.name == "stage1")
		{
			setSizeGrill = 1.36f;
		}
		else if (transform.name == "stage2")
		{
			setSizeGrill = 1.2f;
		}
		else if (transform.name == "stage3")
		{
			setSizeGrill = 1.2f;
		}
		defaultGrillSize = grill.transform.localScale;
	}

	public void ResetLevel()
	{

		//handIn.DOLocalMoveY(heightScreen, 0);
		//grill.DOLocalMoveX(widthScreen, 0);
		//bbq.DOLocalMove(new Vector2(0, heightScreen), 0);
		//winBg.DOFade(0, .5f);
		//correct.transform.DOScale(Vector2.zero, .15f);
		//wrong.transform.DOScale(Vector2.zero, .15f);
		//bbq.SetParent(main[0].GetChild(5));
		//OpenGrill();
		//AssignButton();
		//main[0].GetChild(6).GetComponent<CanvasGroup>().DOFade(0, 0f);
		//grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		//grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		lastButton.interactable = false;
		lastButton.blocksRaycasts = false;
		lastButton.ignoreParentGroups = false;
		lastButton.DOFade(0, 0f);
		grill.DOScale(defaultGrillSize.x, 0f);
		handIn.DOLocalMoveY(heightScreen, 0);
		grill.DOLocalMoveX(widthScreen, 0);
		bbq.SetParent(main[0].GetChild(5));
		bbq.DOScale(Vector2.one, .5f);
		bbq.DOLocalMove(new Vector2(0, heightScreen), 0);
		winBg.DOFade(0, .15f);
		correct.transform.DOScale(Vector2.zero, .15f);
		wrong.transform.DOScale(Vector2.zero, .15f);
		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		cw_Parent.SetParent(grill.parent);
		cw_Parent.DOLocalMoveX(0, 0);
		AssignButton();
		OpenGrill();

	}

	public void OnChangeTo()
	{
		currentCanvasGroup.interactable = true;
		currentCanvasGroup.blocksRaycasts = true;
		currentCanvasGroup.DOFade(1, .5f);
		transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetComponent<ObjectAnimation>().GetDrawTrigger(1);
		for (int i = 1; i < transform.GetChild(0).childCount; i++)
		{
			if (i == 1)
			{
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = true;
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = true;
			}
			else
			{
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().interactable = false;
				transform.GetChild(0).GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = false;
			}

		}
	}
	public void SkipClosingAnimationEvent() {
		Invoke("alternatifskip", 1);
	}
	void alternatifskip() {

		grill.GetComponent<CanvasGroup>().interactable = false;
		Invoke("CloseGrill", .2f);
		handIn.DOLocalMoveY(heightScreen, .6f).SetDelay(2).OnComplete(delegate
		{
			currentLevel += 1;
		});
		grill.DOLocalMoveX(-widthScreen, 1.5f).SetDelay(2).OnComplete(delegate
		{
			Invoke("OnMainTutorial", .1f);
		});
	}


	public void OnMainTutorial()
	{

		if (currentLevel == 0)
		{
			main[0].GetChild(6).GetChild(0).GetComponent<Image>().DOFade(0, 0);
			ResetLevel();
			leftListButton[0].interactable = false;
			leftListButton[3].interactable = false;
			grill.transform.DOLocalMoveX(defaultGrillPos.x, .5f).SetEase(Ease.OutBounce).OnComplete(delegate
			{
				handIn.DOLocalMoveY(0, .5f).OnComplete(delegate
				{
					bbq.GetComponent<Image>().DOFade(0, 0);
					bbq.DOMoveY(defaultBbqPos.y, .3f).OnComplete(delegate
					{
						main[0].GetChild(6).GetComponent<CanvasGroup>().DOFade(1, .5f).OnComplete(delegate {
							//Invoke("SkipClosingAnimationEvent", 1);
						});
					});
				}); ;
			});

		}
		else if (currentLevel == 1)
		{
			ResetLevel();
			leftListButton[0].interactable = true;
			leftListButton[3].interactable = false;
			main[0].GetChild(6).GetChild(0).GetComponent<Image>().DOFade(1, .5f);
			bbq.GetComponent<Image>().DOFade(1, 0);
			main[0].GetChild(6).DOLocalMove(new Vector2(-260, -80), 0f);
			grill.transform.DOLocalMoveX(defaultGrillPos.x, .6f).SetEase(Ease.OutBounce).OnComplete(delegate {
				handIn.DOLocalMoveY(0f, .5f).OnComplete(delegate {
					bbq.DOMoveY(defaultBbqPos.y, .3f).OnComplete(delegate {
						grill.GetComponent<CanvasGroup>().interactable = true;
					});
				}); ;
			});

		}
		else if (currentLevel == 2)
		{
			ResetLevel();
			leftListButton[3].interactable = true;
			leftListButton[0].interactable = false;
			main[0].GetChild(6).GetChild(0).GetComponent<Image>().DOFade(1, .5f);
			main[0].GetChild(6).DOLocalMove(new Vector2(-150, -210), 0f);
			grill.transform.DOLocalMoveX(defaultGrillPos.x, .6f).SetEase(Ease.OutBounce).OnComplete(delegate {
				handIn.DOLocalMoveY(0f, .5f).OnComplete(delegate {
					bbq.DOMoveY(defaultBbqPos.y, .3f).OnComplete(delegate {
						grill.GetComponent<CanvasGroup>().interactable = true;
					});
				}); ;
			});
		}
		else if (currentLevel == 3)
		{
			main[0].GetChild(6).GetChild(0).GetComponent<Image>().DOFade(0, .5f);
			lastButton.DOFade(1, .5f).OnComplete(delegate
			{
				lastButton.interactable = true;
				lastButton.blocksRaycasts = true;
				lastButton.ignoreParentGroups = true;
			});

		}
	}

	public void GoToStage1()
	{
		lastButton.interactable = false;
		lastButton.blocksRaycasts = false;
		lastButton.ignoreParentGroups = false;
		lastButton.DOFade(0, .5f).OnComplete(delegate
		{
			GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
			gameManager.OnChangeStatus("stage1");
		});


	}
	public void OnButtonCliked(Transform transformBtn, bool answerStatus)
	{
		//foreach (Button btn in leftListButton)
		//{
		//	btn.interactable = false;
		//}
		//foreach (Button btn in rightListButton)
		//{
		//	btn.interactable = false;
		//}
		//bbq.DOKill();

		//main[0].GetChild(6).GetComponent<CanvasGroup>().DOFade(0, .5f);
		//bbq.DOMove(transform.position, .3f).OnComplete(delegate
		//{
		//	bbq.SetParent(transform);
		//	Invoke("CloseGrill", .25f);
		//});
		//float delayset = 1.75f;
		//Debug.Log(answerStatus);
		//winBg.DOFade(1, .5f).SetDelay(delayset);

		//if (answerStatus == true) animated = correct.transform;

		//else if (answerStatus == false) animated = wrong.transform;

		//foreach (Button button in leftListButton) { 
		//	button.transform.GetChild(0).GetComponent<Image>().DOKill();
		//	button.transform.GetChild(0).GetComponent<Image>().DOFade(0, .5f);
		//}
		//foreach (Button button in rightListButton)
		//{
		//	button.transform.GetChild(0).GetComponent<Image>().DOKill();
		//	button.transform.GetChild(0).GetComponent<Image>().DOFade(0, .5f);
		//}

		//animated.transform.DOScale(Vector2.one, .5f).SetDelay(delayset).OnComplete(delegate
		//{
		//	handIn.DOLocalMoveY(1200, 1);
		//	winBg.DOFade(0, .3f).SetDelay(2.5f);
		//	animated.transform.DOScale(Vector2.zero, .3f).SetDelay(2.5f);

		//	grill.DOLocalMoveX(-900, .8f).SetDelay(3).OnComplete(delegate
		//	{
		//		currentLevel += 1;
		//		Invoke("OnMainTutorial", .1f);
		//	});
		//});
		main[0].GetChild(6).GetChild(0).GetComponent<Image>().DOFade(0, .5f);
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
		bbq.DOKill();

		bbq.DOScale(Vector2.one, .2f);
		bbq.DOMove(transformBtn.position, .2f).OnComplete(delegate
		{
			bool gotForbiden = false;

			Invoke("CloseGrill", .25f);
		});
		float delayset = 1.15f;
		Debug.Log(answerStatus);
		winBg.DOFade(1, .3f).SetDelay(delayset);

		if (answerStatus == true)
		{
			animated = correct.transform;
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

		animated.transform.DOScale(Vector2.one, .3f).SetDelay(delayset).OnComplete(delegate
		{
			cw_Parent.SetParent(grill);
			handIn.DOLocalMoveY(heightScreen, .6f);
			winBg.DOFade(0, .3f).SetDelay(.8f);
			animated.transform.DOScale(Vector2.zero, 0).SetDelay(2f);
			currentLevel += 1;
			grill.DOLocalMoveX(-widthScreen, 1.5f).SetDelay(.5f).OnComplete(delegate
			{
				Invoke("OnMainTutorial", .1f);
			});
		});
	}

	public void OnFirstTutorial(CanvasGroup canvasGroup)
	{

		currentCanvasGroup.DOKill();
		currentCanvasGroup.interactable = false;
		currentCanvasGroup.blocksRaycasts = false;
		float setsDel = 0;
		if (canvasGroup.transform.name == "T (7)")
		{
			setsDel = 0;
			currentCanvasGroup.DOFade(0, .3f).SetDelay(0);
		}
		else
		{
			setsDel = .3f;
			currentCanvasGroup.DOFade(0, .3f).SetDelay(.1f);
		}
		canvasGroup.DOKill();
		canvasGroup.DOFade(1, .3f).OnComplete(delegate
		{
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			currentCanvasGroup = canvasGroup;
		});
		if (canvasGroup.transform.name == "T (7)" || canvasGroup.transform.name == "T (5)")
		{
			bbqs.DOFade(0, .5f);
		}
		else if (canvasGroup.transform.name != "T (7)" && canvasGroup.transform.name != "T (5)" && canvasGroup.transform.name != "MainTutorial")
		{
			bbqs.DOFade(1, .5f);
		}
	}

	public void AssignButton()
	{
		int countLeft = grill.GetChild(2).childCount;
		int countRight = grill.GetChild(1).childCount;
		leftListButton.Clear();
		rightListButton.Clear();
		for (int i = 1; i < countLeft - 1; i++)
		{
			Button newbtn = grill.GetChild(2).GetChild(i).GetComponent<Button>();
			newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0);
			bool status;
			if (newbtn.name == correctAnswer)
			{
				status = true;
			}
			else
			{
				status = false;
			}
			newbtn.onClick.RemoveAllListeners();
			newbtn.onClick.AddListener(delegate { OnButtonCliked(newbtn.transform, status); });
			leftListButton.Add(newbtn);
			foreach (string names in availableClick)
			{
				if (newbtn.name == names)
				{
					newbtn.transform.GetChild(0).DOComplete();
					newbtn.transform.GetChild(0).DOKill();
					newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(1, .5F).SetLoops(-1, LoopType.Yoyo);
				}
			}



		}
		for (int i = 1; i < countRight; i++)
		{
			Button newbtn = grill.GetChild(1).GetChild(i).GetComponent<Button>();
			newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0);
			bool status;
			if (newbtn.name == correctAnswer)
			{
				status = true;
			}
			else
			{
				status = false;
			}
			newbtn.onClick.RemoveAllListeners();
			newbtn.onClick.AddListener(delegate { OnButtonCliked(newbtn.transform, status); });
			rightListButton.Add(newbtn);
			foreach (string names in availableClick)
			{
				if (newbtn.name == names)
				{
					newbtn.transform.GetChild(0).DOComplete();
					newbtn.transform.GetChild(0).DOKill();
					newbtn.transform.GetChild(0).GetComponent<Image>().DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
				}
			}
		}
	}

	public void CloseGrill()
	{
		//grill.GetChild(2).DOLocalMoveX(0, .5f);
		//grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(0, .3f);
		//Debug.Log(
		//grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).name);
		//grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(1, .3f);
		//grill.GetChild(2).DORotate(new Vector2(0, 180), .5f).OnComplete(delegate
		//{
		//	grill.DOLocalMoveX(140, .3f);
		//});

		grill.GetChild(2).DOLocalMoveX(-128f, .3f);
		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(0, .2f);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(1, .1f);
		grill.GetChild(2).DORotate(new Vector2(0, 180), .3f).OnComplete(delegate
		{
			//Debug.Log(newLastpos);
			grill.DOLocalMoveX(0, .5f).OnComplete(delegate {
				grill.DOScale(1.36f, .3f).SetEase(Ease.OutBounce);
			});

		});


	}

	public void OpenGrill()
	{
		grill.GetChild(2).DOLocalMoveX(defaultPosLeft.x, 0);
		grill.GetChild(2).GetChild(0).GetComponent<Image>().DOFade(1, 0);
		grill.GetChild(2).GetChild(grill.GetChild(2).childCount - 1).GetComponent<Image>().DOFade(0, 0);
		grill.GetChild(2).DORotate(new Vector2(0, 0), 0).OnComplete(delegate
		{
			grill.DOLocalMoveX(0, 0);
		});
	}

	public void ShutDownCanvasGroup(CanvasGroup offPage)
	{
		offPage.interactable = false;
		offPage.blocksRaycasts = false;
		offPage.DOFade(0, .5f);
	}

	public void CloseTutorial()
	{
		main[0].GetComponent<CanvasGroup>().interactable = false;
		main[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
		main[0].GetComponent<CanvasGroup>().DOFade(0, .5f);

	}
	public void closeObject(Transform transform) {
		transform.GetComponent<Image>().DOKill();
		transform.GetComponent<Image>().DOFade(0, .5f).SetDelay(1).OnComplete(delegate {
			transform.gameObject.SetActive(false);
		});
	}
	public void closeObjectThis(Transform transform)
	{
		transform.GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(delegate {
			transform.gameObject.SetActive(false);
		});
	}
}
