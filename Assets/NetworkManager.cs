    using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance = null;
    // public
    public Text StatusText;
    public InputField IDInput, RoomNuberInput;

    // Connect함수는 Callback으로 작동
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public void DisConnect() => PhotonNetwork.Disconnect();
    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomNuberInput.text == "" ? (Random.Range(0, 1000)).ToString() : RoomNuberInput.text, new RoomOptions{MaxPlayers = 2});
    public void JoinRoom() => PhotonNetwork.JoinRoom(RoomNuberInput.text);

    public override void OnConnectedToMaster(){
        Debug.LogError("서버 접속 완료");
        if(IDInput.text == ""){
            DisConnect();
            return;
        }
        PhotonNetwork.LocalPlayer.NickName = IDInput.text;

        JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause){
        Debug.LogError("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = IDInput.text;

    }
    public override void OnJoinedLobby(){
        Debug.LogError("로비 접속 완료");
    }

    public override void OnJoinedRoom(){
        Debug.LogError("방 접속 완료");
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.LogError("방 랜덤 참가 실패");
        // OnCreateRoomFailed : 방 만들기 실패 (이름이 겹치는 경우)
        // OnJoinRoomFailed : 방 참가 실패
        // 방을 참가하거나 만들 때는 ConnetedMaster or LobbyJoinned 되어있을 때
    }

    public bool isInRoom(){
        return PhotonNetwork.InRoom;
    }

    // protected
    protected void Update() => StatusText.text = PhotonNetwork.NetworkClientState.ToString();

    // protected
    protected void Awake(){
        if(instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    // private
    [ContextMenu("정보")]
    void Info(){
        if(PhotonNetwork.InRoom){
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원 수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대 인원 수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerName = "";
            for(int i = 0; i< PhotonNetwork.PlayerList.Length;i++){
                playerName += "Player" + i + "_" + PhotonNetwork.PlayerList[i].NickName + ", ";
            }
        } else{
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결했는지? : " + PhotonNetwork.IsConnected);
        }
    }
}
