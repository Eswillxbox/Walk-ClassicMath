using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class y_Basic : MonoBehaviour
{
    [Header("基本属性")]
    public TextMesh numText;
    public int basicNum;
    public GameObject leftBasic;
    public GameObject rightBasic;
    [Header("特殊效果")]
    public string basic_kind;
    private GameObject basicEffect;
    private Material firstMaterial;
    public Material highLight;
    public int isWaitChoose = 0;
    void Start()
    {
        firstMaterial = GetComponent<MeshRenderer>().material;
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
        numText.gameObject.SetActive(true);
        if (basicEffect == null)
        {
            basicEffect = Instantiate(GameManager.instance.GetEffect(basic_kind), this.transform.position, this.transform.rotation, this.transform);
            Invoke("StopEffect", 2.0f);
        }
    }

    public void UpBasic()
    {
        this.transform.position += new Vector3(0, 0.28f, 0);
        numText.gameObject.SetActive(false);
        isWaitChoose = 0;
        //平台上升时效果消失
        if (basicEffect != null)
            basicEffect.gameObject.SetActive(false);
        basic_kind = "无用";
    }

    private void StopEffect()
    {
        basicEffect.GetComponent<ParticleSystem>().Stop();
    }

    public void ReBasic()
    {
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        if (isWaitChoose != 0)
            GetComponent<MeshRenderer>().material = highLight;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = firstMaterial;
    }
}
