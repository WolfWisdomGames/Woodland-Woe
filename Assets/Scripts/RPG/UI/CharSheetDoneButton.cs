using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSheetDoneButton : MonoBehaviour
{
    // This destroys the entire character sheet modal so the user returns to whatever previous screen they were in.
    public void ConfirmClick()
    {
        CharsheetUIInitializer.characterBeingDisplayed.ConfirmChanges();
        Destroy(transform.parent.gameObject);
    }
}
