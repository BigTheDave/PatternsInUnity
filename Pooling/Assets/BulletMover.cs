using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float Speed = 5;
    public float Lifetime = 1;
    private float mLifetime = 0;
    private void OnEnable()
    {
        mLifetime = Lifetime;
    }
    private void FixedUpdate()
    {
        mLifetime -= Time.deltaTime;
        if (mLifetime <= 0) this.gameObject.SetActive(false);
        this.transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
    }
}
