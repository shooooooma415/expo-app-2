using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [Header("BGM設定")]
    public AudioClip gameBGM;           // ゲームのBGM
    public float bgmVolume = 0.3f;      // BGMの音量（効果音より少し小さく）
    
    [Header("効果音設定")]
    public AudioClip damageSound;       // ダメージ音
    public AudioClip deathSound;        // 死亡音
    public AudioClip shootSound;        // 射撃音
    public AudioClip reloadSound;       // リロード音
    public AudioClip hitSound;          // 命中音
    
    [Header("ゲーム設定")]
    public bool autoStartBGM = true;    // 自動的にBGMを開始するか
    
    void Start()
    {
        // BGMマネージャーの初期化
        InitializeBGM();
        
        // サウンドマネージャーの初期化
        InitializeSoundManager();
        
        // その他のゲーム初期化処理をここに追加
        Debug.Log("ゲームが開始されました！");
    }
    
    void InitializeBGM()
    {
        // BGMマネージャーが存在しない場合は作成
        if (BGMManager.Instance == null)
        {
            GameObject bgmManagerObject = new GameObject("BGMManager");
            BGMManager bgmManager = bgmManagerObject.AddComponent<BGMManager>();
            
            // BGMの設定
            if (gameBGM != null)
            {
                bgmManager.bgmClip = gameBGM;
                bgmManager.volume = bgmVolume;
                bgmManager.loop = true;
                bgmManager.playOnStart = autoStartBGM;
            }
        }
        else
        {
            // 既存のBGMマネージャーにBGMを設定
            if (gameBGM != null)
            {
                BGMManager.Instance.bgmClip = gameBGM;
                BGMManager.Instance.SetVolume(bgmVolume);
                
                if (autoStartBGM && !BGMManager.Instance.IsPlaying())
                {
                    BGMManager.Instance.PlayBGM();
                }
            }
        }
    }
    
    void InitializeSoundManager()
    {
        // サウンドマネージャーが存在しない場合は作成
        if (SoundManager.Instance == null)
        {
            GameObject soundManagerObject = new GameObject("SoundManager");
            SoundManager soundManager = soundManagerObject.AddComponent<SoundManager>();
            
            // 効果音の設定
            soundManager.damageSound = damageSound;
            soundManager.deathSound = deathSound;
            soundManager.shootSound = shootSound;
            soundManager.reloadSound = reloadSound;
            soundManager.hitSound = hitSound;
        }
        else
        {
            // 既存のサウンドマネージャーに効果音を設定
            if (damageSound != null) SoundManager.Instance.damageSound = damageSound;
            if (deathSound != null) SoundManager.Instance.deathSound = deathSound;
            if (shootSound != null) SoundManager.Instance.shootSound = shootSound;
            if (reloadSound != null) SoundManager.Instance.reloadSound = reloadSound;
            if (hitSound != null) SoundManager.Instance.hitSound = hitSound;
        }
    }
    
    // 他のスクリプトからBGMを制御するための静的メソッド
    public static void PlayBGM()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM();
        }
    }
    
    public static void StopBGM()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.StopBGM();
        }
    }
    
    public static void PauseBGM()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PauseBGM();
        }
    }
    
    public static void ResumeBGM()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.ResumeBGM();
        }
    }
    
    public static void SetBGMVolume(float volume)
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetVolume(volume);
        }
    }
    
    // 効果音制御の静的メソッド
    public static void PlayDamageSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayDamageSound();
        }
    }
    
    public static void PlayDeathSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayDeathSound();
        }
    }
    
    public static void PlayShootSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayShootSound();
        }
    }
    
    public static void SetSFXVolume(float volume)
    {
        if (SoundManager.Instance != null)
        {
            // SoundManager.Instance.sfxVolume = volume;
        }
    }
} 