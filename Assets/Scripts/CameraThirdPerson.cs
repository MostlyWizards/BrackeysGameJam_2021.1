using UnityEngine;

public class CameraThirdPerson : MonoBehaviour {

    //Current rotation values (in degrees);
    float currentXAngle = 0f;
    float currentYAngle = 0f;

    //Upper and lower limits (in degrees) for vertical rotation (along the local x-axis of the gameobject);
    [Range(0f, 90f)]
    public float upperVerticalLimit = 60f;
    [Range(0f, 90f)]
    public float lowerVerticalLimit = 60f;

    //Variables to store old rotation values for interpolation purposes;
    float oldHorizontalInput = 0f;
    float oldVerticalInput = 0f;

    //Camera turning speed; 
    public float cameraSpeed = 250f;

    //Whether camera rotation values will be smoothed;
    public bool smoothCameraRotation = false;

    //This value controls how smoothly the old camera rotation angles will be interpolated toward the new camera rotation angles;
    //Setting this value to '50f' (or above) will result in no smoothing at all;
    //Setting this value to '1f' (or below) will result in very noticable smoothing;
    //For most situations, a value of '25f' is recommended;
    [Range(1f, 50f)]
    public float cameraSmoothingFactor = 25f;

    //Variables for storing current facing direction and upwards direction;
    Vector3 facingDirection;
    Vector3 upwardsDirection;

    //References to transform and camera components;
    protected Transform tr;
    protected Camera cam;

    // Internal
    Vector2 axisIntensities;


    public void SetAxisIntensity(Vector2 value) { axisIntensities = value; }

    //Setup references.
    void Awake ()
    {
        tr = transform;
        cam = GetComponentInChildren<Camera>();

        //Set angle variables to current rotation angles of this transform;
        currentXAngle = tr.localRotation.eulerAngles.x;
        currentYAngle = tr.localRotation.eulerAngles.y;

        //Execute camera rotation code once to calculate facing and upwards direction;
        RotateCamera(0f, 0f);
    }


    void Update()
    {
        RotateCamera(axisIntensities.x, axisIntensities.y);
    }

    //Rotate camera; 
    protected void RotateCamera(float newHorizontalInput, float newVerticalInput)
    {
        if(smoothCameraRotation)
        {
            //Lerp input;
            oldHorizontalInput = Mathf.Lerp (oldHorizontalInput, newHorizontalInput, Time.deltaTime * cameraSmoothingFactor);
            oldVerticalInput = Mathf.Lerp (oldVerticalInput, newVerticalInput, Time.deltaTime * cameraSmoothingFactor);
        }
        else
        {
            //Replace old input directly;
            oldHorizontalInput = newHorizontalInput;
            oldVerticalInput = newVerticalInput;
        }

        //Add input to camera angles;
        currentXAngle += oldVerticalInput * cameraSpeed * Time.deltaTime;
        currentYAngle += oldHorizontalInput * cameraSpeed * Time.deltaTime;

        //Clamp vertical rotation;
        currentXAngle = Mathf.Clamp(currentXAngle, -upperVerticalLimit, lowerVerticalLimit);

        UpdateRotation();
    }

    //Update camera rotation based on x and y angles;
    protected void UpdateRotation()
    {
        tr.localRotation = Quaternion.Euler(new Vector3(0, currentYAngle, 0));

        //Save 'facingDirection' and 'upwardsDirection' for later;
        facingDirection = tr.forward;
        upwardsDirection = tr.up;

        tr.localRotation = Quaternion.Euler(new Vector3(currentXAngle, currentYAngle, 0));
    }

    //Set the camera's field-of-view (FOV);
    public void SetFOV(float _fov)
    {
        if(cam)
            cam.fieldOfView = _fov;	
    }
}