using System;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 通过 AnimationClip.SampleAnimation 播放动画。
/// 使用 info.weight 做加权 Lerp，与重叠 Clip 形成近似混合。
/// </summary>
[Serializable]
public class PlayAnimationBehaviour : BaseClipBehaviour
{
    public AnimationClip clip;

    private Animator animator;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (clip == null)
            return;

        float weight = info.weight;
        if (weight <= 0f)
            return;

        Transform target = playerData as Transform;
        if (target == null)
            return;

        if (animator == null)
            animator = target.GetComponent<Animator>();
        if (animator == null)
            return;

        double time = playable.GetTime();
        double duration = playable.GetDuration();
        float normalizedTime = duration > 0 ? (float)(time / duration) : 0f;
        float clipTime = normalizedTime * clip.length;

        // 采样前保存当前位置
        Vector3 prevPos = target.position;
        Quaternion prevRot = target.rotation;

        clip.SampleAnimation(animator.gameObject, clipTime);
        animator.Update(0f);

        // Lerp 混合：weight=1 时完全用动画位置，weight 降低时保留原位置
        target.position = Vector3.Lerp(prevPos, target.position, weight);
        target.rotation = Quaternion.Slerp(prevRot, target.rotation, weight);
    }
}
