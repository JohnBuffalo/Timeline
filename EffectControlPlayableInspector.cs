using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace DH.IM
{
    [CustomEditor(typeof(EffectControlPlayable))]
    public class EffectControlPlayableInspector : Editor
    {
        private const string jsonFilePath = "Assets/Editor/TimeLineEdit/Json/";
        private const string luaFilePath = "Assets/Editor/TimeLineEdit/Lua/";

        EffectControlPlayable _target;
        void OnEnable()
        {
            _target = target as EffectControlPlayable;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginVertical();
            _target.setting.skillID = EditorGUILayout.IntField("技能ID:", _target.setting.skillID);
            _target.setting.starFrame = EditorGUILayout.FloatField("启始帧:", _target.setting.starFrame);
            _target.setting.starSeconds = EditorGUILayout.FloatField("启始秒:", _target.setting.starSeconds);
            _target.setting.endFrame = EditorGUILayout.FloatField("结束帧:", _target.setting.endFrame);
            _target.setting.endSeconds = EditorGUILayout.FloatField("结束秒:", _target.setting.endSeconds);
            //EffectType mType = (EffectType)EditorGUILayout.EnumPopup("作用目标:",(EffectType)_target.setting.effectType);
            //_target.setting.effectType = (int)mType;
            _target.setting.effectType = (EffectType)EditorGUILayout.EnumPopup("作用对象:", _target.setting.effectType);
            GameObject obj = AssetDatabase.LoadAssetAtPath(_target.setting.effectPath, typeof(GameObject)) as GameObject;
            obj = EditorGUILayout.ObjectField("特效prefab", obj, typeof(GameObject), false, GUILayout.Width(300)) as GameObject;
            if (obj != null)
            {
                _target.setting.effectPath = AssetDatabase.GetAssetPath(obj);
            }
            else
            {
                _target.setting.effectPath = "";
            }
            if (GUILayout.Button("导出Lua", GUILayout.Width(65), GUILayout.Height(65)))
                ExportToLua(_target.setting);

            GUILayout.EndVertical();
        }

        static void ExportToLua(PlayableSetting setting)
        {
            Debug.Log("ExportToLua");
            string cfgPath = jsonFilePath + setting.skillID + ".json";
            var json = JsonUtility.ToJson(setting);
            byte[] bs = Encoding.UTF8.GetBytes(json);
            Utils.WriteFile(cfgPath, bs);
            CSharpToLuaCfg cfg = new CSharpToLuaCfg();
            cfg.cfgPath = cfgPath;
            cfg.luaFilePath = luaFilePath + setting.skillID + ".lua";
            cfg.typeName = "DH.IM.PlayableSetting";
            JsonToLua.ConvertCfg(cfg);
            AssetDatabase.Refresh();
        }
    }
}