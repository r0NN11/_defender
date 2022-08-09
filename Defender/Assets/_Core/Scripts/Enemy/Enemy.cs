using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Weapon.Bullet;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    public class Enemy : AbstractEnemy
    {
        [SerializeField] private Animator _animator;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private bool _isMove;

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.StartBattle:
                    MoveToPlayer();
                    break;
                case GameState.StopBattle:
                    StopMoveToPlayer();
                    break;
                case GameState.WinGame:
                    break;
                case GameState.UpdateCountEnemy:
                    break;
            }
        }

        protected override void CollisionBullet(AbstractBullet abstractBullet)
        {
            base.CollisionBullet(abstractBullet);
        }

        private void Update()
        {
            EnemyMove();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            if (_gameStateDirector.GetCurrentGameState() == GameState.UpdateCountEnemy)
                MoveToPlayer();
        }

        public override void EnemyMove()
        {
            if (!_isMove || _player == null)
            {
                _animator.SetBool(Walk, false);
                return;
            }
                

            var transformPosition = transform.position - _player.transform.position;
            transformPosition = -transformPosition.normalized;
            _rigidbody.velocity = (transformPosition * _speed);
            transform.forward = new Vector3(transformPosition.x, 0, transformPosition.z);
        }

        public override void MoveToPlayer()
        {
            _animator.SetBool(Walk, true);
            _isMove = true;
        }
        public override void StopMoveToPlayer()
        {
            _animator.SetBool(Walk, false);
            _isMove = false;
        }
    }
}