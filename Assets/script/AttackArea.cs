using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject targetedobj;
    public GameObject player;
    public GameObject DamageCal;
    private List<GameObject> taggedObjects = new List<GameObject>(); // 特定のタグを持つオブジェクトのリスト

    private void OnTriggerEnter(Collider other)
    {
        // 特定のタグを持つオブジェクトをリストに追加
        if (other.CompareTag("enemy"))
        {
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
                    damageCal.Damagecal (player, 1f,obj);

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
    }
}
