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
        //transform�̐���(inspector�̐ݒ�)
        //���W�擾
        Vector3 position =(testobj.position);
        Debug.Log(position);
        //colider��rigidbody�̐���(�y�[��)
        //Input.GetKey�̐���
        //wasd�œ������Ă݂悤�ۑ�
        if (Input.GetKey(KeyCode.K))
        { 
            testobj.position = position + move;
        }
        //���[�J�����W(�]�T�������).localposition�ł悫�H
        //rotation�Ƃ�rotate������H
        //GetKeyDown�Ƃ�Up�Ƃ�Input.GetAxis�Ƃ�Input.GetButton�Ƃ�  https://tech.pjin.jp/blog/2015/09/30/unity%E3%81%AE%E3%82%AD%E3%83%BC%E5%85%A5%E5%8A%9B%E3%81%BE%E3%81%A8%E3%82%81-%EF%BD%9Einput%E3%82%AF%E3%83%A9%E3%82%B9%EF%BD%9E/
        //keycode https://docs.unity3d.com/ja/2022.1/ScriptReference/KeyCode.html
        //�����maincamera���������H
        //constraint�͂���Ȃ��H
    }
}
