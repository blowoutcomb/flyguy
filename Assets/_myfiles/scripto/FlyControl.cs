using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlyControl : MonoBehaviour
{
    public float throttleInc = 0.1f;
    public float maxThrust = 200f;
    public float responsive = 10f;

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;

    private float responseMod
    {
        get { return (rb.mass / 10f) * responsive; }
    }

    Rigidbody rb;


    [SerializeField] TextMeshProUGUI hud;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInput()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space)) throttle += maxThrust;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleInc;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void Update()
    {
        HandleInput();
        UpdateHUD();
    }

    private void FixedUpdate()
    {

        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(transform.up * yaw * responseMod);
        rb.AddTorque(transform.right * pitch * responseMod);
        rb.AddTorque(-transform.forward * roll * responseMod);
    }

    private void UpdateHUD()
    {
        hud.text = "Throttle " + throttle.ToString("F0") + "%\n";
        hud.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }
}
