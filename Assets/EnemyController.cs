using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // public 
    public float moveSpeed = 4.0f;
    
    // protected
    protected void Update(){
        float yMove = moveSpeed * Time.deltaTime;
        gameObject.transform.Translate(new Vector3(0,-yMove,0), 0);

        if(gameObject.transform.localPosition.y < -750){
            GamePlayManager.instance.RemoveEnemy(gameObject.name);
            Destroy(gameObject);
        }
    }

    protected void Awake()
    {
        gameObject.transform.parent = GameObject.Find("InitPosition").transform;
        GamePlayManager.instance.AddEnemy(gameObject.name);
    }
    
    // private
}
