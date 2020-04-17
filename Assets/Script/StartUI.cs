using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    // Public
    public void OnClickStart(){
        // MoveScene 작업 개발
        TestProject.SceneManager.MoveScene("ServerScene");
    }

    protected void Awake(){
        // SetActive
    }

    protected void Start(){
        // application version setting
        // show loading popup
        // guideText Update
    }

    // Protected


    // Private
    [SerializeField] private Text guideText;
    [SerializeField] private GameObject startButtonObj;

}
