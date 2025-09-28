using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
/// <summary>
/// Script to detect when a certain XR Grabbable object has been
/// grabbed and/or released.  Specify the object of interest
/// by associating its XR Grabable component in the script's
/// Inspector panel.
/// </summary>
public class GrabDetection : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] XRInputValueReader<Quaternion> controllerAxis;  //Z-axis
    public Vector3 CurrentAngle;
    public Vector3 StartAngle;
    public float RawAngle;
    public float Angle;

    bool Grabbed = false;

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
            grabInteractable.selectExited.AddListener(OnObjectReleased);
        }
    }

    private void Update()
    {
        var q = controllerAxis.ReadValue();
        CurrentAngle = q.eulerAngles;
        //float z = x.z;

        //while (z > 180) z -= 180;
        //while (z < -180) z += 180;
        //Angle = Quaternion.Angle(q, StartAngle);
        //RawAngle = Angle;

        RawAngle = CurrentAngle.z - StartAngle.z;
        Angle = RawAngle;

        if (Angle > 180) Angle -= 360;

        //Angle = angle;
        //Angle = q.eulerAngles. - StartAngle.eulerAngles.z;
        //Quaternion.
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
            grabInteractable.selectExited.RemoveListener(OnObjectReleased);
        }
    }

    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"Grabbed by {args.interactorObject.transform.name}!");
        // Add your custom logic here for when the object is grabbed

        Grabbed = true;
        StartAngle = controllerAxis.ReadValue().eulerAngles;
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        Debug.Log($"Released by {args.interactorObject.transform.name}!");
        // Add your custom logic here for when the object is released

        Grabbed = false;
    }
}