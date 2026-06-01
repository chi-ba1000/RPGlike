using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerHPUI : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    // プレイヤーのHPを更新する関数
    public void UpdateHPUI(float currentHP, float maxHP)
    {
        Debug.Log(currentHP);
        Debug.Log(maxHP);
        hpSlider.value = currentHP / maxHP;
        hpText.text = $"{currentHP} / {maxHP}";
    }
}
