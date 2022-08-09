using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Player;
using UnityEngine;

namespace _Core.Scripts.Gates
{
    public class GatesController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _radiusDetect = 10;
        [SerializeField] private SphereCollider _sphereCollider;
        
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int Close = Animator.StringToHash("Close");
        private void Start()
        {
            _sphereCollider.radius = _radiusDetect;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player _player))
            {
                _animator.SetBool(Close, false);
                _animator.SetBool(Open, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player _player))
            {
                _animator.SetBool(Open, false);
                _animator.SetBool(Close, true);
            }
        }
    }
}

