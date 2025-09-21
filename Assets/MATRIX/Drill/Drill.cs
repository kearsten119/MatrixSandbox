using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Drill : MonoBehaviour
{
    [SerializeField] Animator buttonAnimator;
    
    [SerializeField] Animator bitAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float drillSpeed = 0f;

    //A reference to the controller's (left or right) Near-Far Interactor
    //valid if the drill has been grabbed; null when the drill is released
    NearFarInteractor interactor = null;
    void Start()
    {
        interactor = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactor)
        {
            //read the trigger (Activate) value.  Use it to set the drill speed.
            drillSpeed = interactor.activateInput.ReadValue() ;
        }
        else
        {
            drillSpeed = 0f;
        }
        
        //float drillSpeed = buttonAnimator.GetLayerWeight(1);
        buttonAnimator.SetLayerWeight(1, drillSpeed);
        bitAnimation.speed = drillSpeed;
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        //Get a reference to the controller's Near-Far Interactor component.
        //From here, we will be able to read the activeInput and activeSelect values as necessary.
        interactor = args.interactorObject.transform.GetComponent<NearFarInteractor>();

        //Debug.Log("Selected");
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        //once we drop the drill, set the interactor to null.
        interactor = null;
    }

}
