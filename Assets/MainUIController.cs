using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    // protected
    protected void Update(){
        if(NetworkManager.instance.StatusText.text == "Joined"){
            inroomUI.gameObject.SetActive(true);
            statusUI.gameObject.SetActive(false);
        } else{
            inroomUI.gameObject.SetActive(false);
            statusUI.gameObject.SetActive(true);
        }
    }


    [SerializeField] private StatusController statusUI;
    [SerializeField] private InRoomController inroomUI;
}
