using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviourPunCallbacks
{
    //public
    public static GamePlayManager instance = null;

    public void SetGameview(bool on = true){
        if(on == true){
            GameView.SetActive(true);

        }else{
            GameView.SetActive(false);
        }
    }

    public void CheckAnswer(){
        for(int i = 0; i<container.Count; ++i){
            if(container[i] == input.text){
                RemoveEnemy(input.text);
                count++;
                Destroy(GameObject.Find(input.text));
                score.text = count.ToString();
                input.text = "";
            }
        }
    }

    //protected
    protected void Update(){
        // 게임 종료 조건 (임시로 5일 때 종료)
        if(count == 5){
            GamePlayManager.instance.SetGameview(false);
        }
    }
    public void AddEnemy(string name){
        container.Add(name);
    }

    public void RemoveEnemy(string name){
        container.Remove(name);
    }

    // protected
    protected void Awake(){
        if(instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    int count = 0;
    // private
    [SerializeField] List<string> container;
    [SerializeField] GameObject GameView;
    [SerializeField] Text input;
    [SerializeField] Text score;
}
