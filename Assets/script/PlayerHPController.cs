using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHPController : MonoBehaviour
{
    public int MaxHP;
    public float Armor = 1f;
    public float currentHP;
    public Animator anim;
    public playerHPUI playerhpUI;
    public scenechanger sceneChanger;
    public GameObject gameoverUI;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        gameoverUI.SetActive(false);
        currentHP = MaxHP;
        playerhpUI.UpdateHPUI(currentHP, MaxHP);
    }

    //ダメージを受ける処理
    public void Damaged(float Damage)
    {
        Debug.Log(gameObject.name + "に" + Damage);
        StartCoroutine(Hit());
        currentHP = currentHP - Damage * Armor;
        //ダメージをHPUIに送り更新する
        playerhpUI.UpdateHPUI(currentHP, MaxHP);
        if (currentHP <= 0)
        {
            //死亡モーションを呼び出す
            Debug.Log("GameOver");
            gameoverUI.SetActive(true);
            StartCoroutine(GameOver());
        }
    }

    //回復する処理
    public void Heal(float Heal)
    {
        currentHP = currentHP + Heal;
        if (currentHP > MaxHP)
        {
            currentHP = MaxHP;
        }
        //回復したHPをHPUIに送り更新する
        playerhpUI.UpdateHPUI(currentHP, MaxHP);
    }

    //ダメージを受けたときのアニメーション
    IEnumerator Hit()
    {
        anim.SetBool("Hit", true);
        yield return null;
        anim.SetBool("Hit", false);
    }

    //ゲームオーバーの処理
    IEnumerator GameOver()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        //タイトルに戻る
        sceneChanger.Gotitle();
    }
}
