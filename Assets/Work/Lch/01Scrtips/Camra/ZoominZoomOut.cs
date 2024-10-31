using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoominZoomOut : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // �ó׸ӽ� ���� ī�޶� ����
    public float zoomSpeed = 10f; // �� �ӵ�
    public float minFOV = 15f; // �ּ� �� (����)
    public float maxFOV = 60f; // �ִ� �� (�ܾƿ�)

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // ���콺 ��ũ�� �Է� �ޱ�
        if (scrollInput != 0f)
        {
            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            currentFOV -= scrollInput * zoomSpeed;

            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        }
    }
}
