using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameConstants
{
    public static class WaveInfo
    {
        public const float IdleTime = 10f;
        public static readonly Dictionary<int, WaveSpawn> Wave = new Dictionary<int, WaveSpawn>() 
        {
            { 0, new WaveSpawn(2,10,5) },
            { 1, new WaveSpawn(2,2,5) },
            { 2, new WaveSpawn(20,40f,5) },
            { 3, new WaveSpawn(30,40f,5) },
            { 4, new WaveSpawn(35,40f,5) },
            { 5, new WaveSpawn(40,40f,5) },
            { 6, new WaveSpawn(40,60,7) },
            { 7, new WaveSpawn(45,60,8) },
            { 8, new WaveSpawn(45,60,9) },
            { 9, new WaveSpawn(50,60,10) },
            { 10, new WaveSpawn(50,60,10) },
            { 11, new WaveSpawn(100,60,10) }

        };
    }
    public static class Score
    {
        public const string ScoreKey = "Score";
        public const string HighScoreKey = "HighScore";
    }
    public static class Particles
    {
        public static string[] ParticlesNames = new string[]
            {
                "BloodHit",
                "Explosion",
                "MuzzleFlash"
            };
    }
    public static class PickupItem
    {
        public static string[] ConsumablesNames = new string[] 
            { 
                "AmmoPack", 
                "HealthPack", 
                "MoneyPack"
            };
        
        public static Vector2 pickupSpawnMaxPos = new Vector2(66, 100);
        public static Vector2 pickupSpawnMinPos = new Vector2(-10, 50);
    }

    public static class Enemy
    {
        public static string[] EnemiesNames = new string[] 
            { 
                "FastZombie", 
                "TankZombie", 
                "Zombie1", 
                "Zombie2" 
            };
    }

    public static class FunctionNames
    {
        public const string TakeDamage = "TakeDamage";
        public const string DestroySelf = "DestroySelf";
    }

    public static class Player
    {
        public static Vector3 SpawnPlayerPosition = new Vector3(0, 0, 0);
        public const string PlayerPrefabPath = "Player";
    }

    namespace UI
    {
        public static class HUDText
        {
            public const string PlayerHealth = "HEALTH : ";
            public const string Wave = "WAVE : ";
            public const string WaveTime = "TIME : ";
            public const string TimeStringFormat = "0:00";
            public const string Ammo = "AMMO : ";
            public const string Score = "SCORE : ";
            public const string AmmoSlash = " / ";
            public const string RemainsEnemies = "Enemies : ";
        }
    }
}


public class WaveSpawn
{
    public int enemyCount;
    public int pickupCount;
    public float timeWave;

    public WaveSpawn(int count , float time, int pickupItems)
    {
        enemyCount = count;
        timeWave = time;
        pickupCount = pickupItems;
    }
}