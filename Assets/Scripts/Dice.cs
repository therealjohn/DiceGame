using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private float upwardForceMultiplier = 8f;
    [SerializeField] private float torqueLowEndAmount = 100f;
    [SerializeField] private float torqueHighEndAmount = 500f;

    public int sideValue;
    public bool canRoll;

    public bool IsDoneRolling => 
        rigidbody.velocity == Vector3.zero && rigidbody.IsSleeping() && !canRoll;

    private new Rigidbody rigidbody;    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Reset();
    }

    public void Reset()
    {
        sideValue = -1;
        canRoll = true;
    }

    public void Roll()
    {
        canRoll = false;

        rigidbody.AddForce(Vector3.up * upwardForceMultiplier, ForceMode.Impulse);

        rigidbody.AddTorque(Random.Range(torqueLowEndAmount, torqueHighEndAmount),
            Random.Range(torqueLowEndAmount, torqueHighEndAmount),
            Random.Range(torqueLowEndAmount, torqueHighEndAmount));        
    }
}
