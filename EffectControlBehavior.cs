using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEditor;

namespace DH.IM
{
    [Serializable]
    public class EffectControlBehavior : PlayableBehaviour
    {
        [NonSerialized]
        public string path;
        public EffectType eType = EffectType.SELF;
        bool firstFrameHappend = false;
        GameObject effect;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (path == null) return;
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab == null)
                return;

            GameObject target = GameObject.Find("TargetPos");

            if (eType == EffectType.SELF || eType == EffectType.NONE)
                target = GameObject.Find("SourcePos");
            else if (eType == EffectType.TARGET)
                target = GameObject.Find("TargetPos");


            effect = UnityEngine.Object.Instantiate<GameObject>(prefab, target.transform);
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (effect)
            {
                UnityEngine.Object.DestroyImmediate(effect.gameObject);
                firstFrameHappend = false;
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (effect)
            {
                UnityEngine.Object.DestroyImmediate(effect.gameObject);
                firstFrameHappend = false;
            }
        }

    }
}
