using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // シングルトンパターンでサウンドマネージャーを管理
    public static SoundManager Instance { get; private set; }
    
    [Header("効果音クリップ")]
    public AudioClip damageSound;        // ダメージ音
    public AudioClip deathSound;         // 死亡音
    public AudioClip shootSound;         // 射撃音
    public AudioClip reloadSound;        // リロード音
    public AudioClip hitSound;           // 命中音
    
    [Header("効果音ごとの音量")]
    [Range(0f, 1f)] public float damageVolume = 0.7f;
    [Range(0f, 1f)] public float deathVolume = 0.7f;
    [Range(0f, 1f)] public float shootVolume = 0.7f;
    [Range(0f, 1f)] public float reloadVolume = 0.7f;
    [Range(0f, 1f)] public float hitVolume = 0.7f;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でもオブジェクトを保持
            
            // AudioSourceコンポーネントの設定
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            // AudioSourceの初期設定
            audioSource.volume = damageVolume;
            audioSource.playOnAwake = false;
        }
        else
        {
            // 既にサウンドマネージャーが存在する場合は破棄
            Destroy(gameObject);
        }
    }
    
    // ダメージ音を再生
    public void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound, damageVolume);
        }
    }
    
    // 死亡音を再生
    public void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound, deathVolume);
        }
    }
    
    // 射撃音を再生
    public void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound, shootVolume);
        }
    }
    
    // リロード音を再生
    public void PlayReloadSound()
    {
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound, reloadVolume);
        }
    }
    
    // 命中音を再生
    public void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound, hitVolume);
        }
    }
    
    // カスタム効果音を再生
    public void PlayCustomSound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip, damageVolume);
        }
    }
    
    // 効果音の音量を設定
    public void SetSFXVolume(float newVolume)
    {
        damageVolume = Mathf.Clamp01(newVolume); // 0.0f から 1.0f の範囲に制限
    }
    
    // 効果音の音量を取得
    public float GetSFXVolume()
    {
        return damageVolume;
    }
    
    // 効果音を一時停止
    public void PauseAllSFX()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }
    
    // 効果音を再開
    public void ResumeAllSFX()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
} 