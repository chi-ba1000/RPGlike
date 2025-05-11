using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public int MaxHP;
    public float Armor = 1f;
    public float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float Damage)
    {
        Debug.Log(gameObject.name + "‚É" +  Damage);
        currentHP = currentHP - Damage * Armor;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
