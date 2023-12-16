using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabHandPoint : MonoBehaviour
{
    public HandData rightHandPose;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;

    private Quaternion finalHandRotation;
    private Quaternion startingHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        Console.WriteLine(arg.interactorObject);
            if(arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            Console.WriteLine(handData);
            handData.animator.enabled = false;

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);

        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for(int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h1, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotations)
    {
        h1.root.localPosition = newPosition;
        h1.root.localRotation = newRotation;

        for(int i = 0; i < newBonesRotations.Length; i++)
        {
            h1.fingerBones[i].localRotation = newBonesRotations[i];
        }
    }

}
