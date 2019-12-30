using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour {
	public Transform objetivo;
	public Transform arma;
	public ParticleSystem efectoDisparo;
	public float alcance = 100f;
	public float poderDestructivo = 1.0f;
	public float frecuencia = 1.0f;
    private float precision; // Maxima precision: 100
	private Animator anim;

    private Coroutine disparar;


	void Start () {
		anim = GetComponentInChildren<Animator> ();
		arma = GetComponentInChildren<AttachWeapon>().Weapon;
		objetivo = GameObject.Find ("Jugador").GetComponent<Transform> ();

        precision = 100f;
        
        disparar = null;
	}
	
	
	void Update () {

        if (anim.gameObject.activeSelf)
        {
            
            if (disparar == null)
            {
                disparar = StartCoroutine(Disparar());
            }

            if (!anim.GetBool("muriendo"))
            {
                Vector3 direccion = objetivo.position - transform.position;
                direccion.y = 0;

                transform.rotation = Quaternion.LookRotation(direccion);
            }
            else
            {
                StopCoroutine(disparar);
            }
        }

		//Debug.DrawRay (arma.position, arma.forward * alcance, Color.red);
	}

	IEnumerator Disparar() {
        
        while (!anim.GetBool("muriendo")) {					
			anim.SetBool ("disparando", false);

			yield return new WaitForSeconds (frecuencia);

			anim.SetBool ("disparando", true);
		
			RaycastHit impacto;
			efectoDisparo.Play ();		
			GetComponent<AudioSource> ().Play (); // Sonido de disparo	

			if (Physics.Raycast (arma.position, arma.forward, out impacto, alcance)) {
				 //Debug.Log (impacto.transform.name);
                 
				VidaJugador vida = impacto.transform.GetComponent<VidaJugador> ();
				if (vida != null) {
                   
                    float number = Random.Range(0f, 100f);

                    if(  precision >= number )
                    {                     
                        vida.PerderVida(poderDestructivo);
                    }

                    
				}
			
			}
		}

	}
}
