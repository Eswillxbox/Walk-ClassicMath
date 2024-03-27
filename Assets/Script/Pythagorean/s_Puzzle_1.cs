using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_Puzzle_1 : MonoBehaviour
{
    //UI的子物体


    //创建的空物体(存储精灵的物体)
    GameObject spawned_Object;

    //判断是否可以生成
    bool create_Enable = true;



    // Start is called before the first frame update
    void Start()
    {
        // 添加EventTrigger组件
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // 创建一个新的EventTrigger.Entry用于PointerDown事件
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { OnImageClicked((PointerEventData)data); });

        // 将PointerDown事件添加到EventTrigger
        eventTrigger.triggers.Add(pointerDownEntry);
    }

    // Update is called once per frame
    void Update()
    {
        //如果没有生成的物体都不执行
        if (spawned_Object == null)
        {
            return;
        }

        //生成物跟随
        FollowMouse(spawned_Object);


        //执行抬起的时候的方法
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }
    }


    //生成物跟随
    void FollowMouse(GameObject gameObject)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gameObject.transform.position = mousePos;
    }


    //鼠标左键抬起，将算式的子物体（填空）精灵更新成创建的物体的精灵
    void OnMouseButtonUp()
    {
        //获取鼠标位置，转换成世界坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //获取鼠标位置的所有物体
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Formula"))
            {
                //只有当名字一致才能添加上
                if (hit.collider.gameObject.name == spawned_Object.name)
                {
                    //更换对应位置的精灵展示
                    hit.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
                        = spawned_Object.GetComponent<SpriteRenderer>().sprite;
                    //添加上之后设置该处不能创建
                    create_Enable = false;
                    //将自身的颜色设置成黑色，表示不可以选中
                    gameObject.GetComponent<Image>().color = Color.black;
                    break;
                }
                
            }
        }
        //不论是否添加上都销毁该物体
        Destroy(spawned_Object);
    }


    //点击UI生成物体
    public void OnImageClicked(PointerEventData eventData)
    {
        //如果不存在卡片，不允许创建
        if (!create_Enable)
        {
            return;
        }

        Image clickedImage;
        //这里进行一个判断防止点击到子物体，获取到要获取的对象身上的精灵
        if (eventData.pointerCurrentRaycast.gameObject.transform.childCount == 0)
        {
            clickedImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();
        }
        else
        {
            // 获取点击的Image的子物体image组件
            clickedImage = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).GetComponent<Image>();
        }
        
        // 获取Image的精灵图片
        Sprite sprite = clickedImage.sprite;

        // 创建一个空物体
        spawned_Object = new GameObject(eventData.pointerCurrentRaycast.gameObject.name);
        //设置创建物体的大小
        spawned_Object.transform.localScale = new Vector3(0.4f,0.5f,1);
        // 添加SpriteRenderer组件，并将精灵图片添加到空物体上
        SpriteRenderer sr = spawned_Object.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        // 设置跟随鼠标的标志为真
        //isFollowing = true;
    }




}
