using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// 播放 AnimationClip 的 Clip，不依赖 Unity 原生 AnimationTrack。
/// </summary>
[Serializable]
public class PlayAnimationClip : BaseClipAsset
{
    [Tooltip("要播放的 AnimationClip")]
    public AnimationClip clip;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PlayAnimationBehaviour>.Create(graph);
        playable.GetBehaviour().clip = clip;
        return playable;
    }
}
