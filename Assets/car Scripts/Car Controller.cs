using UnityEngine;

public class CarController : MonoBehaviour
{
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

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isbreaking = Input.GetKey(KeyCode.Space);
    }

    void Acceleration()
    {
        fLW.motorTorque = verticalInput * motorforce;
        fRW.motorTorque = verticalInput * motorforce;
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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Acceleration();
        Breaking();
        Steering();
        SetWheel();
    }
}
