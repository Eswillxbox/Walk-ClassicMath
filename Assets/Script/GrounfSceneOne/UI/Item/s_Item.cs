using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class s_Item : MonoBehaviour
{
   
    public int number;//自身表示的数字
    public int index;//索引
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
