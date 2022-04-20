using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllerRumble : MonoBehaviour
{
    PlayerIndex p1,p2,p3,p4;
    GamePadState state;
    GamePadState prevState;

    private void Start()
    {
        p1 = PlayerIndex.One;
        p2 = PlayerIndex.Two;
        p3 = PlayerIndex.Three;
        p4 = PlayerIndex.Four;
    }

    public void PlaceRumble(int pNum)
    {
        PlayerIndex thisIndex = p1;
        switch (pNum)
        {
            case 1:
                thisIndex = p1;
                break;
            case 2:
                thisIndex = p2;
                break;
            case 3:
                thisIndex = p3;
                break;
            case 4:
                thisIndex = p4;
                break;
        }
        StartCoroutine(Rumble(thisIndex));
    }

    IEnumerator Rumble(PlayerIndex pIndex)
    {
        GamePad.SetVibration(pIndex, 0.1f, 0.1f);
        yield return new WaitForSeconds(0.3f);
        GamePad.SetVibration(pIndex, 0f, 0f);
    }
}