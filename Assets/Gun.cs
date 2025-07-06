using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public shotmanage shotManager; 
    public GameObject bulletPrefab;
    public float shotSpeed;
    
    // 効果音用のコンポーネントとクリップ
    public AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called before the first frame update
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
        
        // デバッグ情報を表示
        Debug.Log("Gun初期化完了 - Shot Speed: " + shotSpeed);
    }

    public void Fire()
    {
        // 弾があるかチェック
        if (shotManager != null && shotManager.shotCount > 0)
        {
            Debug.Log("射撃開始 - 残弾: " + shotManager.shotCount);
            
            // 弾を生成
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            
            // 弾のタグを設定
            bullet.tag = "Shell";
            
            // BulletControllerコンポーネントを取得または追加
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController == null)
            {
                bulletController = bullet.AddComponent<BulletController>();
            }
            
            // 弾の設定
            bulletController.SetBulletSpeed(shotSpeed);
            
            // Rigidbodyの設定
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb == null)
            {
                bulletRb = bullet.AddComponent<Rigidbody>();
            }
            bulletRb.velocity = transform.forward * shotSpeed;
            
            // Colliderの設定（Triggerにする）
            Collider bulletCollider = bullet.GetComponent<Collider>();
            if (bulletCollider != null)
            {
                bulletCollider.isTrigger = true;
            }
            
            Debug.Log("弾を生成しました: " + bullet.name + " - 速度: " + shotSpeed);
            
            // 射撃カウントを更新
            shotManager.currentshot();
            
            // 射撃音を再生（SoundManagerを使用）
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayShootSound();
            }
            // ローカルの射撃音も再生（設定されている場合）
            else if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            // 弾の生存時間を設定（BulletControllerで管理されるため、ここでは削除）
            // Destroy(bullet, 3.0f); // この行を削除
        }
        else
        {
            Debug.Log("弾が不足しています - 残弾: " + (shotManager != null ? shotManager.shotCount : 0));
        }
        
    }

    // 「リロード処理を実行せよ」という命令を受け取るメソッド
    public void Reload()
    {
        if (shotManager != null)
        {
            shotManager.Reload(); // shotManagerにReloadメソッドを新しく作る
            
            // リロード音を再生
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayReloadSound();
            }
            
            Debug.Log("リロード完了");
        }
    }

}
