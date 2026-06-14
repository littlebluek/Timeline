using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// FollowTransform Clip。
/// </summary>
[Serializable]
public class FollowTransformClip : BaseClipAsset
{
    public FollowTransformBehaviour template = new FollowTransformBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<FollowTransformBehaviour>.Create(graph, template);
    }
}
