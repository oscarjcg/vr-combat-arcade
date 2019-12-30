using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour {

	public float vida = 100f;
    

	public void PerderVida(float cantidad) {
		
		if (vida <= 0f) {           
            vida = 0f;            			
		}
        else
        {
            vida -= cantidad;
        }
	}
}
