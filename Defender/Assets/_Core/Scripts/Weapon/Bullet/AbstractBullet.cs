using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Weapon.Bullet
{
    public abstract class AbstractBullet : MonoBehaviour
    {
        [SerializeField] private float _distanceBullet = 10;
        [SerializeField] private float _speedBullet = 50;
        [SerializeField] private float _damage = 1;
        
        private Vector3 _startPoint;
        private void OnEnable()
        {
            _startPoint = transform.position;
        }
        private void Update()
        {
            var speedBullet = transform.forward * Time.deltaTime * _speedBullet;
            transform.position += speedBullet;    
            
            if(Vector3.Distance(_startPoint, transform.position) > _distanceBullet)
                DeactivateBullet();
        }
        public virtual void MoveBullet()
        {
            gameObject.SetActive(true);
        }
        public virtual float GetDamage() => _damage;
        
        public virtual void DeactivateBullet()
        {
            gameObject.SetActive(false);
        }
    }
}

