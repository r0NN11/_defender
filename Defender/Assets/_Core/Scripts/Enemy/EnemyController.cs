using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    public class EnemyController
    {
        private GameStateDirector _gameStateDirector;
        private Transform _enemyKilled;
        private List<AbstractEnemy> _abstractEnemies;
        private int _countEnemyActivate;
        private int _countAbstractEnemyRemove;
        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }
        public EnemyController()
        {
            _abstractEnemies = new List<AbstractEnemy>();
        }
        public Transform GetEnemyKilled() => _enemyKilled;
        
        public void AddAbstractEnemy(AbstractEnemy abstractEnemy)
        {
            if (!_abstractEnemies.Contains(abstractEnemy))
                _abstractEnemies.Add(abstractEnemy);
            _countEnemyActivate++;
        }
        public void RemoveAbstractEnemy(AbstractEnemy abstractEnemy)
        {
            _enemyKilled = abstractEnemy.transform;

            _countAbstractEnemyRemove++;
            
            if (_countEnemyActivate - _countAbstractEnemyRemove <= 0)
            {
                _gameStateDirector.SetGameState(GameState.WinGame);
            }
            _gameStateDirector.SetGameState(GameState.UpdateCountEnemy);
        }
        public int GetRemoveAbstractEnemy() => _countAbstractEnemyRemove;
        public int GetCountEnemyActivate() => _countEnemyActivate - _countAbstractEnemyRemove;
    }   
}

