using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Сamera
{
    public class CameraDirector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _currentCinemachineVirtualCamera;
        private GameStateDirector _gameStateDirector;
        private Player.Player _player;
        private Transform _mainFollow;
        private Vector3 _offset;
        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }
        private void Start()
        {
            _mainFollow = _currentCinemachineVirtualCamera.Follow;
        }
        public Camera GetCamera() => _camera;

    }
}

