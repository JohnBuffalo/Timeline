using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace DH.IM
{
    //[TrackBindingType(typeof(GameObject))]
    [TrackClipType(typeof(EffectControlPlayable))]
    public class EffectTrackAsset : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            timelineAsset.editorSettings.fps = 30;
            foreach (var clip in GetClips())
            {
                var mAsset = clip.asset as EffectControlPlayable;
                if (mAsset)
                {
                    mAsset.setting.starFrame = (float)clip.start * timelineAsset.editorSettings.fps;
                    mAsset.setting.endFrame = (float)clip.end * timelineAsset.editorSettings.fps;
                    mAsset.setting.starSeconds = (float)clip.start;
                    mAsset.setting.endSeconds = (float)clip.end;
                }
            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}