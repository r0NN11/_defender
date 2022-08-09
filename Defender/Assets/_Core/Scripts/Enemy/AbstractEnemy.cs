using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Weapon.Bullet;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Bullet = Zenject.SpaceFighter.Bullet;

namespace _Core.Scripts.Enemy
{
    public class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected float _speed = 10;
        protected int _damage = 10;
        protected GameStateDirector _gameStateDirector;
        protected Player.Player _player;
        protected EnemyController _enemyController;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector, Player.Player player,
            EnemyController enemyController)
        {
            _gameStateDirector = gameStateDirector;
            _player = player;
            _enemyController = enemyController;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        public virtual void Start()
        {
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AbstractBullet bullet))
                CollisionBullet(bullet);
        }

        public virtual void OnEnable()
        {
            _enemyController.AddAbstractEnemy(this);
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Player.Player player))
                CollisionPlayer();
        }
        

        public virtual void EnemyMove() { }
        protected virtual void OnDestroy()
        {
        }

        protected virtual void CollisionPlayer()
        {
            _player.ReduceHealth(_damage);
        }

        protected virtual void CollisionBullet(AbstractBullet abstractBullet)
        {
            abstractBullet.DeactivateBullet();
            DisableEnemy();
        }
        public virtual void MoveToPlayer() {}
        public virtual void StopMoveToPlayer() {}
        public virtual int GetDamage() => _damage;
        protected virtual void DisableEnemy()
        {
            _enemyController.RemoveAbstractEnemy(this);
            gameObject.SetActive(false);
            _gameStateDirector.OnChangeGameState -= ChangeGameState;
        }
        
        protected virtual void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.WinGame:
                    break;
                case GameState.UpdateCountEnemy:
                    break;
            }
        }
    }
}