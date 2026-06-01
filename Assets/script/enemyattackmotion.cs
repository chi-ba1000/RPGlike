using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyattackmotion : MonoBehaviour
{
    private GameObject DamageCal;
    private GameObject playerobj;
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数
    private Animator anim;
    private bool hitcount;
    private bool cooltime = false;
    // Start is called before the first frame update
    void Start()
    {
        // このオブジェクトにアタッチされているNavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        DamageCal = GameObject.FindGameObjectWithTag("system");
        playerobj = GameObject.FindGameObjectWithTag("Player");
    }
    //zakoの攻撃モーション
    public void Zako()
    {
        if (!cooltime)
        {
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
            anim.SetTrigger("attack");
            Debug.Log("攻撃モーション");
            StartCoroutine(Cooltime(0.16f,0.25f,1f));
        }
    }

    //midbossの攻撃モーション
    public void Midboss()
    {
        Debug.Log("攻撃モーション");
        if (!cooltime)
        {
            int random = Random.Range(0, 3);
            switch(random)
            {
                case 0:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack1");
                    Debug.Log("攻撃モーション1");
                    StartCoroutine(Cooltime(0.25f, 0.166f, 4f));
                    break;
                case 1:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack2");
                    Debug.Log("攻撃モーション2");
                    StartCoroutine(Cooltime(0.83f, 0.83f, 7f));
                    break;
                case 2:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack3");
                    Debug.Log("攻撃モーション3");
                    StartCoroutine(Cooltime(0.25f, 0.66f, 4f));
                    break;
            }
        }
    }

    //bossの攻撃モーション
    public void Boss()
    {
        Debug.Log("攻撃モーション");
        if (!cooltime)
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack1");
                    Debug.Log("攻撃モーション1");
                    StartCoroutine(Cooltime(0.25f, 0.166f, 4f));
                    break;
                case 1:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack2");
                    Debug.Log("攻撃モーション2");
                    StartCoroutine(Cooltime(0.83f, 0.83f, 7f));
                    break;
                case 2:
                    anim.SetBool("idle", true);
                    anim.SetBool("walk", false);
                    anim.SetTrigger("attack3");
                    Debug.Log("攻撃モーション3");
                    StartCoroutine(Cooltime(0.25f, 0.66f, 4f));
                    break;
            }
        }
    }

    //攻撃のクールタイムとダメージ計算
    IEnumerator Cooltime(float yobidousa,float atosuki,float damage)
    {
        cooltime = true;
        Debug.Log("攻撃");
        DamageCal damagecal = DamageCal.GetComponent<DamageCal>();
        yield return new WaitForSeconds(yobidousa);
        if (hitcount)
        {
            damagecal.Damagecal(gameObject, damage, playerobj);
        }        
        yield return new WaitForSeconds(atosuki);
        cooltime = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        hitcount = true;
    }
}
