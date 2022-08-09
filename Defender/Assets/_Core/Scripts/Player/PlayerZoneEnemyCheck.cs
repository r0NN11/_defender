using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Player
{
    public class PlayerZoneEnemyCheck : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private float _radiusDetect;
        [SerializeField] private float _timeCheckEnemy = 0.5f;

        [SerializeField] private AbstractEnemy _abstractEnemy;
        private GameStateDirector _gameStateDirector;
        private List<AbstractEnemy> _abstractEnemies;

        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        private void Start()
        {
            _sphereCollider.radius = _radiusDetect;
            _abstractEnemies = new List<AbstractEnemy>();
            StartCoroutine(CalculateShotEnemy());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AbstractEnemy _enemy))
            {
                var abstractEnemy = _enemy;
                var abstractEnemies = _abstractEnemies;
                abstractEnemies.Add(abstractEnemy);
            }
        }

        private IEnumerator CalculateShotEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeCheckEnemy);

                float tempMinDistance = _radiusDetect;
                bool isEnemyCheck = false;

                for (var i = 0; i < _abstractEnemies.Count; i++)
                {
                    if (!_abstractEnemies[i].gameObject.activeSelf)
                        continue;

                    var distance = Vector3.Distance(transform.position, _abstractEnemies[i].transform.position);

                    if (_abstractEnemy != null)
                    {
                        if (_abstractEnemy == _abstractEnemies[i])
                            continue;
                    }

                    if (distance < tempMinDistance)
                    {
                        isEnemyCheck = true;
                        tempMinDistance = distance;
                        _abstractEnemy = _abstractEnemies[i];
                    }
                }

                if (!isEnemyCheck)
                    _abstractEnemy = null;

                if (_abstractEnemy != null)
                {
                    _player.SetAimAbstractEnemy(_abstractEnemy.transform);
                    
                }
                else
                {
                    _player.SetNullAimAbstractEnemy();
                }
            }
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.WinGame:
                    break;
                case GameState.Loss:
                    StopAllCoroutines();
                    break;
                case GameState.UpdateCountEnemy:
                    break;
            }
        }
    }
}