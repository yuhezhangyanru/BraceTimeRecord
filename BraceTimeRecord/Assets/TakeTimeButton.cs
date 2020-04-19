using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeTimeButton : MonoBehaviour
{

    private bool isTaking = false;//默认是没有计时，但是也需要根据退出后台的状态记录一下当前的计时状态
    private float todaySeconds = 0f; //今日已经不带牙套的时间

    public Text todayTime; //今日计时
    public Text txtButton;
    public Image imgAnim;//动画
    private float imgAnimValue = 0f;

    /**
     * 点击计时的按钮
     * **/
    public void ClickTakingTimeButton()
    {
        Debug.Log("当前的及时状态=" + isTaking + ",getDate="+ getDate()+ "@");
        txtButton.text = isTaking ? "开始计时" : "结束计时";
        if(isTaking == true)//说明该保存啦
        {
            //保存记录时间
            string todayKey = Const.DATE_TIME_HEAD + getDate();
            PlayerPrefs.SetFloat(todayKey, todaySeconds);
            string totalDays = PlayerPrefs.GetString(Const.TOTAL_DAYS);
            Debug.Log("此时的总记录=" + totalDays + ",保存?" + ((totalDays.IndexOf(todayKey) == -1)));
            if (totalDays.IndexOf(todayKey) == -1)
            {
                totalDays = totalDays + "," + todayKey;
                PlayerPrefs.SetString(Const.TOTAL_DAYS, totalDays);
            }
        }
        isTaking = !isTaking;
    }

    // Start is called before the first frame update
    void Start()
    {
        todaySeconds = PlayerPrefs.GetFloat(Const.DATE_TIME_HEAD+getDate());
        Debug.Log("今天记录的时间是=" + todaySeconds + "@");
        printTime(todayTime, todaySeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTaking)
        {
            todaySeconds += Time.deltaTime;

            imgAnimValue += Time.deltaTime;// * 0.1f;
            if(imgAnimValue > 1f)
            {
                imgAnimValue = 0f;
            }
            imgAnim.fillAmount = imgAnimValue;

            printTime(todayTime, todaySeconds);
        }
    }

    //打印时间
    private void printTime(Text text, float timeSecond)
    {
        text.text = TimeUtils.GetTime(timeSecond);
    }


    public string getDate()
    {
        return  System.DateTime.Now.ToShortDateString();
    }
}
