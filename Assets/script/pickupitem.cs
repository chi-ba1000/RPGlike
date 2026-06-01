using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    public pickup.Item item;
    public GameObject messageset;
    public TextMeshProUGUI maintext;
    public GameObject pickup;
    private bool ispickable = false;
    // itemリストに加え，ゲームオブジェクトを破壊
    void Pickup()
    {
        maintext.text = item + "をゲットした";
        var currentitem = item;
        pickup pickup = GetComponent<pickup>();
        Destroy(gameObject);
    }

    //Fキーを押したときPickup関数を発火
    IEnumerator pickitem()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        if (ispickable)
        {
            Pickup();
        }

    }

    //アイテムのインタラクトメッセージ
    private void OnTriggerStay(Collider other)
    {
        ispickable = true;
        if (ispickable && other.CompareTag("Player"))
        {
            Debug.Log("pickup");
            maintext.text = "Fでひろう";
            StartCoroutine(pickitem());
        }
    }

    //メッセージを閉じる
    private void OnTriggerExit(Collider other)
    {
        ispickable = false;
        if (other.CompareTag("Player"))
        {
            messageset.SetActive(false);
            maintext.text = "";
        }
    }
}
