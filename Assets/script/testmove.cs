using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmove : MonoBehaviour
{

    public Transform testobj;
    public Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transformの説明(inspectorの設定)
        //座標取得
        Vector3 position =(testobj.position);
        Debug.Log(position);
        //coliderとrigidbodyの説明(軽ーく)
        //Input.GetKeyの説明
        //wasdで動かしてみよう課題
        if (Input.GetKey(KeyCode.K))
        { 
            testobj.position = position + move;
        }
        //ローカル座標(余裕があれば).localpositionでよき？
        //rotationとかrotateもあり？
        //GetKeyDownとかUpとかInput.GetAxisとかInput.GetButtonとか  https://tech.pjin.jp/blog/2015/09/30/unity%E3%81%AE%E3%82%AD%E3%83%BC%E5%85%A5%E5%8A%9B%E3%81%BE%E3%81%A8%E3%82%81-%EF%BD%9Einput%E3%82%AF%E3%83%A9%E3%82%B9%EF%BD%9E/
        //keycode https://docs.unity3d.com/ja/2022.1/ScriptReference/KeyCode.html
        //からのmaincameraあたっち？
        //constraintはいらない？
    }
}
