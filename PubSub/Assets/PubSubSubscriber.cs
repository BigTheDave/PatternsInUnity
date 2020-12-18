using com.clydebuiltgames.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PubSubSubscriber : MonoBehaviour
{
    public string EventName;
    public ValidMessageTypes ExpectedType = ValidMessageTypes.BOOLEAN;
    public UnityEvent OnMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public BoolEvent BOOLEANMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public IntEvent INTMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public FloatEvent FLOATMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public StringEvent STRINGMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public GameObjectEvent GAMEOBJECTMessageRecieved;
    [HideInInspector]
    public TransformEvent TRANSFORMMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public ColourEvent COLORMessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public ColourEvent VECTOR2MessageRecieved;
    [HideInInspector]
    [InspectorName("Message Recieved")]
    public ColourEvent VECTOR3MessageRecieved;
    [Flags]
    public enum ValidMessageTypes
    {
        BOOLEAN=        0b000000001,
        INT =           0b000000010,
        FLOAT =         0b000000100,
        STRING =        0b000001000,
        GAMEOBJECT =    0b000010000,
        TRANSFORM =     0b000100000,
        VECTOR2 =       0b001000000,
        VECTOR3 =       0b010000000,
        COLOR =         0b100000000
    }
    private void Awake()
    {
        PubSubBroker.Instance.Subscribe(EventName, MessageRecieved); 
    } 
    private void OnDestroy()
    {
        PubSubBroker.Instance.Unsubscribe(EventName, MessageRecieved);
    }
    public void TriggerEvent<T>(PubSubBroker.PubSubMessage message, ValidMessageTypes messageType, UnityEvent<T> trigger)
    {
        if (!ExpectedType.HasFlag(messageType)) return;
        if (!message.Is<T>()) return;
        trigger.Invoke(message.GetValue<T>());
    }
    public void MessageRecieved(PubSubBroker.PubSubMessage message)
    {
        OnMessageRecieved.Invoke();
        TriggerEvent(message, ValidMessageTypes.BOOLEAN, BOOLEANMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.INT, INTMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.FLOAT, FLOATMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.STRING, STRINGMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.TRANSFORM, TRANSFORMMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.GAMEOBJECT, GAMEOBJECTMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.COLOR, COLORMessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.VECTOR2, VECTOR2MessageRecieved); 
        TriggerEvent(message, ValidMessageTypes.VECTOR3, VECTOR3MessageRecieved); 
    }
}
