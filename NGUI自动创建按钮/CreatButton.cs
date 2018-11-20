using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatButton : MonoBehaviour
{
    //按钮预设物
    public GameObject ButtonPrefab;

    //按钮数量
    public int ButtonCount;

    //按钮生成点的位置
    public GameObject ButtonLocation;

    //生成的按钮的父物体位置
    public GameObject FatherLocation;

    //生成按钮水平距离
    public float HorizontalRange;

    //生成按钮竖直距离
    public float VerticalRange;

    //生成按钮的名字
    public List<string> ButtonNameList = new List<string>();

    //获取按钮的Text  让ButtonNameList赋值给他
    UILabel buttonText;

    //生成按钮的宽度的数组  
    public List<int> ButtonWidthList = new List<int>();

    //生成按钮的长度的数组
    public List<int> ButtonLengthList = new List<int>();

    //生成按钮名字字体大小的数组
    public List<int> ButtonNameFontSizeList = new List<int>();

    //生成按钮的图片所需要的图集
    public UIAtlas ButtonAtlas;

    //生成按钮的图片名字数组
    public List<string> ButtonPicNameList = new List<string>();


    void Start()
    {
        BurnButton();
    }


    private void BurnButton()
    {
        for (int i = 0; i < ButtonCount; i++)
        {
            //生成以及添加在父物体下
            GameObject button = NGUITools.AddChild(FatherLocation, ButtonPrefab);
            //改位置
            button.transform.position = new Vector2(ButtonLocation.transform.position.x + i * HorizontalRange,
                ButtonLocation.transform.position.y                                     + i * VerticalRange);
            //改名字
            buttonText = button.GetComponent<UILabel>();
            if (ButtonNameList.Count == ButtonCount)
            {
                buttonText.text = ButtonNameList[i];
            }

            //添加点击事件
            UIEventListener.Get(button).onClick = OnClickEvent;
            //获取按钮宽度 赋值
            if (ButtonWidthList.Count == ButtonCount)
            {
                button.GetComponent<UISprite>().width = ButtonWidthList[i];
            }

            // 获取按钮长度 赋值
            if (ButtonLengthList.Count == ButtonCount)
            {
                button.GetComponent<UISprite>().height = ButtonLengthList[i];
            }

            //获取字体大小
            if (ButtonNameFontSizeList.Count == ButtonCount)
            {
                button.GetComponent<UILabel>().fontSize = ButtonNameFontSizeList[i];
            }

            //给按钮添加图集
            button.GetComponent<UISprite>().atlas = ButtonAtlas;
            //给生成按钮添加图片
            if (ButtonPicNameList.Count == ButtonCount)
            {
                button.GetComponent<UISprite>().spriteName = ButtonPicNameList[i];
            }
        }
    }


    public void OnClickEvent(GameObject button)
    {
        if (button.GetComponent<UILabel>().text == "11")
        {
            print("11111");
        }
        else if (button.GetComponent<UILabel>().text == "22")
        {
            print("222222");
        }
        else
        {
            print("3333333");
        }
    }
}