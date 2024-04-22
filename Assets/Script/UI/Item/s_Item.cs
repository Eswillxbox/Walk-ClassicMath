using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Item : MonoBehaviour
{
   
    public int number;//自身表示的数字
    public bool isColliderItem;//判断是否碰撞到道具

    

    // Start is called before the first frame update
    void Start()
    {
        number = int.Parse(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        GetItem();
    }

    //拾取道具
    void GetItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColliderItem)
        {
            s_Item_UI.instance.UpdateUI(number);//更新UI为对应的数字
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliderItem = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliderItem = false;
        }
    }
}
