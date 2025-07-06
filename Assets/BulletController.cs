using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("弾の設定")]
    public float bulletSpeed = 20f;          // 弾の速度
    public float bulletLifetime = 3f;        // 弾の生存時間
    public int bulletDamage = 10;            // 弾のダメージ
    
    [Header("効果音設定")]
    public AudioSource audioSource;
    public AudioClip bulletFlySound;         // 弾の飛行音
    public AudioClip bulletHitSound;         // 弾の命中音
    public AudioClip bulletDestroySound;     // 弾の破壊音
    
    private Rigidbody rb;
    private bool hasHit = false;             // 既に何かに当たったかどうか
    
    void Start()
    {
        // Rigidbodyの取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // AudioSourceコンポーネントの設定
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // 弾の飛行音を再生
        if (audioSource != null && bulletFlySound != null)
        {
            audioSource.clip = bulletFlySound;
            audioSource.loop = true;  // 飛行中はループ再生
            audioSource.Play();
        }
        
        // 一定時間後に弾を破壊
        StartCoroutine(DestroyBulletAfterTime());
    }
    
    void Update()
    {
        // 弾が何かに当たったら飛行音を停止
        if (hasHit && audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 既に当たっている場合は処理しない
        if (hasHit) return;
        
        // DestroyObjectコンポーネントを持つオブジェクトに当たった場合
        if (other.GetComponent<DestroyObject>() != null)
        {
            hasHit = true;
            
            // 飛行音を停止
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
            // 命中音を再生
            if (audioSource != null && bulletHitSound != null)
            {
                audioSource.PlayOneShot(bulletHitSound);
            }
            
            // SoundManagerの命中音も再生
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayHitSound();
            }
            
            // 少し遅延してから弾を破壊（効果音を聞くため）
            StartCoroutine(DestroyBulletWithDelay(0.1f));
        }
        // その他の障害物に当たった場合
        else if (other.CompareTag("Obstacle") || other.CompareTag("Wall"))
        {
            hasHit = true;
            
            // 飛行音を停止
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
            // 破壊音を再生
            if (audioSource != null && bulletDestroySound != null)
            {
                audioSource.PlayOneShot(bulletDestroySound);
            }
            
            // 少し遅延してから弾を破壊
            StartCoroutine(DestroyBulletWithDelay(0.1f));
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // 既に当たっている場合は処理しない
        if (hasHit) return;
        
        // 何かに衝突した場合
        hasHit = true;
        
        // 飛行音を停止
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        // 破壊音を再生
        if (audioSource != null && bulletDestroySound != null)
        {
            audioSource.PlayOneShot(bulletDestroySound);
        }
        
        // 少し遅延してから弾を破壊
        StartCoroutine(DestroyBulletWithDelay(0.1f));
    }
    
    // 一定時間後に弾を破壊
    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        
        if (!hasHit)
        {
            // 飛行音を停止
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
            Destroy(gameObject);
        }
    }
    
    // 遅延してから弾を破壊
    IEnumerator DestroyBulletWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    // 弾の速度を設定
    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
        if (rb != null)
        {
            rb.velocity = transform.forward * bulletSpeed;
        }
    }
    
    // 弾のダメージを設定
    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }
} 