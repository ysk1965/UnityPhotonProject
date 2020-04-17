using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    // public
    public PhotonView PhotonView;
    // protected
    protected void OnEnable(){
        StartCoroutine(CountTime());
    }

    protected void Disable(){
        fallScale = 5.0f;
        GamePlayManager.instance.SetGameview(false);
        StopCoroutine(CountTime());
    }

    IEnumerator CountTime(){
        while(true){
            InstantiateEnemy();
            yield return new WaitForSeconds(fallScale);
        }
    }

    //private
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject InitPosition;

    string randomNumber = "";
    float randomXpos = 0.0f;

    float fallScale = 5.0f;
    void InstantiateEnemy(){
        if(PhotonView.IsMine){
            //PhotonView.RPC("randomNumberRPC", RpcTarget.All, randomNumber);
            //PhotonView.RPC("randomXposRPC", RpcTarget.All, randomXpos);
            GameObject enemy = PhotonNetwork.Instantiate("TextEnemy", Vector3.zero, Quaternion.identity);

            Debug.LogError("randomNumber : " + randomNumber);
            Debug.LogError("randomXpos : " + randomXpos);
            enemy.name = randomNumber;
            if(enemy.GetComponent<Text>() != null){
                enemy.GetComponent<Text>().text = randomNumber;
            }
            enemy.transform.parent = InitPosition.transform;
            enemy.transform.localPosition = new Vector3(randomXpos, 0, 1);
        }
    }

    [PunRPC]
    string randomNumberRPC(){
        return Random.Range(1, 1000).ToString();
    }

    [PunRPC]
    float randomXposRPC(){
        return Random.Range(-400, 400);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        randomNumber = Random.Range(1, 1000).ToString();
        randomXpos = Random.Range(-400, 400);
        if(stream.IsWriting) {
            stream.SendNext(randomNumber); 
            stream.SendNext(randomXpos); 
        }// sendNext는 어떤 타입이던 담을 수 있음.
        else {
            randomNumber = (string)stream.ReceiveNext();
            randomXpos = (float)stream.ReceiveNext();
        }// 받을 때는 강제 형변환 하여 대입
    }
}
