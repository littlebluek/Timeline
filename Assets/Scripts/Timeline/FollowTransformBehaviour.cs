using System;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// FollowTransform 的 Behaviour，在 ProcessFrame 中跟随目标位置/旋转。
/// 使用 info.weight 做加权 Lerp，与重叠 Clip 形成近似混合。
/// </summary>
[Serializable]
public class FollowTransformBehaviour : BaseClipBehaviour
{
    [Tooltip("要跟随的目标物体名称")]
    public string targetName;

    [Tooltip("相对于目标的位置偏移（本地坐标系）")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("是否跟随目标位置")]
    public bool followPosition = true;

    [Tooltip("是否跟随目标旋转")]
    public bool followRotation = true;

    private GameObject targetObj;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!string.IsNullOrEmpty(targetName))
            targetObj = GameObject.Find(targetName);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        targetObj = null;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Transform boundTransform = playerData as Transform;
        if (boundTransform == null || targetObj == null)
            return;

        float weight = info.weight;
        if (weight <= 0f)
            return;

        Transform targetTransform = targetObj.transform;

        if (followPosition)
        {
            Vector3 targetPos = targetTransform.position + targetTransform.TransformDirection(positionOffset);
            boundTransform.position = Vector3.Lerp(boundTransform.position, targetPos, weight);
        }

        if (followRotation)
        {
            boundTransform.rotation = Quaternion.Slerp(boundTransform.rotation, targetTransform.rotation, weight);
        }
    }
}
