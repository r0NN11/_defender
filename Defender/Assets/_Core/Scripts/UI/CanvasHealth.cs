using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Core.Scripts.UI
{
    public class CanvasHealth : CanvasAbstract
    {
        [SerializeField] private Image _fillingHealth;
        [SerializeField] private TextMeshProUGUI _textMeshHealth;
        private Player.Player _player;

        [Inject]
        public void Constructor(Player.Player player)
        {
            _player = player;
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.CollisionPlayer:
                    ImageFilling();
                    break;
                case GameState.StartGame:
                    gameObject.SetActive(true);
                    ImageFilling();
                    break;
            }
        }

        private void ImageFilling()
        {
            var health = _player.GetCurrentHealth();
            _fillingHealth.fillAmount = health/ 100f;
            _textMeshHealth.text = $" {health}  HP";
        }
    }
}