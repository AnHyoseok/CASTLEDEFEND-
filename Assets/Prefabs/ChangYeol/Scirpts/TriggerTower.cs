using Defend.Projectile;
using UnityEngine;

namespace Defend.UI
{
    public class TriggerTower : MonoBehaviour
    {
        #region Variables
        public MeshRenderer mrenderer;
        #endregion
        private void Start()
        {
            mrenderer.material.color = Color.yellow;
        }
    }
}