using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject player;
    public GameObject DamageCal;
    private bool triggerenter = false;
    private float damage;
    private List<GameObject> taggedObjects = new List<GameObject>(); // 特定のタグを持つオブジェクトのリスト
    public void Call(float takedamage)
    {
        triggerenter = true;
        damage = takedamage;
        Debug.Log("ダメージ"+takedamage);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("triggerenter");
        // 特定のタグを持つオブジェクトをリストに追加
        if (other.CompareTag("enemy") && triggerenter)
        {
            Debug.Log("true");
            if (!taggedObjects.Contains(other.gameObject)) // 重複を避けるため、リストに追加する前に確認
            {
                taggedObjects.Add(other.gameObject);

                // 一時的に削除したいオブジェクトを保存するリスト
                List<GameObject> toRemove = new List<GameObject>();

                // リスト内のオブジェクトを一つずつ出力
                foreach (var obj in taggedObjects)
                {
                    Debug.Log("タグを持つオブジェクト: " + obj.name);
                    DamageCal damageCal = DamageCal.GetComponent<DamageCal>();
                    damageCal.Damagecal (player, damage,obj);
                    Debug.Log("敵" + obj + "に" + damage + "のダメージ");

                    // 削除するオブジェクトを保存
                    toRemove.Add(obj);
                }

                // toRemove に保存したオブジェクトを taggedObjects から削除
                foreach (var obj in toRemove)
                {
                    taggedObjects.Remove(obj);
                }
            }
        }
        triggerenter = false;
    }
}
