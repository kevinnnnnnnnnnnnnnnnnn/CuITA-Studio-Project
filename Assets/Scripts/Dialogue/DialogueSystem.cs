using System.Collections;
using System.Collections.Generic;
using System.IO;
using CulTA;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TextMeshProUGUI textLable;
    
    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    
    
    [Header("文本播放相关")]
    List<string>textList = new List<string>();
    public float textSpeed;
    
    
    [SerializeField]bool textFinished;//本行文本是否结束
    [SerializeField]bool cancelTyping;//是否取消输入文本
    
    
    public bool isInOverGameNPC;
    
    public Button leaveDreamButton;
    public Button exitDreamButton;
    
    
    private void Awake()
    {
        GetTextFromFile(textFile);       
    }
    
    
    private void OnEnable()
    {
        textFinished = true;             
        StartCoroutine(SetTextUI());
    }
    
    
    private void Update()
    {
        if (isInOverGameNPC && index >= textList.Count)
        {
            if (leaveDreamButton != null && exitDreamButton != null)
            {
                leaveDreamButton.gameObject.SetActive(true);
                exitDreamButton.gameObject.SetActive(true);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = true;
            TimeLineManager.instance.menuButton.gameObject.SetActive(true);
            
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        
        if((Input.GetKeyDown(KeyCode.E) || AndroidInputButton.instance.isClickedInteractiveButton) && index == textList.Count)
        {
            AndroidInputButton.instance.isClickedInteractiveButton = false;
            
            TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = true;
            TimeLineManager.instance.menuButton.gameObject.SetActive(true);
            
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.E) || AndroidInputButton.instance.isClickedInteractiveButton)
        {
            AndroidInputButton.instance.isClickedInteractiveButton = false;
            
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
    void GetTextFromFile(TextAsset file)
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
        TransitionManager.instance.player.GetComponent<PlayerMove>().enabled = false;
        TransitionManager.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        
        textFinished = false;
        textLable.text = "";
        
        int letter = 0;//当前行文本中已显示的文字个数
        
        //使当前行文本逐字输出
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
            if (textList[index][letter] == ' ')
            {
                textLable.text += GameManager.instance.playerName;
                letter++;
                
                yield return new WaitForSeconds(textSpeed);
                continue;
            }
            
            textLable.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        
        //含空格则输出PlayerName,无空格则为NPC名字
        if (textList[index][0] == ' ')
        {
            while (letter < textList[index].Length)
            {
                if (textList[index][letter] == ' ')
                {
                    textLable.text += GameManager.instance.playerName;
                    letter++;
                    
                    continue;
                }
            
                textLable.text += textList[index][letter];
                letter++;
            }
        }
        else
            textLable.text = textList[index];
        
        cancelTyping = false;
        textFinished = true;
        index++;
    }

}
