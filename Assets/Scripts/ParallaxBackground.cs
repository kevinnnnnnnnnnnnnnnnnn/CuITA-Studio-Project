using UnityEngine;

namespace CulTA
{
    /// <summary>
    /// only in x-axis, move in safe area, start from origin position
    /// </summary>
    public class ParallaxBackground : IEntryPoint
    {
        private readonly Transform _origin;
        private readonly Transform _follow;
        public float safeAreaX;

        public ParallaxBackground(EntryPointScheduler scheduler, Transform origin, Transform follow, float safeAreaX = 3f)
        {
            this.safeAreaX = safeAreaX;
            _origin = origin;
            _follow = follow;
            OriginX = _origin.position.x;
            scheduler.AddUpdate(Update);
            scheduler.AddOnDrawGizmosSelected(OnDrawGizmosSelected);
            // scheduler.AddOnGUI(() =>
            // {
            //     GUI.skin.label.fontSize = 30;
            //     GUILayout.Label($"refX: {ReferenceX}, originPos: {_origin.position}, delta:{_delta}, target: {_targetPos}, lerp: {_lerp}");
            // });
        }

        public float OriginX { get; set; }
        public float ReferenceX { get; set; }

        public void RefreshConfiguration(float safeAreaX)
        {
            this.safeAreaX = safeAreaX;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_origin.position, new Vector3(2 * safeAreaX, 0.1f, 0.1f));
        }

        private void Update()
        {
            ReferenceX = _follow.position.x;

            // cal
            var delta = ReferenceX - OriginX;
            var absDelta = Mathf.Abs(delta);
            var deltaSign = Mathf.Sign(delta);
            const float boundMax = 10f;
            var lerp = (boundMax - Mathf.Clamp(absDelta, 0f, boundMax)) / boundMax;

            var targetPos = _origin.position;
            targetPos.x = OriginX + deltaSign * Mathf.Lerp(0, safeAreaX, 1 - lerp);

            _origin.position = targetPos;
        }
    }
}