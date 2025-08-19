using System;
using UnityEngine;


//Main namespace of the game
namespace RPG.Example
{
    //The Robot class stores the data of each robot
    public class RobotOne:MonoBehaviour
    {
        ///* 
        // Variables
        //===============
        // */
        //public int age = 5;
        //public float price = 99.99f;
        ////private string name = "McBot";
        //public bool isTurnedOn = false;

        ///*
        // Constructor
        //===============
        //*/
        //public Robot()
        //{
        //    //isTurnedOn=true;
        //    float newPrice = CalculatePrice(0.7f, 1);

        //    if(newPrice > 75f)
        //    {
        //        price = newPrice;
        //    }
        //    else
        //    {
        //        //print("Price is too low.");
        //        //Debug.Log("Price is too low.");
        //        //Debug.LogWarning("Price is too low.");
        //        //Debug.LogError("Price is too low.");
        //        Log("Price is too low.");
        //        Log(age);
        //        Log(price);
        //        Log(isTurnedOn);
        //    }
        //}

        //[Obsolete("")]
        ////calculates the price after applying a discount
        //public float CalculatePrice(float discount, int quantity)
        //{
        //    //Apply a 10% discount to the price and return it
        //    return (price - (price * discount)) * quantity;

        //}

        ////applies discount to price
        //public float ApplyDiscount(float discount)
        //{
        //    //Apply a 10% discount to the price and return it
        //    return (price - (price * discount));
        //}

        ////Logs a message to the console
        //public void Log<T>(T message)
        //{
        //    Debug.Log(message);
        //}
    }
}
