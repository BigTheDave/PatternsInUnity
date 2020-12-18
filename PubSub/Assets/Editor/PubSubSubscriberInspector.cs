using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PubSubSubscriber))]
public class PubSubSubscriberInspector : Editor
{
    SerializedProperty prop_expectedTypes;
    private void OnEnable()
    {
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); 
        prop_expectedTypes = serializedObject.FindProperty("ExpectedType");
        var flags = prop_expectedTypes.intValue; 
        foreach(var flagName in prop_expectedTypes.enumDisplayNames)
        {
            if(Enum.TryParse<PubSubSubscriber.ValidMessageTypes>(flagName, out var flagValue))
            {
                if ((flags & (int)flagValue) == 0) continue; 
                var prop_messageEvent = serializedObject.FindProperty($"{flagName}MessageRecieved");
                EditorGUILayout.PropertyField(prop_messageEvent);
            } 
        } serializedObject.ApplyModifiedProperties();
    }
}
