//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//public class ForcePlayerMovement : MonoBehaviour
//{
//    [Header("PlayerRun")]
//    public float runMaxSpeed;     //tragetpoeed
//    public float runAcceleration; //from 0 to runmaxspeed
//    public float runAccelAmount;
//    public float runDecceleration;
//    public float runDeccelAmount;

//    [Range(0.01f, 1)] public float accelInAir;
//    [Range(0.01f, 1)] public float deccelInAir;

//    public bool doConserveMomentum;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    private void OnValidate()
//    {
//        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
//        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

//        #region
//        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
//        runDecceleration 
//        #endregion
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    private void FixedUpdate()
//    {
//        #region Run
//        float targetSpeed = moveInput * moveSpeed;
//    }
//}
