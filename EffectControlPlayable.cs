using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;

namespace DH.IM
{
    /// <summary>
    /// 作用类型
    /// </summary>
    [Serializable]
    public enum EffectType
    {
        NONE,
        TARGET,
        SELF
    }

    [Serializable]
    public class PlayableSetting
    {
        public int skillID = 1;
        public float starFrame = 0;
        public float starSeconds = 0;
        public float endFrame = 0;
        public float endSeconds = 0;
        public string effectPath = "";
        public EffectType effectType = EffectType.TARGET;
    }

    [Serializable]
    public class EffectControlPlayable : PlayableAsset
    {
        [SerializeField]
        public PlayableSetting setting = new PlayableSetting();

        EffectControlBehavior template = new EffectControlBehavior();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            template.path = setting.effectPath;
            template.eType = setting.effectType;
            return ScriptPlayable<EffectControlBehavior>.Create(graph, template);
        }

    }

    
}