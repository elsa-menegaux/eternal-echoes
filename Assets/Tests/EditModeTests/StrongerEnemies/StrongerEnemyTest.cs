using System;
using System.IO;
using NUnit.Framework;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.TestTools;

public class StrongerEnemiesTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void StrongerEnemies_ShouldHaveHigherStats()
    {
        // Create GameObjects to attach the EnemyStats to
        GameObject weakEnemyObject = new GameObject("WeakEnemy");
        GameObject strongEnemyObject = new GameObject("StrongEnemy");
        GameObject mediumEnemyObject = new GameObject("MediumEnemy");

        // Use AddComponent to add EnemyStats component
        EnemyStats weakEnemy = weakEnemyObject.AddComponent<EnemyStats>();
        EnemyStats strongEnemy = strongEnemyObject.AddComponent<EnemyStats>();
        EnemyStats mediumEnemy = mediumEnemyObject.AddComponent<EnemyStats>();
        
        //weak enemy with a low room count scaling
        int WeakEnemyRoom = 1;
        weakEnemy.Name = "Weak Enemy";
        weakEnemy.scaleModifier = 1;
        weakEnemy.ScaleStats(WeakEnemyRoom);

        //strong enemy with high room count scaling
        int StrongEnemyRoom = 10;
        strongEnemy.Name = "Strong Enemy";
        strongEnemy.scaleModifier = 1f;
        strongEnemy.ScaleStats(StrongEnemyRoom);

        //very strong enemy with low room count scaling
        int MediumEnemyRoom = 3; 
        mediumEnemy.Name = "Medium Enemy";
        mediumEnemy.scaleModifier = 2f;
        mediumEnemy.ScaleStats(MediumEnemyRoom);

        // Act: Calculate damage for both enemies
        float weakEnemyDamage = weakEnemy.currentDamage;
        float strongEnemyDamage = strongEnemy.currentDamage;
        float mediumEnemyDamage = mediumEnemy.currentDamage;

        // Assert: The strong enemy should deal more damage the stronger it is
        //Assert.That(weakEnemyDamage, Is.GreaterThan(0.0f), "Weak enemy damage should be greater than 0");
        //Assert.That(strongEnemyDamage, Is.GreaterThan(0.0f), "Strong enemy damage should be greater than 0");
        //Assert.That(mediumEnemyDamage, Is.GreaterThan(0.0f), "Medium enemy damage should be greater than 0");
        Assert.That(strongEnemy.currentDamage, Is.GreaterThan(weakEnemy.currentDamage), "Strong enemy should deal more damage than weak enemy");
        Assert.That(strongEnemy.currentDamage, Is.GreaterThan(mediumEnemy.currentDamage), "Strong enemy should deal more damage than medium enemy");
        Assert.That(mediumEnemy.currentDamage, Is.GreaterThan(weakEnemy.currentDamage), "Medium enemy should deal more damage than weak enemy");

        // Clean up after test
        UnityEngine.Object.DestroyImmediate(weakEnemyObject);
        UnityEngine.Object.DestroyImmediate(strongEnemyObject);
        UnityEngine.Object.DestroyImmediate(mediumEnemyObject);
    }
}