using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using _Core.Scripts.Money;
using _Core.Scripts.Сamera;
using UnityEngine;
using Zenject;

namespace _Core.Scripts
{
    public class UntitledInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Joystick>().FromInstance(FindObjectOfType<Joystick>(true)).AsSingle();
            Container.Bind<CameraDirector>().FromInstance(FindObjectOfType<CameraDirector>(true)).AsSingle();
            Container.Bind<Player.Player>().FromInstance(FindObjectOfType<Player.Player>(true)).AsSingle();
            Container.Bind<GameStateDirector>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle();
            Container.Bind<MoneyController>().AsSingle();

            CreateEnemy();
            string MoneyUIVariant = "MoneyUI Variant";

            Container.BindFactory<MoneyUI, MoneyUI.MoneyInject>()
                .FromComponentInNewPrefabResource(MoneyUIVariant);
            
            string MoneyGroundVariant = "MoneyGround Variant";

            Container.BindFactory<MoneyGround, MoneyGround.MoneyInject>()
                .FromComponentInNewPrefabResource(MoneyGroundVariant);
        }
        private void CreateEnemy()
        {
            string NAME_0_ENEMY = "0_Enemy_Variant";

            Container.BindFactory<EnemyInject_0, EnemyInject_0.FactoryEnemyInject>()
                .FromComponentInNewPrefabResource(NAME_0_ENEMY);
        }
    }

}

