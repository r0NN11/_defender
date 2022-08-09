using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Weapon.Bullet;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Core.Scripts.Weapon
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [SerializeField] private AbstractBullet _abstractBullet;
        [SerializeField] private Transform _pointSpawnBullet;
        [SerializeField] private Transform _pointDistanceCheck;
        [SerializeField] protected float _timeUpdateShot = 0.1f;

        protected GameStateDirector _gameStateDirector;
        protected BulletPoolManager _bulletPoolManager;
        protected Transform _transformAimAbstractEnemy;
        protected const string LAYER_ENEMY = "Enemy";

        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        private void Awake()
        {
            _bulletPoolManager = new BulletPoolManager(_abstractBullet);
        }

        public void SetAimAbstractEnemy(Transform transform) => _transformAimAbstractEnemy = transform;

        public virtual void Shot()
        {
            var abstractBullet = _bulletPoolManager.GetBullet();

            abstractBullet.transform.position = _pointSpawnBullet.position;
            abstractBullet.transform.forward = _pointSpawnBullet.forward;

            abstractBullet.MoveBullet();
        }

        public virtual void StarShootingWeapon()
        {
            StopAllCoroutines();
            StartCoroutine(Shooting());
        }

        public virtual void StopShootingWeapon()
        {
            StopAllCoroutines();
        }

        protected virtual IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeUpdateShot);

                if (_transformAimAbstractEnemy == null)
                    continue;
                if (Physics.Raycast(_pointSpawnBullet.position, _pointSpawnBullet.forward, 1000,
                        LayerMask.GetMask(LAYER_ENEMY)))
                {
                    if (_gameStateDirector.GetCurrentGameState() == GameState.StartBattle||_gameStateDirector.GetCurrentGameState() == GameState.UpdateCountEnemy)
                        Shot();
                }
            }
        }


        public sealed class BulletPoolManager
        {
            private const int COUNT_BULLET_START_POOL_MANAGER = 50;

            private readonly List<AbstractBullet> _abstractBullets;
            private readonly AbstractBullet _abstractBullet;


            public BulletPoolManager(AbstractBullet abstractBullet)
            {
                _abstractBullet = abstractBullet;
                _abstractBullets = new List<AbstractBullet>(COUNT_BULLET_START_POOL_MANAGER);

                BulletCreator(COUNT_BULLET_START_POOL_MANAGER);
            }

            public AbstractBullet GetBullet()
            {
                for (var i = 0; i < _abstractBullets.Count; i++)
                {
                    if (!_abstractBullets[i].gameObject.activeSelf)
                    {
                        return _abstractBullets[i];
                    }
                }

                BulletCreator(COUNT_BULLET_START_POOL_MANAGER / 2);
                return GetBullet();
            }

            private void BulletCreator(int countBullet)
            {
                while (countBullet > 0)
                {
                    _abstractBullets.Add(Object.Instantiate(_abstractBullet));
                    _abstractBullets[_abstractBullets.Count - 1].gameObject.SetActive(false);
                    countBullet--;
                }
            }
        }
    }
}