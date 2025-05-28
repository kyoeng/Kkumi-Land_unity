using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;     // 카메라
    
    private bool _isMove = false;                   // 드래그 플래그 값
    private Vector3 _lastMousePosition;             // 이전 마우스 위치 저장용





    private void Update()
    {
        Movement();
    }





    private void Movement()
    {
        // 마우스 좌우 모두 클릭 중인 경우
        if (Input.GetMouseButton(0) && !Input.GetButton("RotOn"))
        {
            if (!_isMove)
            {
                _isMove = true;
                _lastMousePosition = Input.mousePosition;
            }
            else
            {
                Vector3 currentMousePosition = Input.mousePosition;
                Vector3 delta = currentMousePosition - _lastMousePosition;

                Vector3 cameraF = _camera.forward.normalized;
                Vector3 cameraR = _camera.right.normalized;

                cameraF.y = 0;
                cameraR.y = 0;

                transform.position -= (cameraR * delta.x + cameraF * delta.y) * 0.03f;
                _lastMousePosition = currentMousePosition;
            }
        }
        // 아닌 경우
        else
        {
            _isMove = false;
        }
    }
}
