using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Basic : MonoBehaviour
{
    [Header("基本属性")]
    public TextMesh numText;
    public GameObject leftBasic;
    public GameObject rightBasic;
    public String basic_kind;
    private GameObject basicEffect;
    void Start()
    {
        SetBasicKind();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetBasicKind()
    {
        int i = UnityEngine.Random.Range(0, 4);
        switch (i)
        {
            case 1: basic_kind = "得与失"; break;
            case 2: basic_kind = "陷阱"; break;
            case 3: basic_kind = "无用"; break;
            case 0: basic_kind = "帮助"; break;
            default: break;
        }
    }

    public void DownBasic()
    {
        this.transform.position -= new Vector3(0, 0.28f, 0);
        if (basicEffect == null)
            basicEffect = Instantiate(GameManager.instance.GetEffect(basic_kind), this.transform.position, this.transform.rotation, this.transform);
    }

    public void UpBasic()
    {
        this.transform.position += new Vector3(0, 0.28f, 0);
        //平台上升时效果消失
        if (basicEffect != null)
            basicEffect.gameObject.SetActive(false);
    }
}
