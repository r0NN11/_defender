using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using _Core.Scripts.Money;
using _Core.Scripts.Сamera;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.UI
{
    public class CanvasMoney : CanvasAbstract
    {
     [SerializeField] private int _minCountEnemyActivateMoney;
        [SerializeField] private int _maxCountEnemyActivateMoney;

        [SerializeField] private int _minCountMoney;
        [SerializeField] private int _maxCountMoney;

        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private Transform _cash;

        private EnemyController _enemyController;
        private MoneyController _moneyController;
        private SaveSystem _saveSystem;
        private Camera _camera;
        private Vector3 _startScale;
        private Tween _tweenScale;
        private int _countEnemyLastActivateMoney;
        private int _randomCountEnemyMoney;
        private int _randomMoney;

        [Inject]
        private void Construct(EnemyController enemyController, MoneyController moneyController,
            CameraDirector cameraDirector)
        {
            _enemyController = enemyController;
            _moneyController = moneyController;
            _camera = cameraDirector.GetCamera();
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    _saveSystem = SaveSystem.Instance;
                    gameObject.SetActive(true);

                    UpdateTextMoney();
                    CalculateRandom();

                    _startScale = _cash.localScale;
                    
                    break;

                case GameState.UpdateCountEnemy:

                    if (!gameObject.activeSelf)
                        return;

                    if ((_enemyController.GetRemoveAbstractEnemy() - _countEnemyLastActivateMoney) >=
                        _randomCountEnemyMoney)
                    {
                        _countEnemyLastActivateMoney += _enemyController.GetRemoveAbstractEnemy();

                        MoneyActivate();
                        CalculateRandom();
                    }

                    break;
            }
        }

        private void CalculateRandom()
        {
            _randomCountEnemyMoney = Random.Range(_minCountEnemyActivateMoney, _maxCountEnemyActivateMoney);
            _randomMoney = Random.Range(_minCountMoney, _maxCountMoney);
        }

        private void MoneyActivate()
        {
            var currentEnemyKilled = _enemyController.GetEnemyKilled();

            while (_randomMoney > 0)
            {
                var money = _moneyController.GetMoneyGround();

                var position = currentEnemyKilled.position;

                var positionEnemy = new Vector3(position.x + Random.Range(-0.5f, 0.5f),
                    position.y + Random.Range(0.2f, 1),
                    position.z + Random.Range(-0.5f, 0.5f));
                
                money.transform.position = positionEnemy;
                money.gameObject.SetActive(true);

                money.SetFlyMoney(CreateUIMoney);
                
                _randomMoney--;
            }
        }

        private void CalculateCash()
        {
            _saveSystem.GetSaveData().countMoney++;
            _saveSystem.SaveGame();
            
            UpdateTextMoney();
            
            _tweenScale?.Kill();

            _tweenScale = _cash.DOScale(_startScale * 1.3f, 0.05f);
            _tweenScale.onComplete += () => _cash.DOScale(_startScale, 0.05f);
        }

        private void UpdateTextMoney()
        {
            _textMeshProUGUI.text = $"{_saveSystem.GetSaveData().countMoney}";
        }

        private void CreateUIMoney(Vector3 position)
        {
            var money = _moneyController.GetMoneyUI();

            money.transform.position = _camera.WorldToScreenPoint(position);

            money.transform.SetParent(transform);
            money.gameObject.SetActive(true);
            money.Fly(_cash.position, CalculateCash);
        }
    }
}

