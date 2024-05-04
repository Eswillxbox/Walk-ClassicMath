using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    static public AudioManage instance;
    public AudioClip Bgm;
    public AudioClip[] clips;

    AudioSource source;

    private void Awake()
    {
        if (instance != null)
            AudioManage.Destroy(instance);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = Bgm;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClips(ClipSelect clipSelect)
    {
        switch (clipSelect)
        {
            case ClipSelect.��ת:
                source.PlayOneShot(clips[0]);
                break;

            case ClipSelect.ѡ��:
                source.PlayOneShot(clips[1]);
                break;

            case ClipSelect.��ȡ����:
                source.PlayOneShot(clips[2]);
                break;
            case ClipSelect.ʹ�õ���:
                source.PlayOneShot(clips[3]);
                break;
        }
    }
}

public enum ClipSelect
{
    ��ת,ѡ��,��ȡ����, ʹ�õ���
}
