using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s_Player_01 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetItem(RaycastHit hitInfo)
    {
        GameManager.instance.UI.GetComponent<s_UIControl>().UpdateUI(hitInfo.collider.gameObject.GetComponent<s_Item>().index);
        //¼ñµ½Ïú»Ù
        hitInfo.collider.gameObject.SetActive(false);


    }
}
