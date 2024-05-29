using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataParam : MonoBehaviour
{
    static PlayerDataParam myInstance;      //シングルトン用インスタンス

    public PlayerData m_PData;      // プレイヤーデータの取得用変数

    public static PlayerData data => myInstance.m_PData;

    private void Awake()
    {
        if (myInstance == null)
        {
            myInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public static PlayerDataParam GetInstance()
    //{
    //    return myInstance = myInstance ?? CreateInstance();
    //}

    //static PlayerDataParam CreateInstance()
    //{
    //    //var prefab = Resources.LoadAll<PlayerDataParam>("")[0];
    //    /*static PlayerDataParam instance = thisGameObject.Instantiate<PlayerDataParam>(prefab)*/;
    //    GameObject.DontDestroyOnLoad(instance);
    //    return this;
    //}


    //public static PlayerData PData
    //{
    //    get { return m_PData; }  //取得用
    //}

}
