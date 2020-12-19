using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 vector;

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
        Vector3 offset = movementFactor*vector;
        transform.position = startPos + offset;
        
    }
}
