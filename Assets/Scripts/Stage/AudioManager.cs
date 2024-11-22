using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using EnumManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolum;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolum;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init() 
    {
        //배경음
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClip;
        bgmPlayer.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[1];
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //효과음
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i=0; i<sfxPlayers.Length; i++){
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolum;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
        }        
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay){
            bgmPlayer.Play();
        }
        else{
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx (Sfx sfx) {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if (sfx == Sfx.Hit)
            {
                ranIndex = Random.Range(0, 2);
            }

            sfxPlayers[loopIndex].volume = sfxVolum;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void PlaySfx (int idx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if ((Sfx) idx == Sfx.Hit)
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[idx + ranIndex];
            sfxPlayers[loopIndex].Play();
            print(sfxClips[idx + ranIndex].name);
            break;
        }
    }
}
