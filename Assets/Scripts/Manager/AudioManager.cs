using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("BGM")]
    public AudioClip bgmClip;
    AudioSource bgmPlayer;
    private float bgmVolume = 0.5f;
    public float BgmVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmPlayer.volume = bgmVolume;
        }
    }

    [SerializeField]AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public int channels = 16;
    AudioSource[] sfxPlayers;
    private float sfxVolume = 0.5f;
    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            foreach (var player in sfxPlayers)
            {
                player.volume = sfxVolume;
            }
        }
    }


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

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObjcet.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }

    }

    public void EffectBgm(bool isPlay)
    {
        if(bgmEffect == null) return;

        bgmEffect.enabled = isPlay;
    }
    public void PlaySfx(AudioClip clip)
    {
        // 사용하지 않는 플레이어 찾는 구조 좋음
        // 실행가능한 플레이어를 얻어오는 기능을 별도로 두는 것도 고려
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            // 루프 로직 많이 활용됨
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (!sfxPlayers[loopIndex].isPlaying)
            {
                sfxPlayers[loopIndex].clip = clip;
                sfxPlayers[loopIndex].Play();
                break;
            }
        }
    }

    public void StartScene()
    {
        bgmEffect = Camera.main.gameObject.AddComponent<AudioHighPassFilter>();
        bgmEffect.cutoffFrequency = 2500;
        EffectBgm(false);
    }
}
