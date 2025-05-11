using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // 生成するプレハブ
    public int maxAmount = 10; // 生成する最大数
    public float coolTime = 2.0f; // クールタイム（秒）
    public float repopTime = 30f;
    public GameObject wakiba;

    private int currentAmount = 0; // 現在生成されている数
    private bool count = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
            Vector3 randomPosition = new Vector3(
                Random.Range(90f, 103f),
                32f,
                Random.Range(95f, 128f)
            );
            Debug.Log("わきました");

            Instantiate(prefab, randomPosition, Quaternion.identity);
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
