using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    public float leftclick;
    public Animator anim;
    public GameObject AttackArea;
    private bool cooltime = false; 
    public GameObject pickupObj;
    //使用可能武器
    public items none;
    public items woodensword;
    public items ironsword;

    // Update is called once per frame
    void Update()
    {
        //左クリックで攻撃
        leftclick = Input.GetAxis("Fire");
        Debug.Log(leftclick);
        //攻撃モーションの呼び出し
        if (leftclick == 0)
        {
            anim.SetBool("Attack", false);
        }
        //攻撃時の処理
        if (leftclick > 0)
        {
            //pickupスクリプトから現在の武器を取得
            var pickupComp = pickupObj.GetComponent<pickup>();
            string wepon = pickupComp.HasItem;
            Debug.Log(wepon);
            //武器によって攻撃モーションとダメージを変える
            switch (wepon)
            {
                case "none":
                    int random = Random.Range(0, 2);
                    anim.SetInteger("weapon", 0);
                    anim.SetInteger("random", random);
                    anim.SetBool("Attack", true);
                    if (cooltime == false)
                    {
                        float damage = none.attackDamage;
                        StartCoroutine(HandsAttackFrame("none",damage));
                    }
                    break;
                case "woodensword":
                    anim.SetInteger("weapon", 1);
                    anim.SetInteger("sword", 1);
                    anim.SetBool("Attack", true);
                    if (cooltime == false)
                    {
                        float damage = woodensword.attackDamage;
                        StartCoroutine(HandsAttackFrame("sword",damage));
                    }
                    break;
                case "ironsword":
                    anim.SetInteger("weapon", 1);
                    anim.SetInteger("sword", 2);
                    anim.SetBool("Attack", true);
                    if (cooltime == false)
                    {
                        float damage = ironsword.attackDamage;
                        StartCoroutine(HandsAttackFrame("sword",damage));
                    }
                    break;
            }
            
        }
    }
    //攻撃モーションのフレームに合わせ
    IEnumerator HandsAttackFrame(string type,float damage)
    {
        Debug.Log("攻撃モーション"+damage);
        AttackArea attackarea = AttackArea.GetComponent<AttackArea>();
        //武器により攻撃モーションのフレームが違うため、武器によって処理を分ける
        switch (type)
        {
            case "none":
                cooltime = true;
                yield return new WaitForSeconds(0.3f);
                yield return null;
                attackarea.Call(damage);
                yield return new WaitForSeconds(0.3f);
                cooltime = false;
                break;
            case "sword":
                cooltime = true;
                yield return new WaitForSeconds(0.8f);
                yield return null;
                attackarea.Call(damage);
                yield return new WaitForSeconds(1f);
                cooltime = false;
                break;
        }
    }
}
