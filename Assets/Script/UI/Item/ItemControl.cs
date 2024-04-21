using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    //道具预制体
    public GameObject ItemPrefab;
    //道具栏
    public Transform ItemsPrefab;

    //记录道具索引和数量,用于存储
    public Dictionary<string,int> ItemCount = new Dictionary<string,int>();
    //道具栏
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


    //初始化
    void Init()
    {
        foreach (var item in Resources.LoadAll("Item"))
        {
            ItemDic.Add(((Item)item).Index, ((Item)item));
        }

        //这里添加从人物读取数据到ItemCount的功能


        bool firstEnter;
        if (ItemCount == null || ItemCount.Count == 0)//判断是否是第一次进入场景
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
                //初次的话添加数据进角色道具数据,数据读取和存储只在场景开始和结束进行
                ItemCount.Add(item.Key, item.Value.ItemCount);
            }
            else
            {
                //不是第一次读取数据,将玩家身上的道具数量读取到道具本身
                item.Value.ItemCount = ItemCount[item.Key];
            }
        }

        foreach (var item in ItemDic)
        {
            CreateNewItem(ItemDic[item.Key]);
        }
    }

    //创建道具，并附加值
    void CreateNewItem(Item item)
    {
        //创建新的道具展示，并设置服务提
        GameObject newItem = Instantiate(ItemPrefab,ItemsPrefab);
        newItem.transform.SetParent(ItemsPrefab);

        //打印
        newItem.transform.Find("道具展示").GetComponent<Image>().sprite = item.Icon;
        newItem.transform.Find("道具数量").GetComponent<Text>().text = item.ItemCount.ToString();


        //创建过相同的道具就更新否则创建
        if (Items.ContainsKey(item.Index))
        {
            Items[item.Index] = newItem;
        }
        else
        {
            Items.Add(item.Index, newItem);
        }
    }

    //更新UI
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
