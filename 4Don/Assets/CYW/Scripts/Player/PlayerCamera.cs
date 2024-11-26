using System;
using Fusion;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineCamera _camera;

    public static PlayerCamera Instance;

    private void Awake()
    {
        Instance = this;
        _camera = GetComponent<CinemachineCamera>();
    }

    public void SetTarget(Transform target)
    {
        _camera.Target.TrackingTarget = target;
    }
}
