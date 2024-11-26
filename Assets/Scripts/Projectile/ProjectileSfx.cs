using System.Collections.Generic;
using UnityEngine;

namespace Defend.Projectile
{
    public class ProjectileSfx : MonoBehaviour
    {
        [SerializeField] protected List<AudioSource> audioSources;

        void Start()
        {
            if (audioSources != null)
            {
                int num = Random.Range(0, audioSources.Count);
                audioSources[num].Play();
            }
        }

        void Update()
        {

        }
    }
}
