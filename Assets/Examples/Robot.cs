using System;
using UnityEngine;


//Main namespace of the game
namespace RPG.Example
{
    //The Robot class stores the data of each robot
    public class Robot : MonoBehaviour
    {
        private BatteryRegulation includeBattery;

        public Robot()
        {
            includeBattery = new Battery(80f);
            includeBattery.CheckHealth();
            Charger.ChargeBattery(includeBattery);
            includeBattery.CheckHealth();
            print(Charger.chargerInUse);
        }
    }

    public class Battery : BatteryRegulation
    {
        public Battery(float newHealth) : base(newHealth) { }
        public override void CheckHealth()
        {
            Debug.Log(health);
        }
    }

    static class Charger
    {
        public static bool chargerInUse = false;

        public static void ChargeBattery(BatteryRegulation batteryToCharge)
        {
            chargerInUse = true;
            batteryToCharge.health = 100f;
        }
    }

    public abstract class BatteryRegulation
    {
        public float health;
        public BatteryRegulation(float newHealth)
        {
            health = newHealth;
            Debug.Log("New Battery Created!");
        }
        public abstract void CheckHealth();
    }
}
