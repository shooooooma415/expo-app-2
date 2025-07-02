using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private float currentHealth = 100;   // 現在のHP
    public Slider healthBar;  
    public Slider enemySlider;       // HPバーのスライダー

    //ダメージを受け取ってHPを減らす関数
    public void Damage(int damage)
    {
        // デバッグログで現在のHPとダメージを表示
        Debug.Log("ダメージを受けました: " + damage);
        Debug.Log("現在のHP : " + currentHealth);

        //受け取ったダメージ分HPを減らす
        currentHealth -= damage;

        // HPバーを更新
        healthBar.value = currentHealth;
        // HPバーを更新
        enemySlider.value = currentHealth;

        // HPが0以下になったらプレイヤーが死ぬ処理
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
