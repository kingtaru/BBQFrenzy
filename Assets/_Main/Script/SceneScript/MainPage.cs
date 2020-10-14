using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaruClass;
using DG.Tweening;
using System.Diagnostics.Tracing;

public class MainPage : MonoBehaviour
{
    public Transform clonedParent;
    public int spawnMax;
    public List<Transform> spawns;
    public List<Transform> clonedSpawns;
    public List<Transform> posSet;
    public List<Transform> posDown;
    
    public List<int> indexing;
    [HideInInspector]
    public List<int> newIndexing;
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        preperation.DOFade(1, 0).OnComplete(delegate {
            preperation.interactable = true;
            preperation.blocksRaycasts = true;
            preperation.ignoreParentGroups = true;
        });

        ready = false;
        StartCoroutine("Cloning");
        indexing = new List<int>();
        for (int i = 0; i < posSet.Count; i++){
            indexing.Add(i);
        }
		foreach (Transform transforms in posSet)
		{
			transforms.localPosition = new Vector3(transforms.localPosition.x, (Screen.height + 200) * 1, 0);
		}
		foreach (Transform transforms in posDown)
		{
            transforms.localPosition = new Vector3(transforms.localPosition.x, (Screen.height + 200) * -1, 0);
        }
		Transform posDownParent = posDown[0].parent;
        Transform posSetParent = posSet[0].parent;
	}
    public CanvasGroup preperation;
    public IEnumerator Cloning() {
        for (int i = 0; i < spawnMax; i++) {
            foreach (Transform transforms in spawns)
            {
                var newObject = Instantiate(transforms);

                newObject.SetParent(clonedParent);
                newObject.localScale = new Vector3(1, 1, 1);
                newObject.position = transforms.position;
                clonedSpawns.Add(newObject);
                yield return newObject;
            }
        }
        Invoke("DoShuffle", .5f);
        preperation.DOFade(0, .5f).SetDelay(3f).OnComplete(delegate {
            preperation.interactable = false;
            preperation.blocksRaycasts = false;
            preperation.ignoreParentGroups = false;
        });
    }
    public void OnChangeTo() { 
    
    }

    public void DoShuffle() {
        List<int> tempIndex = new List<int>();
        foreach (int i in indexing) {
            tempIndex.Add(i);
        }
        Utility.Shuffle(tempIndex);
        if (tempIndex[tempIndex.Count - 1] != indexing[0])
        {
            indexing = new List<int>();
            foreach (int i in tempIndex)
            {
                indexing.Add(i);
            }
            float delays = Random.Range(.2f, .4f);
            Invoke("DoSpawn", delays);
        }
        else {
            DoShuffle();
        }
    }
    public int countObject=0;
    public int tempindex=0;
    public void DoSpawn() {
        int currentIndex = indexing[tempindex];
        int currentIndex2 = indexing[tempindex+1];
        //Spawn
        float falling = Random.Range(4, 6);
        clonedSpawns[countObject].DOLocalMove(posSet[currentIndex].transform.localPosition, 0);
        clonedSpawns[countObject].DOLocalMove(posDown[currentIndex].localPosition, falling ).SetEase(Ease.Linear);
        //double spawn
        clonedSpawns[countObject+1].DOLocalMove(posSet[currentIndex2].transform.localPosition, 0);
        clonedSpawns[countObject+1].DOLocalMove(posDown[currentIndex2].localPosition, falling).SetEase(Ease.Linear);


		countObject +=2;
        tempindex+=2;
        
        if (tempindex == indexing.Count -2)
        {
            tempindex = 0;
            DoShuffle();
        }
        else {
            float delays = Random.Range(.2f, .4f);
            Invoke("DoSpawn", delays) ;
        }
        if (countObject > (clonedSpawns.Count - 2))
        {
            countObject = 0;
        }
    }


}
public static class Utility
{
    public static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
};