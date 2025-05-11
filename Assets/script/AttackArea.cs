using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject targetedobj;
    public GameObject player;
    public GameObject DamageCal;
    private List<GameObject> taggedObjects = new List<GameObject>(); // ����̃^�O�����I�u�W�F�N�g�̃��X�g

    private void OnTriggerEnter(Collider other)
    {
        // ����̃^�O�����I�u�W�F�N�g�����X�g�ɒǉ�
        if (other.CompareTag("enemy"))
        {
            if (!taggedObjects.Contains(other.gameObject)) // �d��������邽�߁A���X�g�ɒǉ�����O�Ɋm�F
            {
                taggedObjects.Add(other.gameObject);

                // �ꎞ�I�ɍ폜�������I�u�W�F�N�g��ۑ����郊�X�g
                List<GameObject> toRemove = new List<GameObject>();

                // ���X�g���̃I�u�W�F�N�g������o��
                foreach (var obj in taggedObjects)
                {
                    Debug.Log("�^�O�����I�u�W�F�N�g: " + obj.name);
                    DamageCal damageCal = DamageCal.GetComponent<DamageCal>();
                    damageCal.Damagecal (player, 1f,obj);

                    // �폜����I�u�W�F�N�g��ۑ�
                    toRemove.Add(obj);
                }

                // toRemove �ɕۑ������I�u�W�F�N�g�� taggedObjects ����폜
                foreach (var obj in toRemove)
                {
                    taggedObjects.Remove(obj);
                }
            }
        }
    }
}
