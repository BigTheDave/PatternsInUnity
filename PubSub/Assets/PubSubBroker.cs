using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PubSubBroker : MonoBehaviour
{
    private static PubSubBroker _instance;
    public static PubSubBroker Instance => Equals(_instance, null) ? (_instance = CreateNewBroker()) : _instance;
    private static PubSubBroker CreateNewBroker()
    {
        var gameObject = new GameObject("PubSubBroker");
        return gameObject.AddComponent<PubSubBroker>();
    } 
    public struct PubSubMessage
    {
        public string EventName;
        public object Sender;
        public bool HasValue;
        public object Value;
        public bool Is<T>()
        {
            if (!HasValue) return false;
            return (Value is T);
        }

        public T GetValue<T>()
        { 
            if (Is<T>())
            {
                return (T)Value;
            }
            return default(T);
        }
        public Component GetSenderAsComponent()
        { 
            if (Sender is Component) return (Component)Sender;
            return null;
        }
    }
    public string[] EventNames => EventLibrary.Keys.ToArray();
    public List<Action<PubSubMessage>> GetSubscribers(string eventName)
    {
        return EventLibrary[eventName];
    }
    private Dictionary<string, List<Action<PubSubMessage>>> EventLibrary = new Dictionary<string, List<Action<PubSubMessage>>>();
    public void Subscribe(string EventName, Action<PubSubMessage> Callback)
    {
        if(!EventLibrary.ContainsKey(EventName))
        {
            EventLibrary.Add(EventName, new List<Action<PubSubMessage>>());
        }
        EventLibrary[EventName].Add(Callback);
    }
    public void Unsubscribe(string EventName, Action<PubSubMessage> Callback)
    {
        if (!EventLibrary.ContainsKey(EventName))
        {
            return;
        }
        EventLibrary[EventName].Remove(Callback);
    }
    private void PublishMessage(PubSubMessage message)
    {
        var EventName = message.EventName;
        if (!EventLibrary.ContainsKey(EventName))
        {
            Debug.LogWarning($"PubSubBroker: Event Name '{EventName}' not found");
            return;
        }
        foreach (var callback in EventLibrary[EventName])
        {
            try
            {
                callback.Invoke(message);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

    }
    public void Publish(string EventName, object sender)
    {
        var message = new PubSubMessage()
        {
            EventName = EventName,
            Sender = sender,
            HasValue = false,
            Value = null
        };
        PublishMessage(message);
    }
    public void Publish<T>(string EventName, object sender, T value)
    { 
        var message = new PubSubMessage()
        {
            EventName = EventName,
            Sender = sender,
            HasValue = true,
            Value = value
        };
        PublishMessage(message);
    }
}
