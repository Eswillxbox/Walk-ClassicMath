using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "自定义/道具", order = 0)]
public class Item : ScriptableObject
{
    public string Index;//名称
    public int ItemCount;//数量
    public Sprite Icon;//图标

    
}
public enum ItemType
{
    生命值, 体力, 其他
}
