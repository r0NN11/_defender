using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Enemy
{
    public class EnemyZonePlayerCheck : MonoBehaviour
    {
        [SerializeField] private float _radiusDetect = 10;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private AbstractEnemy _abstractEnemy;
       
        private void Start()
        {
            _sphereCollider.radius = _radiusDetect;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _abstractEnemy.MoveToPlayer();
            }
        }
                
    }
}

