using UnityEngine;

/// <summary>
/// 实时读取另一个物体的 Transform 位置、旋转，叠加位置偏移后设置到自己身上。
/// 挂载到跟随物体上，在 Update 中每帧执行。使用 ExecuteAlways 确保编辑器下也生效。
/// </summary>
[ExecuteAlways]
public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    [Tooltip("要跟随的目标物体")]
    private Transform m_Target;

    [SerializeField]
    [Tooltip("相对于目标的位置偏移（本地坐标系）")]
    private Vector3 m_PositionOffset = Vector3.zero;

    [SerializeField]
    [Tooltip("是否跟随目标旋转")]
    private bool m_FollowRotation = true;

    [SerializeField]
    [Tooltip("是否跟随目标位置")]
    private bool m_FollowPosition = true;

    [SerializeField]
    [Tooltip("位置跟随的平滑速度，0 表示瞬间跟随")]
    private float m_PositionSmoothSpeed = 0f;

    [SerializeField]
    [Tooltip("旋转跟随的平滑速度，0 表示瞬间跟随")]
    private float m_RotationSmoothSpeed = 0f;

    private void LateUpdate()
    {
        Follow();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // 编辑器中修改参数后立即刷新
        if (!Application.isPlaying)
            Follow();
    }
#endif

    private void Follow()
    {
        if (m_Target == null)
            return;

        // 计算目标位置 + 偏移
        Vector3 targetPosition = m_Target.position + m_Target.TransformDirection(m_PositionOffset);

        if (m_FollowPosition)
        {
            if (m_PositionSmoothSpeed > 0f)
                transform.position = Vector3.Lerp(transform.position, targetPosition, m_PositionSmoothSpeed * Time.deltaTime);
            else
                transform.position = targetPosition;
        }

        if (m_FollowRotation)
        {
            Quaternion targetRotation = m_Target.rotation;

            if (m_RotationSmoothSpeed > 0f)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSmoothSpeed * Time.deltaTime);
            else
                transform.rotation = targetRotation;
        }
    }
}
