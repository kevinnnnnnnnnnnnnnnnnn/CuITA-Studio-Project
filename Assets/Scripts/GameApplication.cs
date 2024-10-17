using UnityEngine;

namespace CulTA
{
    public static class GameApplication
    {
        public static BuiltInResources BuiltInResources { get; private set; }
        public static GameObject GlobalGameObject { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Load()
        {
            BuiltInResources = Resources.Load<BuiltInResources>("BuiltInResources");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void PostLoad()
        {
            GlobalGameObject = Object.Instantiate(Resources.Load<GameObject>("GlobalGameObject"));
            Object.DontDestroyOnLoad(GlobalGameObject);
        }
    }
}