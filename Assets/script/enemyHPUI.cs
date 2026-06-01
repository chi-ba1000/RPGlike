using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemyHPUI : MonoBehaviour
{
    public Slider enemyhpSlider;
    private Camera mainCamera;
    void Awake()
    {
        //‰Šú‰»
        mainCamera = Camera.main;
        enemyhpSlider = GetComponentInChildren<Slider>();
    }
    void Update()
    {//í‚ÉƒJƒƒ‰‚Ì•ûŒü‚ğŒü‚­‚æ‚¤‚É‚·‚é
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
    public void EnemyUpdateHPUI(float currentHP, float maxHP)
    {//“G‚ÌHP‚ğXV‚·‚éŠÖ”
        enemyhpSlider.value = currentHP / maxHP;
    }
    // Start is called before the first frame update
}
