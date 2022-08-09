using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts
{
    public class CalculateNextGameState : MonoBehaviour
    {
        private GameStateDirector _gameStateDirector;
        private GameState _battlestate;
        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Player.Player _player))
                SetBattleState(_battlestate);
        }
        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    _battlestate = GameState.StartGame;
                    break;
                case GameState.StartBattle:
                    _battlestate = GameState.StartBattle;
                    break;
                case GameState.StopBattle:
                    _battlestate = GameState.StopBattle;
                    break;
                case GameState.WinGame:
                    break;
            }
        }
        private void SetBattleState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    _battlestate = GameState.StartBattle;
                    break;
                case GameState.StartBattle:
                    _battlestate = GameState.StopBattle;
                    break;
                case GameState.StopBattle:
                    _battlestate = GameState.StartBattle;
                    break;
                case GameState.WinGame:
                    break;
                default:
                    _battlestate = GameState.StartBattle;
                    break;
            }
            _gameStateDirector.SetGameState(_battlestate);
        }
    }
}
