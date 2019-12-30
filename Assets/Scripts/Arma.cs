using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

	public float alcance = 100f;
	public float poderDestructivo = 10f;
    public int cargadorCapacidad = 20;
    public int municion = 20;
    public float tiempoRecarga = 3f;
	public Camera jugador;
	public ParticleSystem efectoDisparo;
	public ParticleSystem efectoDisparoTraza;

    private bool recargando;

    private AudioSource audioRecarga;
    private Animator anim;

   
    void Start () {
		ControladorJuego.DisparoIzq += Disparar;
        ControladorJuego.Recarga += Recargar;
        recargando = false;

        audioRecarga = GameObject.Find("AudioRecarga").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
	
	
	void Disparar() {
        if((municion > 0) && !recargando)
        {
            RaycastHit impacto;
            efectoDisparo.Play(); 
            efectoDisparoTraza.Play();

            GetComponent<AudioSource>().Play(); // Sonido de disparo	

            if (Physics.Raycast(jugador.transform.position, jugador.transform.forward, out impacto, alcance))
            {
                           
                Vida vida = impacto.transform.GetComponent<Vida>();
                if (vida != null)
                {
                    vida.PerderVida(poderDestructivo);
                }
            }

            municion--;
        }		
        else
        {
            Recargar();           
        }        

	}

    void Recargar()
    {
        if (!recargando)
            StartCoroutine(RecargarCorrutina());
    }

    IEnumerator RecargarCorrutina()
    {
        float desfase = 0.3f;

        audioRecarga.Play();

        recargando = true;
        anim.SetBool("recargando", true);

        yield return new WaitForSeconds(tiempoRecarga - desfase);
        anim.SetBool("recargando", false);

        yield return new WaitForSeconds(desfase);

        municion = cargadorCapacidad;        
        recargando = false;
        
    }
}
