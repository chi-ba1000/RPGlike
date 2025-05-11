using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    public int MaxHP;
    public float Armor = 1f;
    public float currentHP;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damaged(float Damage)
    {
        Debug.Log(gameObject.name + "‚É" + Damage);
        StartCoroutine(Hit());
        currentHP = currentHP - Damage * Armor;
        if (currentHP <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    IEnumerator Hit()
    {
        anim.SetBool("Hit", true);
        yield return null;
        anim.SetBool("Hit", false);
    }
}
