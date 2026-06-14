using UnityEngine;
using UnityEngine.Timeline;

/// <summary>
/// 通用轨道，只接受 BaseClipAsset 类型的 Clip。
/// 绑定类型为 Transform。
/// </summary>
[TrackColor(0.4f, 0.4f, 0.4f)]
[TrackBindingType(typeof(Transform))]
[TrackClipType(typeof(BaseClipAsset))]
public class UniversalTrack : TrackAsset
{
}
