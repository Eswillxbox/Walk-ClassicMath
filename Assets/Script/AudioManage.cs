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
            case ClipSelect.跳转:
                source.PlayOneShot(clips[0]);
                break;

            case ClipSelect.选择:
                source.PlayOneShot(clips[1]);
                break;

            case ClipSelect.获取道具:
                source.PlayOneShot(clips[2]);
                break;
            case ClipSelect.使用道具:
                source.PlayOneShot(clips[3]);
                break;
        }
    }
}

public enum ClipSelect
{
    跳转,选择,获取道具, 使用道具
}
