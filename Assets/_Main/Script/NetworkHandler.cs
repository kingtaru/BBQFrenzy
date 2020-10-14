using System.Collections;
using System.Collections.Generic;
using TaruClass;
using UnityEngine;

public class NetworkHandler : MonoBehaviour
{
    //string[] lines = System.IO.File.ReadAllLines(@"SomeFile.txt");
    // Start is called before the first frame update
    public List<QuestionsDetails> listOfQuestionS1;
    public List<QuestionsDetails> listOfQuestionS2;
    public List<QuestionsDetails> listOfQuestionS3;
    public List<QuestionsDetails> listOfQuestionS4;
    public List<QuestionsDetails> listOfQuestionS5;
    public List<NewQuestionsDetails> listOfQuestionTemp;
    void Start()
    {
        StartCoroutine("GettingData");
    }
    public string GetTextFromTxt(string path, string data)
    {
        string lines;
        lines = System.IO.File.ReadAllText(path + data);
        return lines;
    }

    public IEnumerator GettingData() {
        //string lines = System.IO.File.ReadAllText("DataInfo.txt");
        string newJson = GetTextFromTxt("Assets/", "DataInfo.txt");
        Debug.Log(newJson);
        NewQuestionsDetails questionsDetails = JsonUtility.FromJson<NewQuestionsDetails>(newJson);
        Debug.Log(questionsDetails);
        listOfQuestionTemp.Add(questionsDetails);
        yield return listOfQuestionTemp;
        foreach (QuestionsDetails newQuestionsDetails in listOfQuestionTemp[0].questionsDetails) {
            switch (newQuestionsDetails.stage) {
                case 1:
                    listOfQuestionS1.Add(newQuestionsDetails);
                    break;
                case 2:
                    listOfQuestionS2.Add(newQuestionsDetails);
                    break;
                case 3:
                    listOfQuestionS3.Add(newQuestionsDetails);
                    break;
                case 4:
                    listOfQuestionS4.Add(newQuestionsDetails);
                    break;
                case 5:
                    listOfQuestionS5.Add(newQuestionsDetails);
                    break;

            }
        }
    }

}
