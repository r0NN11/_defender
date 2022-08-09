using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.UI
{
    public abstract class CanvasAbstract : MonoBehaviour
    {
        protected GameStateDirector _gameStateDirector;
        
        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }
        
        protected abstract void ChangeGameState(GameState gameState);
    }
}

