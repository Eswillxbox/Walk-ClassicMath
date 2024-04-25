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
    private Vector3 velocity;
    private Vector3 targetPosition;
    [Header("特殊效果")]
    public TextMesh kindText;
    public string basic_kind;
    private GameObject basicEffect;
    private Material firstMaterial;
    public Material highLight_Blue;
    public Material highLight_Red;
    public int isWaitChoose = 0;
    public bool isBackBasic = false;
    void Start()
    {
        targetPosition = transform.position;
        //生成时浮起
        UpBasic();
        firstMaterial = GetComponent<MeshRenderer>().material;
        SetBasicKind();
        numText.text = basicNum.ToString();
        kindText.text = basic_kind;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f);
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
        targetPosition -= new Vector3(0, 0.28f, 0);
        numText.gameObject.SetActive(true);
        if (basicEffect == null)
        {
            basicEffect = Instantiate(GameManager.instance.GetEffect(basic_kind), this.transform.position, this.transform.rotation, this.transform);
            Invoke("StopEffect", 2.0f);
        }
    }

    public void UpBasic()
    {
        targetPosition += new Vector3(0, 0.28f, 0);
        numText.gameObject.SetActive(false);
        isWaitChoose = 0;
        //平台上升时效果消失
        if (basicEffect != null)
        {
            basicEffect.gameObject.SetActive(false);
            //第二次浮起说明被走过，失去特殊效果
            basic_kind = "无用";
        }

    }

    private void StopEffect()
    {
        basicEffect.GetComponent<ParticleSystem>().Stop();
    }

    public void ReBasic()
    {
        targetPosition -= new Vector3(0, 0.28f, 0);
        Invoke("DesBasic", 0.5f);
    }

    private void DesBasic()
    {
        Destroy(this.gameObject);
    }
    public void RePlayerBasic()
    {
        targetPosition = new Vector3(-15, -0.56f, 0);
    }

    private void OnMouseEnter()
    {
        if (isWaitChoose != 0)
        {
            GetComponent<MeshRenderer>().material = highLight_Blue;
            kindText.gameObject.SetActive(true);
        }
        else if (isBackBasic)
            GetComponent<MeshRenderer>().material = highLight_Red;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = firstMaterial;
        kindText.gameObject.SetActive(false);
    }
}
