using UnityEngine;
using Object = UnityEngine.Object;

namespace CulTA
{
    public class CameraParallaxBg : MonoBehaviour
    {
        public float safeAreaX = 2f;
        private ParallaxBackground _parallaxBackground;

        private void Awake()
        {
            var cam = Camera.main.transform;
            _parallaxBackground = new ParallaxBackground(EntryPointScheduler.Create(gameObject), transform, cam, safeAreaX);
            _parallaxBackground.Forget();
        }

        private void Update()
        {
            _parallaxBackground.RefreshConfiguration(safeAreaX);
        }
    }
}