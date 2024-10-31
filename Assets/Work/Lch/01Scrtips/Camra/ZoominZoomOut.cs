using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoominZoomOut : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 시네머신 가상 카메라 참조
    public float zoomSpeed = 10f; // 줌 속도
    public float minFOV = 15f; // 최소 줌 (줌인)
    public float maxFOV = 60f; // 최대 줌 (줌아웃)

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // 마우스 스크롤 입력 받기
        if (scrollInput != 0f)
        {
            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            currentFOV -= scrollInput * zoomSpeed;

            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        }
    }
}
