using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        var light = new GameObject("PlayerLightManager").AddComponent<PlayerLightManager>();
        light.playerLight = new GameObject("PlayerLight", typeof(Light2D)); // shit

        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/DoorOpen.prefab");
        var instance = Object.Instantiate(prefab);
        var doorOpen = instance.GetComponent<DoorOpen>();
        instance.GetComponent<DoorOpen>().enabled = false;

        var transition = instance.transform.GetChild(0).GetComponent<Transition>();
        Object.Destroy(transition); // shit

        yield return WaitStep;

        doorOpen.OpenDirectly();

        yield return WaitStep;

        var player = new GameObject("Player", typeof(BoxCollider2D), typeof(Rigidbody2D));
        var playerRb = player.GetComponent<Rigidbody2D>();
        player.tag = "Player";
        player.transform.position = doorOpen.transform.position; // collider enter

        playerRb.bodyType = RigidbodyType2D.Kinematic;
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
