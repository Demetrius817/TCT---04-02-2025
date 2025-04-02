using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour
{

    [SerializeField]  float PULSE_RANGE = 10.0f;
    [SerializeField]  float PULSE_SPEED = 1.0f;
    [SerializeField]  float PULSE_MINIMUM = 1.0f;
    [SerializeField] Light currentSpellLight;

    void Update()
    {
        currentSpellLight.range = PULSE_MINIMUM +
                          Mathf.PingPong(Time.time * PULSE_SPEED,
                                         PULSE_RANGE - PULSE_MINIMUM);
    }
}
