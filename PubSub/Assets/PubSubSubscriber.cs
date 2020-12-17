using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PubSubSubscriber : MonoBehaviour
{
    public string EventName; 
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public BoolEvent OnBooleanMessageRecieved;
    private void Awake()
    {
        PubSubBroker.Instance.Subscribe(EventName, MessageRecieved); 
    } 
    private void OnDestroy()
    {
        PubSubBroker.Instance.Unsubscribe(EventName, MessageRecieved);
    }
    public void MessageRecieved(PubSubBroker.PubSubMessage message)
    {
        OnBooleanMessageRecieved.Invoke(message.GetValue<bool>());
    }
}
