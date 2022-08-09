using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Core.Scripts.UI
{
    public class CanvasLoss : CanvasAbstract
    {
        [SerializeField] private GameObject _gameObjectTextLevel;
        [SerializeField] private float _timeScaleGameObject;
        
        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Loss:
                    ShowCanvasLoss();
                    break;
            }
        }
        private void ShowCanvasLoss()
        {
            gameObject.SetActive(true);
            _gameObjectTextLevel.transform.DOScale(Vector3.one, _timeScaleGameObject).From(Vector3.zero);
        }
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    } 
}

