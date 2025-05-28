using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetPlayer;
    public new Camera camera;

    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraOffsetY;


    private void LateUpdate()
    {
        CameraFollowUpdate();
    }


    private void CameraFollowUpdate()
    {
        Vector3 position = targetPlayer.position;
        Vector3 targetPosition = new Vector3(position.x, position.y + cameraOffsetY, -10f);
        
        camera.transform.position =
            Vector3.Lerp(camera.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
