using TMPro;
using UnityEngine;
using Task = System.Threading.Tasks.Task;


/// <summary>
/// 挂载在Text上
/// </summary>
public class LoadTextUI : MonoBehaviour
{
    public TextMeshProUGUI loadText;

    private void Start()
    {
        LoopLoadText();
    }

    
    /// <summary>
    /// 循环加载文本
    /// </summary>
    public async void LoopLoadText()
    {
        loadText.text = "加载中";

        while (true)
        {
            await Task.Delay(500);

            loadText.text += ".";

            if (loadText.text == "加载中.....")
                loadText.text = "加载中";
        }
        
    }
}
