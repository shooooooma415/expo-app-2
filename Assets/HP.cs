using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private float currentHealth = 100;   // 現在のHP
    public Slider healthBar;  
    public Slider enemySlider;       // HPバーのスライダー
    public GameObject clearUI;
    
    // ダメージ音用のコンポーネントとクリップ
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip deathSound;     // 死亡時の音

    void Start()
    {
        // AudioSourceコンポーネントが存在しない場合は追加
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // 初期HPを設定
        if (healthBar != null)
        {
            healthBar.maxValue = 100;
            healthBar.value = currentHealth;
        }
        if (enemySlider != null)
        {
            enemySlider.maxValue = 100;
            enemySlider.value = currentHealth;
        }
        
        Debug.Log("HP初期化完了 - 初期HP: " + currentHealth);
    }

    //ダメージを受け取ってHPを減らす関数
    public void Damage(int damage)
    {
        // デバッグログで現在のHPとダメージを表示
        Debug.Log("ダメージを受けました: " + damage + " - 現在のHP: " + currentHealth);

        //受け取ったダメージ分HPを減らす
        currentHealth -= damage;
        
        // HPが負の値にならないように制限
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("ダメージ処理後 - 新しいHP: " + currentHealth);

        // HPバーを更新
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
            Debug.Log("HPバー更新: " + healthBar.value);
        }
        else
        {
            Debug.LogWarning("healthBarが設定されていません！");
        }
        
        // 敵HPバーを更新
        if (enemySlider != null)
        {
            enemySlider.value = currentHealth;
            Debug.Log("敵HPバー更新: " + enemySlider.value);
        }
        else
        {
            Debug.LogWarning("enemySliderが設定されていません！");
        }

        // ダメージ音を再生（SoundManagerを使用）
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayDamageSound();
            Debug.Log("ダメージ音を再生しました");
        }
        else
        {
            Debug.LogWarning("SoundManagerが見つかりません！");
        }

        // HPが0以下になったらプレイヤーが死ぬ処理
        if (currentHealth <= 0)
        {
            Debug.Log("HPが0になりました - 死亡処理を実行");
            
            // 死亡音を再生（SoundManagerを使用）
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayDeathSound();
                Debug.Log("死亡音を再生しました");
            }

            if (clearUI != null)
            {
                clearUI.SetActive(true);
                Debug.Log("クリアUIを表示しました");
            }
            else
            {
                Debug.LogWarning("clearUIが設定されていません！");
            }
            
            Debug.Log("オブジェクトを破壊します: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
