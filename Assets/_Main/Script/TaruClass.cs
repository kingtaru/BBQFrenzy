using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace TaruClass {
    [Serializable]
    [ExecuteInEditMode]
    public enum StatusPage
    {
        mainPage, setting, tutorial, stage1, stage2, stage3, stage4, stage5
    }
    public class sp
    { 
    
    }
    [Serializable]
    public class NewQuestionsDetails
    {
        public QuestionsDetails[] questionsDetails;
    }
    [Serializable]
    public class QuestionsDetails
    {
        public int currentLevel;
        public int stage;
        public int level;
        public string[] correctAnswer;
        public List<string> availableButton;
        public List<string> forbidenButton;
        public List<ImagePlace> imagePlaces;
	}
    [Serializable]
    public class QuestionsDetailsBckup
    {
        public int currentLevel;
        public int stage;
        public int level;
        public List<string> correctAnswer;
        public List<string> availableButton;
        public List<string> forbidenButton;
        public List<ImagePlace> imagePlaces;
    }
    [Serializable]
    public class ImagePlace
    {

        public string names;
        public string pos;
    }
    [Serializable]
    public class ImagePlaceBckup {
        public enum name { 
        bbq,chicken, onion,chili,pepper,bacon,satay,sausage,prawn
        }
        public name names;
        public string pos;
    }
    [Serializable]
    public class ImageAssets
    {
        public string name;
        public Sprite image;
    }
    [Serializable]
    public class ImageAssetsToStore
    {
        public string name;
        public Texture2D image;
    }
    [Serializable]
    public class BaseControllers
    {
        public Sprite left;
        public Sprite right;
    }
    [Serializable]
    public class DoubleVector
    {
        public List<Vector3> doubleVectors;
    }
    [Serializable]
    public class BaseControllers45
    {
        public Sprite left;
        public Sprite right;
        public Sprite mid;
    }




}