using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Money
{
    public class MoneyGround : Money
    {
        [SerializeField] private float _timeFlyMoney = 3;

        private void OnEnable()
        {
            StartCoroutine(DelayFly());
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent(out Player.Player player))
                return;
            
            CalculateFlyMoney();
        }

        private void CalculateFlyMoney()
        {
            onFlyMoney?.Invoke(transform.position);
            gameObject.SetActive(false);
        }

        private IEnumerator DelayFly()
        {
            
            yield return new WaitForSeconds(_timeFlyMoney);
            CalculateFlyMoney();
        }

        public class MoneyInject : PlaceholderFactory<MoneyGround>
        {
        }
    }
}

