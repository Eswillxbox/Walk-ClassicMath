using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_Puzzle_1 : MonoBehaviour
{
    //UI��������


    //�����Ŀ�����(�洢���������)
    GameObject spawned_Object;

    //�ж��Ƿ��������
    bool create_Enable = true;



    // Start is called before the first frame update
    void Start()
    {
        // ���EventTrigger���
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        // ����һ���µ�EventTrigger.Entry����PointerDown�¼�
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { OnImageClicked((PointerEventData)data); });

        // ��PointerDown�¼���ӵ�EventTrigger
        eventTrigger.triggers.Add(pointerDownEntry);
    }

    // Update is called once per frame
    void Update()
    {
        //���û�����ɵ����嶼��ִ��
        if (spawned_Object == null)
        {
            return;
        }

        //���������
        FollowMouse(spawned_Object);


        //ִ��̧���ʱ��ķ���
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }
    }


    //���������
    void FollowMouse(GameObject gameObject)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gameObject.transform.position = mousePos;
    }


    //������̧�𣬽���ʽ�������壨��գ�������³ɴ���������ľ���
    void OnMouseButtonUp()
    {
        //��ȡ���λ�ã�ת������������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //��ȡ���λ�õ���������
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Formula"))
            {
                //ֻ�е�����һ�²��������
                if (hit.collider.gameObject.name == spawned_Object.name)
                {
                    //������Ӧλ�õľ���չʾ
                    hit.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
                        = spawned_Object.GetComponent<SpriteRenderer>().sprite;
                    //�����֮�����øô����ܴ���
                    create_Enable = false;
                    //���������ɫ���óɺ�ɫ����ʾ������ѡ��
                    gameObject.GetComponent<Image>().color = Color.black;
                    break;
                }
                
            }
        }
        //�����Ƿ�����϶����ٸ�����
        Destroy(spawned_Object);
    }


    //���UI��������
    public void OnImageClicked(PointerEventData eventData)
    {
        //��������ڿ�Ƭ����������
        if (!create_Enable)
        {
            return;
        }

        Image clickedImage;
        //�������һ���жϷ�ֹ����������壬��ȡ��Ҫ��ȡ�Ķ������ϵľ���
        if (eventData.pointerCurrentRaycast.gameObject.transform.childCount == 0)
        {
            clickedImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();
        }
        else
        {
            // ��ȡ�����Image��������image���
            clickedImage = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).GetComponent<Image>();
        }
        
        // ��ȡImage�ľ���ͼƬ
        Sprite sprite = clickedImage.sprite;

        // ����һ��������
        spawned_Object = new GameObject(eventData.pointerCurrentRaycast.gameObject.name);
        //���ô�������Ĵ�С
        spawned_Object.transform.localScale = new Vector3(0.4f,0.5f,1);
        // ���SpriteRenderer�������������ͼƬ��ӵ���������
        SpriteRenderer sr = spawned_Object.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        // ���ø������ı�־Ϊ��
        //isFollowing = true;
    }




}
