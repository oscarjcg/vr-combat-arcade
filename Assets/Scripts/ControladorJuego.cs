using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void metodoDelegado();

public class ControladorJuego : MonoBehaviour {
	public static ControladorJuego controlador;
    
	public static event metodoDelegado DisparoIzq;
    public static event metodoDelegado Recarga;

    private GameObject plataforma;
	private GameObject enemigos;
	private GameObject interfaz;
	private GameObject puntuacion;
	private int localizacion;
	private bool cubierto;
	private Vector3 actualPosicion;

    private GameObject menu;

    public int numeroEnemigos;
    private GameObject[] zonas;

    
    void Awake() {
		if (controlador == null) {
			controlador = this;
			DontDestroyOnLoad (this);

		} else {
			if (controlador != this) {
				Destroy (gameObject);
			}
		}
	}

	
	void Start () {
		plataforma = GameObject.Find ("Plataforma");
		interfaz = GameObject.Find("CanvasInterfaz");
		puntuacion = GameObject.Find("Puntuacion");
		enemigos = GameObject.Find ("Enemigos");
        menu = GameObject.Find("Canvas");

        // Zonas con enemigos
        int nZonas = enemigos.GetComponent<Transform>().childCount;
        zonas = new GameObject[nZonas];

        for(int i = 0; i < nZonas; i++)
        {
            Transform zona = enemigos.GetComponent<Transform>().GetChild(i);
            zonas[i] = zona.gameObject;
            zonas[i].SetActive(false);           
        }

        localizacion = 0;
		cubierto = false;
		actualPosicion = new Vector3 (148.04f, 3.93f, 44.15f);
		numeroEnemigos = 0;
		
		interfaz.SetActive(false);
       	puntuacion.SetActive(false); 
    }
	
	
	void Update () {
		disparo ();
    }

    // Empezar el juego desde la primera zona
   	public void empezarJuego () {
                
        menu.SetActive(false);
   		interfaz.SetActive(true);
   		localizacion = 0;
		cubierto = false;
		actualPosicion = new Vector3 (148.04f, 3.93f, 44.15f);
		numeroEnemigos = 3;

        activarZona(localizacion);	   			
   	}
    
    // Controles del jugador: disparar, recargar, agacharse - pararse
	public void disparo() {
        
        if ( Input.GetButtonDown("R1")) { // Disparar
			if (DisparoIzq != null) {			
				DisparoIzq ();
			}
		}

		if (Input.GetButtonDown("B")) { // Agacharse - Pararse      
			if (cubierto) {
				StartCoroutine (corutinaPararse());
				cubierto = false;
			}
			else {
				StartCoroutine (corutinaAgacharse());
				cubierto = true;
			}
		}

        if (Input.GetButtonDown("X")) // Recargar
        {
            Recarga();
        }
	}
   
	public void reducirEnemigos() {
		numeroEnemigos--;

		if (numeroEnemigos == 0) { // Si no hay enemigos, pasar a otra zona
			localizacion++;

			switch(localizacion) {
			case 1:
				StartCoroutine (corutinaRelocalizar1()); // Movimiento de relocalizacion
				break;
			case 2:
				StartCoroutine (corutinaRelocalizar2());
				break;			
			case 3:
				StartCoroutine (corutinaRelocalizar3());
				break;
            case 4:
                StartCoroutine(corutinaRelocalizar4());
                break;
            case 5:
                StartCoroutine(corutinaRelocalizar5());
                break;
            case 6:
                StartCoroutine(corutinaRelocalizar6());
                break;                    
            case 7:
                StartCoroutine(corutinaRelocalizar7());
                break;
                
            }
		}
	}
   
    void activarZona(int zona)
    {
        zonas[zona].SetActive(true);
    }

	IEnumerator corutinaAgacharse() {
		float duracion = 0.5f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			
			Vector3 inicioPos = new Vector3 (actualPosicion.x, 3.93f, actualPosicion.z);			
			Vector3 finPos = new Vector3 (actualPosicion.x, 3.03f, actualPosicion.z);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}
	}

	IEnumerator corutinaPararse() {
		float duracion = 0.5f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {		
			Vector3 inicioPos = new Vector3 (actualPosicion.x,  3.03f, actualPosicion.z);
			Vector3 finPos = new Vector3 (actualPosicion.x, 3.93f, actualPosicion.z);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}
	}

	IEnumerator corutinaRelocalizar1() {
		float duracion = 1.0f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (148.04f, 3.93f, 44.15f);
			Vector3 finPos = new Vector3 (143.47f, 3.93f, 44.15f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion; 
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

        duracion = 2.0f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3(143.47f, 3.93f, 44.15f);
            Vector3 finPos = new Vector3(143.79f, 3.93f, 71.41f);
            Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

        activarZona(localizacion);
        cubierto = false;
		numeroEnemigos = 2;
	}

	IEnumerator corutinaRelocalizar2() {
		float duracion = 1.0f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (143.79f, 3.93f, 71.41f);
			Vector3 finPos = new Vector3 (137.858f, 3.93f, 71.41f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (137.858f, 3.93f, 71.41f);
			Vector3 finPos = new Vector3 (137.858f, 3.93f, 85.28f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (137.858f, 3.93f, 85.28f);
			Vector3 finPos = new Vector3 (125.64f, 3.93f, 85.28f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

        activarZona(localizacion);
        cubierto = false;
		numeroEnemigos = 3;	
	}

	IEnumerator corutinaRelocalizar3() {
		float duracion = 1.0f;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (125.64f, 3.93f, 85.28f);
			Vector3 finPos = new Vector3 (116.95f, 3.93f, 85.28f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion) {
			Vector3 inicioPos = new Vector3 (116.95f, 3.93f, 85.28f);
			Vector3 finPos = new Vector3 (116.95f, 3.93f, 76.79f);
			Vector3 nuevoPosicion = Vector3.Lerp (inicioPos, finPos, t);
			actualPosicion = nuevoPosicion;
			plataforma.GetComponent<Transform> ().position = nuevoPosicion;
			yield return null;
		}

        activarZona(localizacion);

        cubierto = false;
		numeroEnemigos = 2;
	}

    IEnumerator corutinaRelocalizar4()
    {
        float duracion = 4.0f;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(116.95f, 3.93f, 76.79f);
            Vector3 finPos = new Vector3(111.7f, 3.93f, 25.5f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }
             

        activarZona(localizacion);

        cubierto = false;
        numeroEnemigos = 3;
    }

    IEnumerator corutinaRelocalizar5()
    {
        float duracion = 1.0f;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(111.7f, 3.93f, 25.5f);
            Vector3 finPos = new Vector3(125f, 3.93f, 9.92f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }

        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(125f, 3.93f, 9.92f);
            Vector3 finPos = new Vector3(143.1f, 3.93f, 10.111f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }


        activarZona(localizacion);

        cubierto = false;
        numeroEnemigos = 2;
    }

    IEnumerator corutinaRelocalizar6()
    {
        float duracion = 2.0f;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(143.1f, 3.93f, 10.111f);
            Vector3 finPos = new Vector3(154.25f, 3.93f, 28.06f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }

        duracion = 2.0f;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(154.25f, 3.93f, 28.06f);
            Vector3 finPos = new Vector3(171.9f, 3.93f, 28.06f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }


        activarZona(localizacion);

        cubierto = false;
        numeroEnemigos = 3;
    }
    
    IEnumerator corutinaRelocalizar7()
    {
        float duracion = 1.5f;
        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(171.9f, 3.93f, 28.06f);
            Vector3 finPos = new Vector3(171.7941f, 3.93f, 48f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }

        for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / duracion)
        {
            Vector3 inicioPos = new Vector3(171.7941f, 3.93f, 48f);
            Vector3 finPos = new Vector3(148.04f, 3.93f, 44.15f);
            Vector3 nuevoPosicion = Vector3.Lerp(inicioPos, finPos, t);
            actualPosicion = nuevoPosicion;
            plataforma.GetComponent<Transform>().position = nuevoPosicion;
            yield return null;
        }
                

        cubierto = false;
        numeroEnemigos = 0;

        menu.SetActive(true);
             
        GameObject.Find("TextoStart").GetComponent<Text>().text = "Fin";
  
        puntuacion.SetActive(true);
        calcularPuntuacion();
    }

    private void calcularPuntuacion() {
       	GameObject textoPuntuacion = GameObject.Find("TextoPuntuacion");
       	Text texto_puntuacion = textoPuntuacion.GetComponent<Text>();
    	GameObject jugador = GameObject.Find("Jugador");
		VidaJugador vidaJugador = jugador.GetComponent<VidaJugador>();

		if(vidaJugador.vida > 70)
			texto_puntuacion.text = "TE HAN SOBRADO MÁS DE 70 VIDAS, MUY BIEN!";
		else if(vidaJugador.vida > 40)
			texto_puntuacion.text = "TE HAN SOBRADO MÁS DE 40 VIDAS, BIEN!";
		else if((vidaJugador.vida < 40) && (vidaJugador.vida > 0))
			texto_puntuacion.text = "TE HAN SOBRADO MENOS DE 40 VIDAS, HA ESTADO REGULAR";
		else if(vidaJugador.vida <= 0)
			texto_puntuacion.text = "NIVEL NO SUPERADO";
    }
    
}
