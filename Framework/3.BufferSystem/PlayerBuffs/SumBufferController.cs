using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumBufferController : MonoBehaviour {
    public HealthState hs;
    public float[] Timer;
    // Use this for initialization
    public PlayerBufferBase DeBufferBleed(int value, float value2, float continueTime, float Rate, int bufferUI)
    {
        PlayerBufferBase pbb = new SumDeBufferBleed();
        pbb.BufferState = 2;
        pbb.Instance(value, 0, 0, value2, 0, 0, continueTime, Rate, bufferUI,hs);
        if (bufferUI != 0)
        {
            if (Timer[0] == 0)
            {
              
            }
            Timer[0]++;
        }
        return pbb;
    }
}
