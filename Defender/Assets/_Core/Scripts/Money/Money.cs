using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Money
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private float _timeFly = 1;

        protected Action<Vector3> onFlyMoney;
        private GameStateDirector _gameStateDirector;
        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        public void SetFlyMoney(Action<Vector3> _onFlyMoney) => onFlyMoney = _onFlyMoney;
        
        public void Fly(Vector3 lastPoint, Action calculateCash)
        {
            transform.DOMove(lastPoint, _timeFly)
                .onComplete += () =>
            {
                calculateCash?.Invoke();
                gameObject.SetActive(false);
            };
        }
    }
}

