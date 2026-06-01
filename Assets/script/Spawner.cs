using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // 生成するプレハブ
    public int maxAmount = 10; // 生成する最大数
    public float coolTime = 2.0f; // クールタイム（秒）
    public float repopTime = 30f;

    private int currentAmount = 0; // 現在生成されている数
    private bool count = false;
    private Collider spawnAreaCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //プレイヤーがトリガー内にいるとき、クールタイムが終了していて、現在の生成数が最大数未満の場合に生成を開始
            Debug.Log(count);
            StartCoroutine(SpawnPrefab());
        }
    }

    private IEnumerator SpawnPrefab()
    {
        Debug.Log(currentAmount + "vs" + maxAmount);
        if (count == false && currentAmount < maxAmount)
        {
            count = true;
            //生成エリアのコライダーを取得
            BoxCollider box = spawnAreaCollider as BoxCollider;
            Vector3 localRandom = new Vector3(
                    Random.Range(-box.size.x * 0.5f, box.size.x * 0.5f),
                    Random.Range(-box.size.y * 0.5f, box.size.y * 0.5f),
                    Random.Range(-box.size.z * 0.5f, box.size.z * 0.5f)
                );
            Debug.Log("わきました");
            //プレハブを生成
            Instantiate(prefab, localRandom, Quaternion.identity);
            currentAmount++;

            yield return new WaitForSeconds(coolTime); // クールタイムを待機
            count = false;
        }
        else
        {
            yield return new WaitForSeconds(repopTime);
            currentAmount = 0;
        }
    }
}
