using UnityEngine;

namespace Player.Data
{
    [System.Serializable]
    public class DieSettings
    {
        public float dieSpeed = 0.7f;
        public ParticleSystem deathParticles;
    }
}