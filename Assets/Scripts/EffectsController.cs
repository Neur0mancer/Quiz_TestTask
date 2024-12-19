using UnityEngine;

namespace Quiz_Bezuglyi
{
    public class EffectsController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _particlesEffect;

        public void PlayEffectOnTransform(Transform transform)
        {
            GameObject effect = Instantiate(_particlesEffect, transform);
            ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
                Destroy(effect, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
            }
        }
    }
}