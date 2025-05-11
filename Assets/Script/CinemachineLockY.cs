using UnityEngine;
using Cinemachine;

[ExecuteAlways]
[SaveDuringPlay]
[AddComponentMenu("Cinemachine/Extensions/Lock Camera Y Position")]
public class CinemachineLockY : CinemachineExtension
{
    public float fixedY = 10f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.RawPosition;
            pos.y = fixedY;
            state.RawPosition = pos;
        }
    }
}
