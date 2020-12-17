using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubSubPublisher : MonoBehaviour
{
    public string EventName;
    public void PublishBoolean(bool value)
    {
        PubSubBroker.Instance.Publish(EventName, this, value);
    } 
}
