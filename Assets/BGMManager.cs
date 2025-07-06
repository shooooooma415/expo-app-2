using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // シングルトンパターンでBGMマネージャーを管理
    public static BGMManager Instance { get; private set; }
    
    [Header("BGM設定")]
    public AudioClip bgmClip;           // BGMのAudioClip
    public float volume = 0.5f;         // BGMの音量
    public bool loop = true;            // ループ再生するかどうか
    public bool playOnStart = true;     // 開始時に自動再生するかどうか
    
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
            audioSource.clip = bgmClip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.playOnAwake = false; // 手動で制御するためfalse
        }
        else
        {
            // 既にBGMマネージャーが存在する場合は破棄
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if (playOnStart && bgmClip != null)
        {
            PlayBGM();
        }
    }
    
    // BGMを再生
    public void PlayBGM()
    {
        if (audioSource != null && bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }
    }
    
    // BGMを停止
    public void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    
    // BGMを一時停止
    public void PauseBGM()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }
    
    // BGMを再開
    public void ResumeBGM()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
    
    // 音量を設定
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // 0.0f から 1.0f の範囲に制限
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
    
    // 音量を取得
    public float GetVolume()
    {
        return volume;
    }
    
    // BGMが再生中かどうかを確認
    public bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
} 