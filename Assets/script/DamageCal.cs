using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCal : MonoBehaviour
{
    public GameObject player;

    // ダメージ計算システム
    public void Damagecal(GameObject Attacker ,float Wepon , GameObject Deffender)
    {
        //攻撃者がプレイヤーの場合、敵にダメージを与える
        if (Attacker == player)
        {
            float Damage = Wepon;
            EnemyController enemycontroller = Deffender.GetComponent<EnemyController>();
            //EnemyHPController enemyHPController = Deffender.GetComponent<EnemyHPController>();
            enemycontroller.TakeDamage(Damage); // ダメージを与える
            Debug.Log("敵" + Deffender + "に" + Damage + "のダメージ");
        }
        //攻撃者がプレイヤー以外の場合、プレイヤーにダメージを与える
        else
        {
            float Damage = Wepon;
            PlayerHPController playerHPController = Deffender.GetComponent<PlayerHPController>();
            playerHPController.Damaged(Damage);
            Debug.Log("プレイヤーに" + Damage + "のダメージ");
        }
    }
}
