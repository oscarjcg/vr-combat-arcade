using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaTexto : MonoBehaviour {
	public VidaJugador vidaJugador;
	public Text texto;
	
	void Start () {
		GameObject jugador = GameObject.Find("Jugador");
		vidaJugador = jugador.GetComponent<VidaJugador>();
		texto = GetComponent<Text>();
	}
	
	
	void Update () {
		actualizarVida();
	}

	void actualizarVida () {
		texto.text = vidaJugador.vida +  "";
	}
}
