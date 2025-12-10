using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTest
{
    private GameObject playerObj;
    private PlayerHealth health;
    private PlayerStats stats;
    private Slider slider;

    [SetUp]
    public void Setup()
    {
        playerObj = new GameObject("PlayerTest");

        stats = playerObj.AddComponent<PlayerStats>();
        health = playerObj.AddComponent<PlayerHealth>();

        // ---- НУЖНО СОЗДАТЬ MOCK SLIDER ----
        var sliderObj = new GameObject("SliderMock");
        slider = sliderObj.AddComponent<Slider>();
        health.healthSlider = slider;

        // Теперь Start() безопасно
        health.Start();
    }

    [Test]
    public void Player_TakesDamage_Correctly()
    {
        float initialHP = stats.health;

        health.TakeDamage(25);

        Assert.AreEqual(initialHP - 25, stats.health);
        Assert.AreEqual(stats.health, slider.value);
    }

    [Test]
    public void Player_Dies_AtZeroHP()
    {
        health.TakeDamage(999);

        Assert.AreEqual(0, stats.health);
        Assert.IsFalse(playerObj.activeSelf);
    }
}