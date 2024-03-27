using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHoverBike : InteractableObjectBase
{

    public override void Interact()
    {
        HoverBike bike = GetComponent<HoverBike>();
        bike.OnPlayerSeat();

    }
}
