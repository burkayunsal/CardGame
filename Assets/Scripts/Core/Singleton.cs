using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   public static T I;

   public virtual void Awake()
   {
      I = this as T;
   }
}
