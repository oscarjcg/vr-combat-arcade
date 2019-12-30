using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textoVida : MonoBehaviour {
	public Text texto;
	public VidaJugador jugador;
    
	void Start () {
		GameObject vidaTexto = GameObject.Find("VidaTexto");
		texto = vidaTexto.GetComponent<Text>();
		jugador = GameObject.Find("Jugador").GetComponent<VidaJugador>();

	}
	
	
	void Update () {
		actualizarVida();
		
	}

	void actualizarVida () {
		texto.text = "VIDA: " + jugador.vida;
	}
}
