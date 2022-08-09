using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Money
{
    public class MoneyUI : Money
    {   
        public class MoneyInject : PlaceholderFactory<MoneyUI> { }
    }
}

