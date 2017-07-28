using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
public class ConsumeApi : MonoBehaviour {

    public List<float> latitud,longitud;
	public List<string> nombre, detalle;
    public float latitudActual, longitudActual, latitudVisita, longitudVisita;
    public Text fichaNombre, fichaDetalle;
	public int objetoIndice;
	public Transform scroll;
	public bool coincideUbicacion;
	public Text distanceTextObject;

	// Use this for initialization
	void Start ()
    {
		coincideUbicacion = false;
        StartCoroutine(getUbicaciones());
        StartCoroutine(getCoordenadas());
		StartCoroutine(comparaUbicacion());
		scroll.gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		obtenerDatosFichas ();
	}

    public IEnumerator getUbicaciones()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(

			"http://192.168.43.213:3001/api/ubicaciones"//192.168.43.213

        );
        yield return webRequest.Send();

        if (!webRequest.isNetworkError)
        { 
            ListaCoordenadas listaCoordenadas = JsonUtility.FromJson<ListaCoordenadas>(
				webRequest.downloadHandler.text);
            // FixJson("Coordenadas", webRequest.downloadHandler.text));
            foreach (Coordenadas coordenada in listaCoordenadas.Coordenadas)
            {
                Debug.Log(coordenada.latitud);
                latitud.Add(float.Parse(coordenada.latitud));
                Debug.Log(coordenada.longitud);
                longitud.Add(float.Parse(coordenada.longitud));
				Debug.Log(coordenada.nombre);
				nombre.Add(coordenada.nombre);
				Debug.Log(coordenada.detalle);
				detalle.Add(coordenada.detalle);
            }
        }

        else
        {
            Debug.Log(webRequest.error);
        }
    }

    IEnumerator getCoordenadas()
    {
        //while true so this function keeps running once started.
        while (true)
        {
            // check if user has location service enabled
            if (!Input.location.isEnabledByUser)
                yield break;

            // Start service before querying location
            Input.location.Start(1.0f, .1f);

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
                yield break;
            }
            else
            {
                latitudActual = Input.location.lastData.latitude;
                longitudActual = Input.location.lastData.longitude;
                
            }
            Input.location.Stop();
        }
    }

	public IEnumerator comparaUbicacion()
    {
		while(true)
		{
			for(objetoIndice=0;objetoIndice < latitud.Count;objetoIndice++)
	        {
				if (retornaDistancia(latitudActual,longitudActual,latitud[objetoIndice],longitud[objetoIndice]) < 99999999.0f)//comparar ubicacion actual con margen con las ubicaciones de la lista
	            {
					latitudVisita = latitud[objetoIndice];
					longitudVisita = longitud[objetoIndice];
					coincideUbicacion = true;
					yield return new WaitForSeconds(10.0f);
	            }
	        }
			coincideUbicacion = false;
			break;
		}
    }

    public float retornaDistancia(float lat1, float lon1, float lat2, float lon2)
    {
        var R = 6378.137; 
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
          Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
          Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        double distance = R * c;
        distance = distance * 1000f;

        float distanceFloat = (float)distance;
		distanceTextObject.text = distanceFloat.ToString ();

        return distanceFloat;
    }



	public void obtenerDatosFichas()
    {
		if (true)
		{
			scroll.gameObject.SetActive (true);
			//fichaNombre.text = nombre[0];
			//fichaDetalle.text = detalle[0];

			fichaNombre.text = nombre[0];
			fichaDetalle.text = detalle[0];
		}

		else
        {
			objetoIndice = -1;
            //nombre.Clear();
			//detalle.Clear();
			scroll.gameObject.SetActive (false);
			//fichaNombre.text ="Loading ...";
			//fichaDetalle.text ="Loading ...";
        }
    }
		
}
