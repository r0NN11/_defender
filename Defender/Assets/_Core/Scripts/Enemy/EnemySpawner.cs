using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _levelEnemyCount1stWave = 15;
        [SerializeField] private int _levelEnemyCountNextWave = 5;
        [SerializeField] private int _levelEnemyMinCount = 10;
        [SerializeField] private Vector3 _boundsSize = new Vector3(70, 0, 70);
        [SerializeField] private Vector3 _centerPoint = new Vector3(0, 0, 20);
        [Inject] private EnemyInject_0.FactoryEnemyInject factoryEnemyInject_0;

        private List<EnemyInject_0> _enemyInject_0;
        private Bounds _bounds;
        private EnemyController _enemyController;
        private GameStateDirector _gameStateDirector;

        [Inject]
        public void Constructor(EnemyController enemyController, GameStateDirector gameStateDirector)
        {
            _enemyController = enemyController;
            _gameStateDirector = gameStateDirector;
        }

        public EnemySpawner()
        {
            _enemyInject_0 = new List<EnemyInject_0>();
        }

        private void OnEnable()
        {
            _bounds = new Bounds(_centerPoint, _boundsSize);
        }

        private void Start()
        {
            CreateEnemy(_levelEnemyCount1stWave);
        }

        private void Update()
        {
            if (_enemyController.GetCountEnemyActivate() < _levelEnemyMinCount)
            {
                CreateEnemy(_levelEnemyCountNextWave);
            }
        }

        private void CreateEnemy(int enemyCount)
        {
            _enemyInject_0.Clear();
            for (var i = 0; i < enemyCount; i++)
            {
                _enemyInject_0.Add(factoryEnemyInject_0.Create());
                var tempEnemyInject = _enemyInject_0[i].transform;
                tempEnemyInject.position = GetRandomSpawnPosition();
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var xPos = (Random.value) * _bounds.size.x;
            var yPos = (Random.value) * _bounds.size.y;
            var zPos = (Random.value) * _bounds.size.z;
            return new Vector3(xPos, yPos, zPos);
        }
    }
}