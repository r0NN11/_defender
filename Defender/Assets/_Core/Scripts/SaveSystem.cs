using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts
{
    public class SaveSystem : MonoBehaviour
    {
        public Action OnUpdate;
        [SerializeField] private SaveData _saveData;

        private static SaveSystem _instance;

        public static SaveSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SaveSystem>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("SaveSystem");
                        _instance = container.AddComponent<SaveSystem>();
                    }
                }

                return _instance;
            }
        }
        private void Awake()
        {
            _saveData = new SaveData();
            _saveData.Init();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        public SaveData GetSaveData() => _saveData;
        public void SaveGame() => _saveData.Save();
    }

    [System.Serializable]
    public class SaveData
    {
        public void Init()
        {
            countMoney = PlayerPrefs.GetInt("countMoney", 0);
        }

        public void Save()
        {
            PlayerPrefs.SetInt("countMoney", countMoney);
        }
        
        public int countMoney = 0;

    }
}

