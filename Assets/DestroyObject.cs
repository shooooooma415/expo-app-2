using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public int damage;          //当たった部位毎のダメージ量
    private HP hp;  
    
    // 効果音用のコンポーネントとクリップ
    public AudioSource audioSource;
    public AudioClip hitSound;          // 命中音
    public AudioClip destroySound;      // 破壊音
    public AudioClip impactSound;       // 衝撃音

    // Start is called before the first frame update
    void Start()
    {
        // 親オブジェクトからHPコンポーネントを取得
        hp = GetComponentInParent<HP>();
        if (hp == null)
        {
            Debug.LogError("親オブジェクトにHPコンポーネントが見つかりません！");
        }
        
        // AudioSourceコンポーネントが存在しない場合は追加
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // デバッグ情報を表示
        Debug.Log("DestroyObject初期化完了 - Damage: " + damage);
    }

    void OnTriggerEnter(Collider other){
 
        //ぶつかったオブジェクトのTagにShellという名前が書いてあったならば（条件）.
        if (other.CompareTag("Shell")){
 
            Debug.Log("銃弾が当たりました: " + other.gameObject.name + " - ダメージ: " + damage); // デバッグログ
            
            // HPコンポーネントが存在するかチェック
            if (hp == null)
            {
                Debug.LogError("HPコンポーネントがnullです！ダメージ処理をスキップします。");
                return;
            }
            
            // 命中音を再生（SoundManagerを使用）
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayHitSound();
            }
            // ローカルの命中音も再生（設定されている場合）
            else if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            
            //HPクラスのDamage関数を呼び出す
            hp.Damage(damage);
            Debug.Log("ダメージ処理完了: " + damage + " ダメージを与えました");
            
            // 衝撃音を再生（設定されている場合）
            if (audioSource != null && impactSound != null)
            {
                audioSource.PlayOneShot(impactSound);
            }
 
            //ぶつかってきたオブジェクトを破壊する.
            Destroy(other.gameObject);
            Debug.Log("弾を破壊しました: " + other.gameObject.name);
            
            // 破壊音を再生（設定されている場合）
            if (audioSource != null && destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }
        }
    }
}
