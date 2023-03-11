using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    Transform steeringPivot;
    [SerializeField]
    Transform barrelRollPivot;

    // RAYCAST
    RaycastHit downHit;
    float rayCastDistance = 10f;
    [SerializeField]
    LayerMask trackLayer;
    [SerializeField]
    LayerMask invisibleTrackLayer_RIGHT;
    [SerializeField]
    LayerMask invisibleTrackLayer_LEFT;
    bool lastFrameOnTrack = true;
    bool thisFrameOnTrack = true;


    // HOVER
    float hover_height = 1.9f; // 1.8f - .5f;
    float height_above_cast = 5;

    // LOGIC VARIABLES
    Vector3 prev_up;
    float current_speed;
    float max_speed = 250; // 170;
    float min_speed = 150;
    float accel = 90f;
    float deccel = 200f;

    float smooth_y;
    float height_smooth = 12f;

    // Rotation related
    private Quaternion tilt;
    private float pitch_smooth = 5f;
    Quaternion global_orientation;
    private Vector3 previousGravity;
    private float turn_angle;
    private float turn_speed = 80;

    //Shield
    Shield shield;


    // Jump
    bool jumping = false;
    float jumpDuration = .6f;


    void Awake()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up * rayCastDistance, Color.green, 50);
        if (Physics.Raycast(transform.position, -transform.up, out downHit, rayCastDistance, trackLayer))
        {
            transform.position = downHit.point + hover_height * transform.up;
            transform.rotation = Quaternion.FromToRotation(transform.up, downHit.normal) * transform.rotation;
            previousGravity = -downHit.normal;
        }
    }
    private void Start(){
        shield=GetComponentInChildren<Shield>();
    }
    // INPUT
    bool accelerate_pressed;
    public void Acelerate(InputAction.CallbackContext context)
    {
        if (context.started)
            accelerate_pressed = true;
        else if (context.canceled)
            accelerate_pressed = false;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && !jumping)
        {
            jumping = true;
        }
            
        //else if (context.canceled)
    }

    private void Update()
    {
        prev_up = transform.up;

        // Calcular velocidad
        if (accelerate_pressed)
        {
            float realAccel = accel * Time.deltaTime;

            current_speed += (current_speed + realAccel > max_speed) ? 0 : realAccel;
        }
        else if (current_speed > 0)
        {
            current_speed -= deccel * Time.deltaTime;
            current_speed = Mathf.Max(current_speed, 0f);
        }
        //else
        //    current_speed = 0f;

        current_speed = Mathf.Max(current_speed, min_speed);


        // Comprobar si esta encima de la carretera
        if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, trackLayer))
        {
            GetTurnInput();
            thisFrameOnTrack = true;
        }
        else if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, invisibleTrackLayer_RIGHT))
        {
            horizontal_input = -1;
            thisFrameOnTrack = false;
        }
        else if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, invisibleTrackLayer_LEFT))
        {
            horizontal_input = 1;
            thisFrameOnTrack = false;
        }

        TurnShip();

        AdjustOrientation();
        //checkGroundMovement();
        MoveShip();


        // Barrel Roll
        // En caso de que se haya salido de la carretera
        if (lastFrameOnTrack && !thisFrameOnTrack)
            BarrelRoll(1, 1, 2);

        // Actualizar valores
        lastFrameOnTrack = thisFrameOnTrack;
    }

    float horizontal_input;
    float vertical_input;
    void GetTurnInput()
    {
        horizontal_input = Input.GetAxis("Horizontal");
        vertical_input = Input.GetAxis("Vertical");
    }

    void TurnShip()
    {
        //// INPUT
        //horizontal_input = Input.GetAxis("Horizontal");
        //vertical_input = Input.GetAxis("Vertical");

        Debug.Log("horizontal_input = " + horizontal_input);

        // Calcular angulo de rotacion
        turn_angle = turn_speed * Time.deltaTime * horizontal_input;

        global_orientation = Quaternion.Euler(0, turn_angle, 0);

        // Inclinacion lateral
        float lateralRotation = Mathf.LerpAngle(steeringPivot.localRotation.eulerAngles.z, -horizontal_input * 20, Time.deltaTime * 5);
        steeringPivot.localRotation = Quaternion.Euler(0, 0, lateralRotation);
    }

    void MoveShip()
    {
        float distance = downHit.distance - height_above_cast;
        smooth_y = Mathf.Lerp(smooth_y, hover_height - distance, Time.deltaTime * height_smooth);
        smooth_y = Mathf.Max(distance / -3, smooth_y); //sanity check on smooth_y

        transform.localPosition += prev_up * smooth_y;
        transform.position += transform.forward * (current_speed * Time.deltaTime);
    }

    void AdjustOrientation()
    {
        //transform.rotation = Quaternion.FromToRotation(transform.up, downHit.normal) * global_orientation;
        Vector3 desired_up = Vector3.Lerp(prev_up, downHit.normal, Time.deltaTime * pitch_smooth);
        tilt.SetLookRotation(transform.forward - Vector3.Project(transform.forward, desired_up), desired_up);
        transform.rotation = tilt * global_orientation;
        previousGravity = -downHit.normal;
    }

    private void BarrelRoll(int orientation, float time = 1, float numLoops = 1)
    {
        barrelRollPivot.DOLocalRotate(new Vector3(0, 0, 360 * orientation * numLoops), time, RotateMode.FastBeyond360);
    }
}
