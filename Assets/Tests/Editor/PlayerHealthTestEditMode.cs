using System.Collections;
using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class PlayerHealthPlayModeTests
    {
        private GameObject _playerObj;
        private PlayerHealth _health;
        private PlayerStats _stats;
        private UnityEngine.UI.Slider _slider;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            _playerObj = new GameObject("PlayerTest");

            _stats = _playerObj.AddComponent<PlayerStats>();
            _health = _playerObj.AddComponent<PlayerHealth>();

            var sliderObj = new GameObject("SliderMock");
            _slider = sliderObj.AddComponent<UnityEngine.UI.Slider>();
            _health.healthSlider = _slider;

            yield return null; // ждём 1 кадр, чтобы Unity создала компоненты
            
            _health.Start();
        }

        [UnityTest]
        public IEnumerator Player_TakesDamage_InPlayMode()
        {
            float startHp = _stats.health;

            _health.TakeDamage(15);

            yield return null;

            Assert.AreEqual(startHp - 15, _stats.health);
            Assert.AreEqual(_stats.health, _health.healthSlider.value);
        }
    }
}