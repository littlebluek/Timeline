using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// 通用 Clip Asset 基类（非泛型）。
/// 所有自定义 Clip 继承此类，覆写 CreatePlayable 返回对应 ScriptPlayable。
/// </summary>
[Serializable]
public abstract class BaseClipAsset : PlayableAsset, ITimelineClipAsset
{
    public virtual ClipCaps clipCaps => ClipCaps.Blending;
}
