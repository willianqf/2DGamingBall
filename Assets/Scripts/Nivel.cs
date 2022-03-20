using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nivel : MonoBehaviour
{
   [SerializeField]
   Sprite[] image = new Sprite[15];

   Image actualimage;
   int nivel = 0;

    void Start()
    {
        actualimage = GetComponent<Image>();
    }
   public void decrement()
   {
       nivel--;
       actualimage.sprite = image[nivel];
   }
   public void increment()
   {
       nivel++;
       actualimage.sprite = image[nivel];
   }
   public void alternivel(int Nivel)
   {
       actualimage.sprite = image[Nivel - 1];
   }

}

