using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "�Զ���/����", order = 0)]
public class Item : ScriptableObject
{
    public string Index;//����
    public int ItemCount;//����
    public Sprite Icon;//ͼ��

    
}
public enum ItemType
{
    ����ֵ, ����, ����
}
