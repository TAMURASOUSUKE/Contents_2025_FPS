using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractObject
{
    public void OnTriggerInteract();
    public bool GetCanInteract();
}
