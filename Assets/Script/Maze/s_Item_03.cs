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
        cube = GameManager.instance.UI.GetComponent<s_TaskControl_03>().cube;
        if (GetComponent<Button>() != null)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate ()
            {
                AudioManage.instance.SetClips(ClipSelect.选择);
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
                GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightCube = true;
            }
            else
            {
                cube.transform.GetChild(4).gameObject.SetActive(false);
                GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightCube = false;
                //输出错误信息
                GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(7);
                GameManager.instance.diaLogDisplay.SetActive(true);
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
                GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightTriangle = true;
            }
            else
            {
                //输出错误信息
                GameManager.instance.diaLogDisplay.GetComponent<y_TextDisplay>().SetTextFile(7);
                GameManager.instance.diaLogDisplay.SetActive(true);

                for (int i = 0; i < cube.transform.childCount - 1; i++)
                {
                    cube.transform.GetChild(i).gameObject.SetActive(false);
                    
                }
                GameManager.instance.UI.GetComponent<s_TaskControl_03>().IsRightTriangle = false;
            }
        }

        
        
    }
}
