using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class s_Item : MonoBehaviour
{
   
    public int number;//�����ʾ������
    public int index;//����
    public GameObject itemObj;

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
}
