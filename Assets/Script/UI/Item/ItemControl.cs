using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    //����Ԥ����
    public GameObject ItemPrefab;
    //������
    public Transform ItemsPrefab;

    //��¼��������������,���ڴ洢
    public Dictionary<string,int> ItemCount = new Dictionary<string,int>();
    //������
    private Dictionary<string,Item> ItemDic = new Dictionary<string,Item>();

    private Dictionary<string,GameObject> Items = new Dictionary<string, GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //��ʼ��
    void Init()
    {
        foreach (var item in Resources.LoadAll("Item"))
        {
            ItemDic.Add(((Item)item).Index, ((Item)item));
        }

        //������Ӵ������ȡ���ݵ�ItemCount�Ĺ���


        bool firstEnter;
        if (ItemCount == null || ItemCount.Count == 0)//�ж��Ƿ��ǵ�һ�ν��볡��
        {
            firstEnter = true;
        }
        else
        {
            firstEnter = false;
        }

            foreach (var item in ItemDic)
        {
            if (firstEnter)
            {
                //���εĻ�������ݽ���ɫ��������,���ݶ�ȡ�ʹ洢ֻ�ڳ�����ʼ�ͽ�������
                ItemCount.Add(item.Key, item.Value.ItemCount);
            }
            else
            {
                //���ǵ�һ�ζ�ȡ����,��������ϵĵ���������ȡ�����߱���
                item.Value.ItemCount = ItemCount[item.Key];
            }
        }

        foreach (var item in ItemDic)
        {
            CreateNewItem(ItemDic[item.Key]);
        }
    }

    //�������ߣ�������ֵ
    void CreateNewItem(Item item)
    {
        //�����µĵ���չʾ�������÷�����
        GameObject newItem = Instantiate(ItemPrefab,ItemsPrefab);
        newItem.transform.SetParent(ItemsPrefab);

        //��ӡ
        newItem.transform.Find("����չʾ").GetComponent<Image>().sprite = item.Icon;
        newItem.transform.Find("��������").GetComponent<Text>().text = item.ItemCount.ToString();


        //��������ͬ�ĵ��߾͸��·��򴴽�
        if (Items.ContainsKey(item.Index))
        {
            Items[item.Index] = newItem;
        }
        else
        {
            Items.Add(item.Index, newItem);
        }
    }

    //����UI
    void RefreshUI()
    {
        if (ItemDic == null)
        {
            return;
        }

        
        foreach (var item in ItemDic)
        {
            CreateNewItem(ItemDic[item.Key]);
        }


    }

    void GetItem(string index)
    {

    }

}
