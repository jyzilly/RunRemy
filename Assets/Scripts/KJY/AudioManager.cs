using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    //�� ȿ������ �ν����� ������� �̸�����
    public enum sfx {bear, cactus, car, carpet, hat, human, jack, rabbit, tire, sphinx, playerOh, tonado, manscream, funscream, ough }


    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        //
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int index = 0; index < sfxPlayers.Length; ++index)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].loop = false;
        }

    }


    //����ϴ� ���
    //����� ���;��ϴ� ��������
    //AudioManager.instance.PlaySfx(AudioManager.Sfx.�̶� �̸� ������ �� enum ȿ���� �̸��� �Է�);
    public void PlaySfx(sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; ++index)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;/*�� �������� �������ߴ� �÷��̾��� �ε���*/
            if (sfxPlayers[loopIndex].isPlaying)

                continue;

            //���� �̸��� ȿ������ ������ ���� �� �� �������� �Ҹ��� ���  ��Ű�� ������
            //���� ������ �ִ°� ������ ������ ����ġ ������ �����ָ� �ȴ�.
            //int ranIndex = 0;
            //if(sfx == sfx.Hit || sfx == sfx.Melee)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }

        // channel �ϳ��� ����
        //sfxPlayers[0].clip = sfxClip[(int)sfx];
        //sfxPlayers[0].Play();
    }

    public void Play3DSfx(sfx sfx, Vector3 position, float volume, float minDistance, float maxDistance)
    {
        // �ӽ� ����� �ҽ� ������Ʈ ����
        GameObject tempSfxObject = new GameObject("Temp3DSfx");
        tempSfxObject.transform.position = position;
        AudioSource tempAudio = tempSfxObject.AddComponent<AudioSource>();

        tempAudio.clip = sfxClip[(int)sfx];
        tempAudio.volume = volume;
        tempAudio.spatialBlend = 1f;             // 3D ȿ�������� ���� (0:2D, 1:3D)
        tempAudio.minDistance = minDistance;      // �ּ� �Ÿ�
        tempAudio.maxDistance = maxDistance;      // �ִ� �Ÿ�
        tempAudio.rolloffMode = AudioRolloffMode.Logarithmic; // ���� ���

        tempAudio.Play();

        // AudioClip ���� + �ణ�� ���� �Ŀ� �ӽ� ������Ʈ ����
        Destroy(tempSfxObject, tempAudio.clip.length + 0.5f);
    }
}