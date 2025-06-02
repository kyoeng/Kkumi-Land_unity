using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;         // 카메라가 볼 타겟
    [SerializeField] private Transform _camera;         // 카메라
    [SerializeField] private float _followSpeed;        // 따라가는 속도
    [SerializeField] private float _sensitivity;        // 감도
    [SerializeField] private float _minDistance;        // 최소 거리
    [SerializeField] private float _maxDistance;        // 최대 거리
    [SerializeField] private float _smoothness;         // 부드러움

    private Vector3 _normalizedDir;     // 카메라 방향
    private Vector3 _finalDir;          // 최종 방향
    private float _applyDistance;       // 적용되고 있는 거리
    private float _clampAngle = 70f;    // 제한 각도
    private float _rotateX;
    private float _rotateY;





    private void Awake()
    {
        _normalizedDir = _camera.localPosition.normalized;
        _applyDistance = _camera.localPosition.magnitude;
    }

    private void Update()
    {
        Rotation();
        Movement();
        ZoomInOut();
    }





    private void Rotation()
    {
        if (Input.GetMouseButton(0) && Input.GetButton("RotOn"))
        {
            _rotateX += -Input.GetAxisRaw("Mouse Y") * _sensitivity * Time.deltaTime;
            _rotateY += Input.GetAxisRaw("Mouse X") * _sensitivity * Time.deltaTime;
            _rotateX = Mathf.Clamp(_rotateX, -_clampAngle, _clampAngle);

            Quaternion rotation = Quaternion.Euler(_rotateX, _rotateY, 0);
            transform.rotation = rotation;
        }
    }


    private void Movement()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            _followSpeed * Time.deltaTime
        );

        _finalDir = transform.TransformPoint(_normalizedDir * _applyDistance);

        _camera.localPosition = Vector3.Lerp(
            _camera.localPosition,
            _normalizedDir * _applyDistance,
            Time.deltaTime * _smoothness
        );
    }


    private void ZoomInOut()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput != 0)
        {
            _applyDistance -= wheelInput * _sensitivity * Time.deltaTime;
            _applyDistance = Mathf.Clamp(_applyDistance, _minDistance, _maxDistance);
        }
    }
}
