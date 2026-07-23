using System;
using UnityEngine;

//1 tick = 1/20 second = 0.05 second

public class Timer : MonoBehaviour
{
    public float tick = 0.05f;

    public static event Action OnTick;

    public void Tick()
    {
        OnTick?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        tick -= Time.deltaTime;

        if (tick <= 0)
        {
            tick = 0.05f;
            Tick();
        }
    }
}
