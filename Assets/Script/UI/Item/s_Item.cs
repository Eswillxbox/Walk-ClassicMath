using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Item : MonoBehaviour
{
   
    public int number;//�����ʾ������
    public bool isColliderItem;//�ж��Ƿ���ײ������

    

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

    //ʰȡ����
    void GetItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColliderItem)
        {
            s_Item_UI.instance.UpdateUI(number);//����UIΪ��Ӧ������
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
