using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace MyVrSample
{
    /// <summary>
    /// 오브젝트를 잡을때 옵셋 위치 설정
    /// </summary>
    public class XRGrabInteractableOffset : XRGrabInteractable
    {
        #region Variables
        private GameObject attachPoint;

        private Vector3 initialLocalPosition;
        private Quaternion initialLocalRotation;
        #endregion

        private void Start()
        {
            if(attachTransform == null)
            {
                attachPoint = new GameObject("Offset Grab Pivot");
                attachPoint.transform.SetParent(transform, false);
                attachTransform = attachPoint.transform;
            }
            else
            {
                initialLocalPosition = attachTransform.localPosition;
                initialLocalRotation = attachTransform.localRotation;
            }
        }

        /*protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if(args.interactorObject is XRDirectInteractor)
            {
                attachTransform.position = args.interactorObject.transform.position;
                attachTransform.rotation = args.interactorObject.transform.rotation;
            }
            else
            {
                attachTransform.localPosition = initialLocalPosition;
                attachTransform.localRotation = initialLocalRotation;
            }

            base.OnSelectEntered(args);
        }*/
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                attachTransform.position = args.interactorObject.transform.position;
                attachTransform.rotation = args.interactorObject.transform.rotation;
            }
            else
            {
                attachTransform.localPosition = initialLocalPosition;
                attachTransform.localRotation = initialLocalRotation;
            }

            base.OnSelectEntering(args);
        }
    }
}