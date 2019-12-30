using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {

	public float vida = 20;
    private Animator anim;
    private float tiempoMuerte = 10f;

   
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	
	public void PerderVida(float cantidad) {
		vida -= cantidad;
		if ((vida <= 0f) && !anim.GetBool("muriendo")) {
            StartCoroutine(Morir());
            
            GameObject controlador = GameObject.Find("Controlador");
            controlador.GetComponent<ControladorJuego>().reducirEnemigos();           
        }
	}

    IEnumerator Morir()
    {
        anim.SetBool("muriendo", true);
        Object.Instantiate(this.transform);
        yield return new WaitForSeconds(tiempoMuerte);

        anim.SetBool("muriendo", false);
        gameObject.transform.parent.gameObject.SetActive(false);     

    }
}
