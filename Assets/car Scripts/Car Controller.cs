using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public AudioSource caraudio;
    public AudioSource carbreak;
    public AudioClip idle;

    public AudioClip run;
    public AudioClip breaksound;
    public float minpitch;
    public float maxpitch;

    public Material brakeLight;


    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLefttWheel;
    public Transform rearRightWheel;

    public WheelCollider fLW;
    public WheelCollider fRW;
    public WheelCollider rLW;
    public WheelCollider rRW;

    public float motorforce;
    public float breakforce;
    public float steerangle;

    float horizontalInput;
    float verticalInput;
    float current_steerangle;
    float current_breakforce;
    bool isbreaking;
    Rigidbody rb;
    bool isbreak;




    void GetInput()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isbreaking = Input.GetKey(KeyCode.Space);
        if (isbreaking)
        {
            BrakeLightOn();
            if (!isbreak)
            {
                carbreak.PlayOneShot(breaksound);
                isbreak = true;
            }
        }
        else
        {
            BrakeLightOff();
            isbreak = false;

        }


    }

    void Acceleration()
    {
        fLW.motorTorque = verticalInput * motorforce;
        fRW.motorTorque = verticalInput * motorforce;
        rLW.motorTorque = verticalInput * motorforce;
        rRW.motorTorque = verticalInput * motorforce;
        current_breakforce = isbreaking ? breakforce : 0f;
    }
    void Breaking()
    {
        fLW.brakeTorque = current_breakforce;
        fRW.brakeTorque = current_breakforce;
        rLW.brakeTorque = current_breakforce;
        rRW.brakeTorque = current_breakforce;

    }
    void Steering()
    {
        current_steerangle = steerangle * horizontalInput;
        fLW.steerAngle = current_steerangle;
        fRW.steerAngle = current_steerangle;
    }
    void Wheelangle(WheelCollider collider, Transform trfm)
    {
        Vector3 pos;
        Quaternion rotate;
        collider.GetWorldPose(out pos, out rotate);
        trfm.position = pos;
        trfm.rotation = rotate;
    }

    void SetWheel()
    {
        Wheelangle(fLW, frontLeftWheel);
        Wheelangle(fRW, frontRightWheel);
        Wheelangle(rLW, rearLefttWheel);
        Wheelangle(rRW, rearRightWheel);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Acceleration();
        Breaking();
        Steering();
        SetWheel();
        Enginesound();
        UpdateDamping();
    }
    public void BrakeLightOn()
    {
        // brakeLight.EnableKeyword("_EMISSION");
        // brakeLight.SetColor("_EmissionColor", Color.red * 6f);
        brakeLight.color = Color.red;
    }

    public void BrakeLightOff()
    {
        // brakeLight.SetColor("_EmissionColor", Color.white);
        brakeLight.color = Color.white;
    }

    void Enginesound()
    {
        float speed = rb.linearVelocity.magnitude * 3.6f;
        float speed1 = Mathf.InverseLerp(0f, 100f, speed);
        // float speed = Mathf.Abs(verticalInput);

        if (speed < 0.1)
        {

            if (caraudio.clip != idle)
            {
                caraudio.clip = idle;
                caraudio.Play();
            }
            caraudio.pitch = Mathf.Lerp(caraudio.pitch, minpitch, Time.deltaTime * 1f);
        }

        else
        {
            if (caraudio.clip != run)
            {
                caraudio.clip = run;
                caraudio.Play();
            }
            caraudio.pitch = Mathf.Lerp(caraudio.pitch, Mathf.Lerp(minpitch, maxpitch, speed1), Time.deltaTime * (verticalInput == 0 ? 6f : 2f));
        }
    }
    void UpdateDamping()
    {


        float LD = verticalInput != 0 ? 0 : .6f;
        float AD = verticalInput != 0 ? 0.05f : .5f;

        rb.linearDamping = Mathf.Lerp(rb.linearDamping, LD, Time.deltaTime * 5f);
        rb.angularDamping = Mathf.Lerp(rb.angularDamping, AD, Time.deltaTime * 5f);

    }



}
