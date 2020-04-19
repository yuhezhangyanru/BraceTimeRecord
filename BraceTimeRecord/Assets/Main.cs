using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public RectTransform rectParent;
    public ItemDateInfo item0;

    private Vector3 pos;
    private int childIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        //初始化组件
        string totalDays = PlayerPrefs.GetString(Const.TOTAL_DAYS);

        Debug.Log("读取所有保存过的时间=" + totalDays + "@");
        string[] array = totalDays.Split(',');
        Dictionary<string, float> timeDic = new Dictionary<string, float>();
        for(int index = 0; index < array.Length; index ++)
        {
            if(array[index] == "")
            {
                continue;
            }
            string tempName = array[index].Substring(Const.DATE_TIME_HEAD.Length);
            timeDic.Add(tempName, PlayerPrefs.GetFloat(array[index]));
        }

        //yanruTODO 测试
        for(int index = 0; index < 30; index ++)
        {
            timeDic.Add("测试日期" + index, index * 100);
        }

        //初始化
        for(int index =0; index < rectParent.childCount; index ++)
        {
            rectParent.GetChild(index).gameObject.SetActive(false);
        }

        pos = new Vector3();

        //构造
        List<string> keyList = new List<string>();
        keyList.AddRange(timeDic.Keys);
        keyList.Reverse();
        for (int index = 0; index < keyList.Count; index ++ )
        //foreach(KeyValuePair<string, float> item in timeDic)
        {

            ItemDateInfo itemTemp = GameObject.Instantiate(item0);
            itemTemp.name = "item_" + (childIndex ++);
            itemTemp.gameObject.SetActive(true);
            itemTemp.transform.SetParent(rectParent);
            itemTemp.transform.position = pos;
            pos.y -= 120;

            itemTemp.txtDate.text = keyList[index];
            itemTemp.txtTime.text = TimeUtils.GetTime(timeDic[keyList[index]]);
        }
    }
     

}
