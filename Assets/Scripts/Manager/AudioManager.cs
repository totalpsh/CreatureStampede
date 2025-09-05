using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 1;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 1;
    public int channels = 16;
    AudioSource[] sfxPlayers;
    int channelIndex;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        bgmClip = Resources.Load<AudioClip>(Path.Sound + "BGM/BGM");
        // ������� �÷��̾� ����
        GameObject bgmObjcet = new GameObject("BGM Player");
        bgmObjcet.transform.parent = this.transform;
        bgmPlayer = bgmObjcet.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmPlayer.Play();

        // ȿ���� �÷��̾� ����
        GameObject sfxObjcet = new GameObject("SFX Player");
        sfxObjcet.transform.parent = this.transform;
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length;i++)
        {
            sfxPlayers[i] = sfxObjcet.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
        }

    }

    public void PlaySfx(AudioClip clip)
    {
        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if(!sfxPlayers[loopIndex].isPlaying )
            {
                sfxPlayers[loopIndex].clip = clip;
                sfxPlayers[loopIndex].Play();
                break;
            }
        }
    }
}
