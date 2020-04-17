using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class InRoomController : MonoBehaviour
{
    // public
    // protected
    protected void OnEnable(){
        if(NetworkManager.instance.isInRoom()){
           UpdateRoomStatus();
        }
    }

    protected void Update(){
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers){
            GamePlayManager.instance.SetGameview(true);
        }
    }
    // private
    [SerializeField] private Text statusText;

    void UpdateRoomStatus(){
        statusText.text = "";
        statusText.text += "Room name : " + PhotonNetwork.CurrentRoom.Name + "\n";
        statusText.text += "Room player count : " + PhotonNetwork.CurrentRoom.PlayerCount + "\n";
        statusText.text += "Room Max Number : " + PhotonNetwork.CurrentRoom.MaxPlayers + "\n";
        string playerName = "";
        for(int i = 0; i< PhotonNetwork.PlayerList.Length;i++){
            playerName += "Player" + i + "_" + PhotonNetwork.PlayerList[i].NickName + "\n";
        }
        
        statusText.text += playerName;
    }
}
