﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AugmentedScript : MonoBehaviour
{


    private float originalLatitude;
    private float originalLongitude;
    private float currentLongitude;
    private float currentLatitude;


    private double distance;

    private bool setOriginalValues = true;

    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private float speed = .1f;

    void Start()
    {
        StartCoroutine(GetCoordinates());
        targetPosition = transform.position;
        originalPosition = transform.position;

    }

    void Update()
    {
		transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }

    IEnumerator GetCoordinates()
    {

        while (true)
        {
            if (!Input.location.isEnabledByUser)
                yield break;
            Input.location.Start(1f, .1f);

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }
				
            if (maxWait < 1)
            {
                yield break;
            }
				
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                yield break;
            }
            else
            {

                if (setOriginalValues)
                {
                    originalLatitude = Input.location.lastData.latitude;
                    originalLongitude = Input.location.lastData.longitude;
                    setOriginalValues = false;
                }
					
                currentLatitude = Input.location.lastData.latitude;
                currentLongitude = Input.location.lastData.longitude;

                Calc(originalLatitude, originalLongitude, currentLatitude, currentLongitude);

            }
            Input.location.Stop();
        }
    }
		
    public void Calc(float lat1, float lon1, float lat2, float lon2)
    {

        var R = 6378.137; // Radius of earth in KM
        var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
          Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
          Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        distance = R * c;
        distance = distance * 1000f; // meters
                                     //set the distance text on the canvas
		//distanceTextObject.text = "Distance: " + distance.ToString();
    
        float distanceFloat = (float)distance;
        
        targetPosition = originalPosition - new Vector3(0, 0, distanceFloat *12);

    }
}
