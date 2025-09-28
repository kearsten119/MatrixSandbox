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
    float StartGrabAngle;
    public float GrabAngle;

    float StartHandleAngle;

    bool Grabbed = false;

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
            grabInteractable.selectExited.AddListener(OnObjectReleased);
        }
    }

    private void Start()
    {
        StartHandleAngle = transform.rotation.eulerAngles.z;
    }


    private void Update()
    {
        if (Grabbed)
        {
            float CurrentAngle = controllerAxis.ReadValue().eulerAngles.z;
            GrabAngle = CurrentAngle - StartGrabAngle;

            if (GrabAngle > 180) GrabAngle -= 360;

            var clampedAngle = Mathf.Clamp(StartHandleAngle + GrabAngle, StartHandleAngle - 90f, StartHandleAngle);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, clampedAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, StartHandleAngle);
        }

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
        StartGrabAngle = controllerAxis.ReadValue().eulerAngles.z;
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        Debug.Log($"Released by {args.interactorObject.transform.name}!");
        // Add your custom logic here for when the object is released

        Grabbed = false;

    }
}