using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animal
{
    public string name;
    public int happiness;
    public int age;
    public int hunger;

    public Animal () {
        this.name = "Foxy";
        this.happiness = 50;
        this.age = 0;
        this.hunger = 0;
    }
}
