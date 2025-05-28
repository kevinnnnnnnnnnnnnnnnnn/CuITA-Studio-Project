using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CulTA
{
    public class MenuGlobalLight : MonoBehaviour
    {
        public static MenuGlobalLight instance;
        
        public Light2D globalLight;

        public float lightIntensity;
        public float lightDuration;


        private void Awake()
        {
            instance = this;
        }

        public void SetMenuLightIntensity()
        {
            StartCoroutine(MenuLightUp());
        }


        IEnumerator MenuLightUp()
        {
            float speed = Mathf.Abs((globalLight.intensity - lightIntensity) / lightDuration);

            while (!Mathf.Approximately(globalLight.intensity, lightIntensity))
            {
                globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, lightIntensity, speed * Time.deltaTime);
                yield return null;
            }
        }

    }
}