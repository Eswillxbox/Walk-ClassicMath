using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class s_Item : MonoBehaviour
{

    public int number;//������ʾ������
    public int index;//����
    public GameObject itemObj;
    public Material firstMaterial;
    public Material highLight_Red;
    public bool isHighLight;


    // Start is called before the first frame update
    void Start()
    {
        if (Regex.IsMatch(this.gameObject.name, @"^[0-9]+$"))
        {
            number = int.Parse(this.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (isHighLight)
        {
            foreach (var item in GetComponentsInChildren<MeshRenderer>())
            {
                item.material = highLight_Red;
            }
        }
    }

    private void OnMouseExit()
    {
        SetStartMaterial();
    }

    public void SetStartMaterial()
    {
        isHighLight = false;
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            item.material = firstMaterial;
        }
    }
}
