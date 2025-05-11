using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCal : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damagecal(GameObject Attacker ,float Wepon , GameObject Deffender)
    {
        if (Attacker == player)
        {
            float Damage = Wepon;
            EnemyHPController enemyHPController = Deffender.GetComponent<EnemyHPController>();
            enemyHPController.TakeDamage(Damage); // É_ÉÅÅ[ÉWÇó^Ç¶ÇÈ
        }
        else
        {
            float Damage = Wepon;
            PlayerHPController playerHPController = Deffender.GetComponent<PlayerHPController>();
            playerHPController.Damaged(Damage);
        }

    }
}
