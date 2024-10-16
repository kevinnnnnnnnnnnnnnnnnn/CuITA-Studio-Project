using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class TestScriptGameScene
{
    private static WaitForSeconds WaitStep { get; } = new(0.8f);

    private bool _isEnd;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/Editor/TestRootScene.unity",
            new LoadSceneParameters());

        var behaviour = new GameObject("TestScriptRootBehaviour").AddComponent<TestScriptRootBehaviour>(); // add from outer
        behaviour.OnEndTest += () => _isEnd = true;

        yield return null;
    }

    [Test]
    public void TestLoadRoot()
    {
        // do nothing
    }

    [UnityTest]
    public IEnumerator Door()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/DoorOpen.prefab");
        var instance = Object.Instantiate(prefab);
        var doorOpen = instance.GetComponent<DoorOpen>();
        doorOpen.enabled = false;

        yield return WaitStep;
        Debug.Log("test door open");

        doorOpen.OpenDoor();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape) || _isEnd);
    }
}

public class TestScriptRootBehaviour : MonoBehaviour
{
    public event Action OnEndTest;

    private void OnGUI()
    {
        GUI.skin.button.fontSize = 30;
        if (GUILayout.Button("EndTest"))
        {
            OnEndTest?.Invoke();
        }
    }
}
