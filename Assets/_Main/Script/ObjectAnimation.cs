using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TaruClass;
using TMPro;

public class ObjectAnimation : MonoBehaviour
{
	#region Enum Declaration
	public enum Animations { 
    None,RotateLeftRight, ZoomInZoomOut,RotateLoop,UpDown ,Draw,DedicatedMan,MoveToPos,Blinking,Loading, DrawSecond
    }
	#endregion Enum Declaration

	#region Variable Declaration
	public Animations animations;
    Vector3 defaultRotPos;
    Vector3 defaultScale;
    public float minValue;
    public Vector3 destinationPos;
    public bool loops;
    [Header("MoveToPos")]
    public List<DoubleVector> moveToPos;
    [Header("DedicatedMan")]
    public List<DoubleVector> destinationPosRotDedicatedMan;
    #endregion Variable DedicatedMan
    void Start()
    {
        defaultRotPos =  new Vector3(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z);
        defaultScale = new Vector3(transform.localScale.x, transform.localScale.y,transform.localScale.z);

        if (animations != Animations.Draw && animations != Animations.DrawSecond)
        {
            doAnimation();
        }
        else {
            transform.GetComponent<Image>().DOFillAmount(0, 0);
        }
    }
    public int tempIndex;
    int tempIndex1;
	#region Animate Object
	void doAnimation() {
        if (animations == Animations.RotateLeftRight)
        {
            float currentPos = transform.localEulerAngles.z;
            Vector3 newDestination;
            if (currentPos < 270)
            {
                newDestination = new Vector3(transform.localRotation.x, transform.localRotation.y, (-1 * minValue));
            }
            else
            {
                newDestination = new Vector3(transform.localRotation.x, transform.localRotation.y, (1 * minValue));
            }
            transform.DOLocalRotate(newDestination, 1).SetEase(Ease.Linear).OnComplete(delegate
            {
                doAnimation();
            });
        }
        else if (animations == Animations.ZoomInZoomOut)
        {
            float currentPos = transform.localScale.z;
            Vector3 newDestination;
            if (currentPos == defaultScale.z)
            {
                newDestination = new Vector3(minValue, minValue, minValue);
            }
            else
            {
                newDestination = defaultScale;
            }
            float delay = Random.Range(0, 2);
            transform.DOScale(newDestination, .4f).SetEase(Ease.OutBounce).SetDelay(delay).OnComplete(delegate
            {
                doAnimation();
            });

        }
        else if (animations == Animations.RotateLoop)
        {
            transform.DORotate(Vector3.zero, 4, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        else if (animations == Animations.UpDown)
        {
            transform.DOLocalMove(destinationPos, .5f).SetLoops(-1, LoopType.Yoyo);

        }
        else if (animations == Animations.Draw)
        {
            transform.GetComponent<Image>().DOFillAmount(1, .5f);
        }
        else if (animations == Animations.DedicatedMan)
        {
            transform.DOLocalMove(destinationPosRotDedicatedMan[tempIndex].doubleVectors[0], minValue).SetEase(Ease.Linear);
            transform.DOLocalRotate(destinationPosRotDedicatedMan[tempIndex].doubleVectors[1], minValue).SetEase(Ease.Linear).OnComplete(delegate
            {
                tempIndex++;
                if (tempIndex > 1)
                {
                    tempIndex = 0;
                }
                doAnimation();
            });
        }
        else if (animations == Animations.MoveToPos)
        {
            transform.DOLocalMove(moveToPos[tempIndex].doubleVectors[0], 1).SetEase(Ease.Linear).SetDelay(.5f); ;
            transform.DOLocalRotate(moveToPos[tempIndex].doubleVectors[1], 1).SetEase(Ease.Linear).SetDelay(.5f).OnComplete(delegate
            {
                tempIndex++;
                if (tempIndex > 1)
                {
                    tempIndex = 0;

                }
                doAnimation();
            });

        }
        else if (animations == Animations.Blinking)
        {
            transform.DOScaleY(minValue, .2f).OnComplete(delegate
            {
                transform.DOScaleY(1, .5f).OnComplete(delegate
                {
                    Invoke("doAnimation", Random.Range(1, 2));
                });
            });

        }
        else if (animations == Animations.Loading) {
			switch (tempIndex1)
			{
                case 0:
                    transform.GetComponent<TextMeshProUGUI>().text = "Loading";
                    break;
                    
                case 1:
                    transform.GetComponent<TextMeshProUGUI>().text = "Loading.";
                    break;
                    
                case 2:
                    transform.GetComponent<TextMeshProUGUI>().text = "Loading..";
                    break;
                    
                case 3:
                    transform.GetComponent<TextMeshProUGUI>().text = "Loading...";
                    
                    break;
			}
            tempIndex1++;
            if(tempIndex1==4)tempIndex1 = 0;
            Invoke("doAnimation", 1);
        }
        else if (animations == Animations.DrawSecond)
        {
            //transform.GetComponent<Image>().DOFillAmount(1, .5f).SetLoops(-1,LoopType.Yoyo);
            float setdelay = Random.Range(2, 4);
            transform.GetComponent<Image>().DOFillAmount(1, .5f).SetDelay(.5f).OnComplete(delegate {
                transform.GetComponent<Image>().DOFillAmount(0, .5f).SetDelay(setdelay).OnComplete(delegate {
                    doAnimation();
                });
            });
        }
    }
    #endregion Animate Object
    public void GetDrawTrigger(float delay) {
        Invoke("doAnimation",delay);
    }
}
