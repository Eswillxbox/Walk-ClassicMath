using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_Item_03 : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isCube;
    public int outsideLength;//外边正方形长度
    public int insideLength;

    Button button;
    GameObject cube;


    private void Start()
    {
        cube = GameManager.instance.UI.GetComponent<s_UIControl_03>().cube;
        if (GetComponent<Button>() != null)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate ()
            {
                JudgeAnswer();
            });
        }
    }

    void JudgeAnswer()
    {
        if (isCube)
        {
            if (cube.GetComponent<s_Item_03>().insideLength == insideLength)
            {
                cube.transform.GetChild(4).gameObject.SetActive(true);
                GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightCube = true;
            }
            else
            {
                cube.transform.GetChild(4).gameObject.SetActive(false);
                GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightCube = false;
                print("似乎这个图案大小不适合");
            }
        }
        else
        {
            if (cube.GetComponent<s_Item_03>().outsideLength == outsideLength)
            {
                //显示三角形
                for (int i = 0; i < cube.transform.childCount - 1; i++)
                {
                    cube.transform.GetChild(i).gameObject.SetActive(true);
                    
                }
                GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightTriangle = true;
            }
            else
            {
                print("似乎这个图案大小不适合");
                for (int i = 0; i < cube.transform.childCount - 1; i++)
                {
                    cube.transform.GetChild(i).gameObject.SetActive(false);
                    
                }
                GameManager.instance.UI.GetComponent<s_UIControl_03>().IsRightTriangle = false;
            }
        }

        
        
    }
}
