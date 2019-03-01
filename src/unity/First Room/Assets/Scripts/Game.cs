using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
   public static Game current;
   public Animal cat;

   public Game () {
       cat = new Animal();
   }
}
