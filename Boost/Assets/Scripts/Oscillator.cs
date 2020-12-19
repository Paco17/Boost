using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 vector = new Vector3(0f, -10f, 0f);
    [SerializeField] float period = 2f;

    //todo Remove from inspector

    // 0 for not moved, 1 fully moved
    [Range(0,1)] [SerializeField] float movementFactor;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
    }


    // Update is called once per frame
    void Update() {
        //Set movement factor
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //Grows continually

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles*tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor*vector;
        transform.position = startPos + offset;
        
    }
}
