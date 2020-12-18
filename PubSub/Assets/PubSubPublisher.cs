using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubSubPublisher : MonoBehaviour
{
    public string EventName;
    public void Publish()
    {
        PubSubBroker.Instance.Publish(EventName, this);
    }
    public void Publish<T>(T value)
    {
        PubSubBroker.Instance.Publish(EventName, this, value);
    }
    public void PublishBoolean(bool value) { Publish(value); }
    public void PublishString(string value) { Publish(value); }
    public void PublishInt(int value) { Publish(value); }
    public void PublishFloat(float value) { Publish(value); }
    public void PublishTransform(Transform value) { Publish(value); }
    public void PublishGameObject(GameObject value) { Publish(value); }
    public void PublishVector2(Vector2 value) { Publish(value); }
    public void PublishVector3(Vector3 value) { Publish(value); }
    public void PublishColor(Color value) { Publish(value); }
}
