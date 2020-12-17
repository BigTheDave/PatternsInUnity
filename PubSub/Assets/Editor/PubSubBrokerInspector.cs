using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PubSubBroker))]
public class PubSubBrokerInspector : Editor
{
    PubSubBroker broker;
    private void OnEnable()
    {
        broker = (PubSubBroker)target;
    }
    private Dictionary<string, bool> Foldouts = new Dictionary<string, bool>();
    private bool GetFoldout(string name)
    {
        return Foldouts.ContainsKey(name) ? Foldouts[name] : false;
    }
    private void SetFoldout(string name, bool b)
    {
        if (!Foldouts.ContainsKey(name)) Foldouts.Add(name, b);
        else Foldouts[name] = b;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Event Names", GUI.skin.box, GUILayout.ExpandWidth(true));
        foreach (var eventName in broker.EventNames)
        {
            SetFoldout(eventName, EditorGUILayout.BeginFoldoutHeaderGroup(GetFoldout(eventName),eventName));
            if(GetFoldout(eventName))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(16);
                GUILayout.BeginVertical();
                foreach(var subscriber in broker.GetSubscribers(eventName))
                {
                    if (subscriber?.Target is Object)
                    {
                        if (GUILayout.Button($"{subscriber?.Target}"))
                        {
                            EditorGUIUtility.PingObject((Object)subscriber.Target);
                        }
                    }
                     else
                    {
                        GUILayout.Label($"{subscriber?.Target}");
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
    }
}
