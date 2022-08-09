using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using _Core.Scripts.Weapon;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speedMove;
        [SerializeField] private float _speedRotation = 15;
        [SerializeField] private Collider _collider;
        [SerializeField] private AbstractWeapon _abstractWeapon;

        private static readonly int Move = Animator.StringToHash("Move");

        private Joystick _joystick;
        private Vector3 _directionJoystickNormalize;
        private GameStateDirector _gameStateDirector;
        private Animator _animator;
        private int _health;
        private Transform _abstractEnemyAim;
        
        [Inject]
        public void Constructor(Joystick joystick, GameStateDirector gameStateDirector)
        {
            _joystick = joystick;
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _abstractWeapon.StarShootingWeapon();
            _health = 100;
            _gameStateDirector.SetGameState(GameState.StartGame);
        }

        private void FixedUpdate()
        {
            Vector3 tempForward;
            if (_joystick.Direction == Vector2.zero|| !_joystick.gameObject.activeSelf)
            {
                _animator.SetBool(Move, false);
                _rigidbody.velocity = Vector3.zero;
                if (_abstractEnemyAim != null)
                {
                    RotationToAbstractEnemy(out tempForward);
                    LerpRotation(tempForward);
                }

                return;
            }
            _animator.SetBool(Move, true);

            var joystickDirection = _joystick.Direction.normalized;
            _directionJoystickNormalize = new Vector3(joystickDirection.x, 0, joystickDirection.y);

            _rigidbody.velocity = _directionJoystickNormalize * _speedMove;
            
            if (_abstractEnemyAim == null)
            {
                tempForward = _directionJoystickNormalize;
            }
            else
            {
                RotationToAbstractEnemy(out tempForward);
            }
            LerpRotation(tempForward);
        }
        public void SetAimAbstractEnemy(Transform abstractEnemy)
        {
            _abstractWeapon.SetAimAbstractEnemy(abstractEnemy);
            _abstractEnemyAim = abstractEnemy;
        }
        public void SetNullAimAbstractEnemy()
        {
            _abstractWeapon.SetAimAbstractEnemy(null);
            _abstractEnemyAim = null;
        }
        public void ReduceHealth(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _gameStateDirector.SetGameState(GameState.Loss);
                Destroy(gameObject);
            }
            _gameStateDirector.SetGameState(GameState.CollisionPlayer);
        }
        public int GetCurrentHealth() => _health;
        private void RotationToAbstractEnemy(out Vector3 forward)
        {
            var transformPosition = -(transform.position - _abstractEnemyAim.position);
            transformPosition = new Vector3(transformPosition.x, 0, transformPosition.z);
            LerpRotation(transformPosition);
            forward = transformPosition;
        }
        private void LerpRotation(Vector3 forward) =>
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward, Vector3.up),
                _speedRotation * Time.deltaTime);
        private void OnCollisionExit(Collision other)
        {
            if (other.collider.TryGetComponent(out Enemy.AbstractEnemy enemy))
                _gameStateDirector.SetGameState(GameState.StartBattle);
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.WinGame:
                    break;
            }
        }
    }
}