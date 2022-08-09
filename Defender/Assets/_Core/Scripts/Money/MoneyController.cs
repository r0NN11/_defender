using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Money
{
    public class MoneyController
    {
        [Inject] private MoneyUI.MoneyInject _moneyUIInject;
        [Inject] private MoneyGround.MoneyInject _moneyGroundInject;

        private List<Money> _listMoneyUI;
        private List<Money> _listMoneyGround;

        public Money GetMoneyUI()
        {
            if (_listMoneyUI == null)
                _listMoneyUI = new List<Money>();

            Money money = null;

            for (var i = 0; i < _listMoneyUI.Count; i++)
            {
                if (_listMoneyUI[i].gameObject.activeSelf)
                    continue;

                money = _listMoneyUI[i];
            }

            if (money == null)
            {
                _listMoneyUI.Add(_moneyUIInject.Create());

                money =
                    _listMoneyUI[_listMoneyUI.Count - 1];
            }

            return money;
        }
        
        public Money GetMoneyGround()
        {
            if (_listMoneyGround == null)
                _listMoneyGround = new List<Money>();

            Money money = null;

            for (var i = 0; i < _listMoneyGround.Count; i++)
            {
                if (_listMoneyGround[i].gameObject.activeSelf)
                    continue;

                money = _listMoneyGround[i];
            }

            if (money == null)
            {
                _listMoneyGround.Add(_moneyGroundInject.Create());

                money =
                    _listMoneyGround[_listMoneyGround.Count - 1];
            }

            return money;
        }

    }
}

