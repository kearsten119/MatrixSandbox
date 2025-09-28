using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Script to detect when a certain XR Grabbable object has been
/// grabbed and/or released.  Specify the object of interest
/// by associating its XR Grabable component in the script's
/// Inspector panel.
/// </summary>
public class HandlePivot : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabInteractable;

    float StartGrabAngle;   //angle of the interactor when the handle is grabbed
    float GrabAngle; //current angle of the interactor relative to the StartGrabAngle

    float StartHandleAngle; //the angle of the handle before it is grabbed.  We will return to this angle when the handle is released.

    bool Grabbed = false;   //true when the handle has been grabbed;  false, otherwise.

    IXRSelectInteractor interactor; //a reference to the XR Interactor that grabs the handle (either the left or right hand)

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            //Subscribe to the grab/release events
            grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
            grabInteractable.selectExited.AddListener(OnObjectReleased);
        }
    }

    void OnDisable()
    {
        //Un-subscribe to the grab/release events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
            grabInteractable.selectExited.RemoveListener(OnObjectReleased);
        }
    }

    private void Start()
    {
        //record the default position of the handle.  We will return
        //to this position whenever the handle is not grabbed.
        StartHandleAngle = transform.rotation.eulerAngles.z;
    }


    private void Update()
    {
        if (Grabbed)
        {
            //read and calculate the orientation of the interactor relative
            //to its orientation when the handle is first grabbed.
            float CurrentAngle = interactor.transform.eulerAngles.z;
            GrabAngle = CurrentAngle - StartGrabAngle;

            //adjust the angle so that we are always in the -180 to 180 range.
            if (GrabAngle > 180) GrabAngle -= 360;

            //clamp the angle.  The handle can rotate a maximum of 90 degrees.
            var clampedAngle = Mathf.Clamp(StartHandleAngle + GrabAngle, StartHandleAngle - 90f, StartHandleAngle);

            //set the rotation of the handle.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, clampedAngle);
        }
        else
        {
            //when the handle is not grabbed, set it to its default position.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, StartHandleAngle);
        }

    }

    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        //Debug.Log($"Grabbed by {args.interactorObject.transform.name}!");

        interactor = args.interactorObject;
        Grabbed = true;
        StartGrabAngle = interactor.transform.rotation.eulerAngles.z;
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        //Debug.Log($"Released by {args.interactorObject.transform.name}!");

        Grabbed = false;
    }
}