using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeTimeButton : MonoBehaviour
{

    private bool isTaking = false;//默认是没有计时，但是也需要根据退出后台的状态记录一下当前的计时状态
    private float todaySeconds = 0f; //今日已经不带牙套的时间
    private float lastTodaySeconds = 0f;

    public Text todayTime; //今日计时
    public Text txtButton;
    public Image imgAnim;//动画
    public Text txtStartTimestamp;

    private float imgAnimValue = 0f;

    public ItemDateInfo itemToday;//当天的元素

    private long timeTicks = 0;
    /**
     * 点击计时的按钮
     * **/
    public void ClickTakingTimeButton()
    {
        Debug.Log("当前的及时状态=" + isTaking + ",getDate="+ TimeUtils.getDate()+ "@");
        txtButton.text = isTaking ? "开始计时" : "结束计时";
        if(isTaking == true)//说明该保存啦
        {
            //用来记录从开始按钮到现在的时间戳，避免因为系统休眠漏掉时间
            float addTime = ((System.DateTime.Now.Ticks - timeTicks) / 10000 / 1000f);

            //保存记录时间
            string todayKey = Const.DATE_TIME_HEAD + TimeUtils.getDate();
            PlayerPrefs.SetFloat(todayKey, addTime);
            string totalDays = PlayerPrefs.GetString(Const.TOTAL_DAYS);

            Debug.Log("此时的总记录=" + totalDays + ",保存?" + ((totalDays.IndexOf(todayKey) == -1))
                +",追加时间="+ addTime 
                +",时间差2="+(todaySeconds - lastTodaySeconds)
                + ",date="+System.DateTime.Now.ToLongDateString());

            if (totalDays.IndexOf(todayKey) == -1)
            {
                totalDays = totalDays + "," + todayKey;
                PlayerPrefs.SetString(Const.TOTAL_DAYS, totalDays);
            }

            lastTodaySeconds = todaySeconds;

            //重置动画
            imgAnimValue = 0f;
            imgAnim.fillAmount = imgAnimValue;


            //刷新列表
            if (itemToday != null)
            {
                itemToday.txtTime.text = todayTime.text;
            }

            txtStartTimestamp.text = "";
        }
        else
        {
            //开始记录时间
            txtStartTimestamp.text = System.DateTime.Now.ToString()+"开始计时";
            timeTicks = System.DateTime.Now.Ticks;
        }
        isTaking = !isTaking;
    }

    // Start is called before the first frame update
    void Start()
    {
        todaySeconds = PlayerPrefs.GetFloat(Const.DATE_TIME_HEAD+ TimeUtils.getDate());
        txtStartTimestamp.text = "";

        Debug.Log("今天记录的时间是=" + todaySeconds + "@");
        lastTodaySeconds = todaySeconds;
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

}
