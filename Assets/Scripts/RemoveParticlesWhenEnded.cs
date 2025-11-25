using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParticlesWhenEnded : MonoBehaviour
{
    ParticleSystem _ps;
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_ps.isStopped && _ps.particleCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
