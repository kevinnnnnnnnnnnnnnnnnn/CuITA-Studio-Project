using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Light
{
    /// <summary>
    /// animated behavior for light, transform.scale will be tweened
    /// </summary>
    public class AnimatedLight : MonoBehaviour
    {
        private float _baseScale = 1f;
        private Coroutine _coroutine;

        private void Start()
        {
            _baseScale = transform.localScale.x;

            // loop animation
            _coroutine = StartCoroutine(Play());
        }

        private void OnDestroy()
        {
            transform.DOKill();
            StopCoroutine(_coroutine);
        }

        private IEnumerator Play()
        {
            transform.DOKill();
            transform.DOScale(_baseScale * 0.8f, 1.5f);

            yield return new WaitForSeconds(Random.Range(0.6f, 2.8f));

            transform.DOKill();
            transform.DOScale(_baseScale * 1f, 1.5f);

            yield return new WaitForSeconds(Random.Range(0.6f, 2.8f));

            _coroutine = StartCoroutine(Play());
        }
    }
}