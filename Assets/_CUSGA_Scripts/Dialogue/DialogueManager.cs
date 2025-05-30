using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    [Header("UI组件")]
    public TextMeshProUGUI textLable;
    public GameObject talkPanel;
    //public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;

    [Header("人物头像")]
    public Sprite face01;
    public Sprite face02;

    
    [Header("文本播放相关")]
    List<string>textList = new List<string>();
    public float textSpeed;

    [SerializeField]bool textFinished;//本行文本是否结束
    [SerializeField]bool cancelTyping;//是否取消输入文本


    private void Awake()
    {
        //GetTextFromFile(textFile);       
        
        if(instance != null)
            Destroy(instance);
        instance = this;
    }

    private void OnEnable()
    {   
        textFinished = true;
        //StartCoroutine(SetTextUI());
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && talkPanel.activeSelf)
        {
            talkPanel.SetActive(false);
            index = 0;
            return;
        }

        if(Input.GetKeyDown(KeyCode.E) && index == textList.Count && talkPanel.activeSelf)
        {
            Debug.Log("close Panel");
            talkPanel.SetActive(false);
            index = 0;
            return;
        }

        if(Input.GetKeyDown(KeyCode.E) && talkPanel.activeSelf)
        {
            if(textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }




    /// <summary>
    /// 将textFile的文本分行切割到textList中
    /// </summary>
    /// <param name="file"></param>
    public void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }


    /// <summary>
    /// 逐行逐字输出文本
    /// </summary>
    /// <returns></returns>
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLable.text = "";

        switch(textList[index])
        {
            case "A\r":
                //faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                //faceImage.sprite = face02;
                index++;
                break;
        }

        int letter = 0;//当前行文本中已显示的文字个数
        
        //使当前行文本逐字输出
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
            textLable.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLable.text = textList[index];//cancelTyping为true不进入循环，取消逐字输出，直接输出全部文字
        
        cancelTyping = false;
        textFinished = true;
        index++;
    }



    public void GetTextFiel(TextAsset file)
    {
        textFile = file;
    }

}
