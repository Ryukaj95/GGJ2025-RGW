using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Funko : MonoBehaviour
{
    [SerializeField] private GameObject Funko0;
    [SerializeField] private GameObject Funko1;
    [SerializeField] private GameObject Funko2;
    [SerializeField] private GameObject Funko3;
    [SerializeField] private GameObject Funko4;
    [SerializeField] private GameObject Funko5;

    private GameObject currentFunkoObject;
    // This goes from 0 to 5
    private int currentFunko;

    private void Awake() {
        Funko0.SetActive(true);
        Funko1.SetActive(false);
        Funko2.SetActive(false);
        Funko3.SetActive(false);
        Funko4.SetActive(false);
        Funko5.SetActive(false);

        currentFunkoObject = Funko0;
        currentFunko = 0;

        // StartCoroutine(TestFunko());
    }

    private void Start() {
        currentFunkoObject = Funko0;
        currentFunko = 0;
    }
    
    public void NextFunko() {
        if (currentFunko == 5) {
            return;
        }

        if (currentFunko != 0) {
            currentFunkoObject.SetActive(false);
        }

        currentFunko++;

        switch (currentFunko) {
            case 1:
                currentFunkoObject = Funko1;
                break;
            case 2:
                currentFunkoObject = Funko2;
                break;
            case 3:
                currentFunkoObject = Funko3;
                break;
            case 4:
                currentFunkoObject = Funko4;
                break;
            case 5:
                currentFunkoObject = Funko5;
                break;
        }

        currentFunkoObject.SetActive(true);
    }

    public void ResetFunko() {
        if (currentFunko == 0) {
            return;
        }

        currentFunkoObject.SetActive(false);
        currentFunkoObject = Funko0;
        currentFunko = 0;
        currentFunkoObject.SetActive(true);
    }

    public void SetFunko(int funkoNumber) {
        if (funkoNumber < 0 || funkoNumber > 5) {
            return;
        }

        if (funkoNumber == currentFunko) {
            return;
        }

        currentFunkoObject.SetActive(false);
        currentFunko = funkoNumber;

        switch (currentFunko) {
            case 0:
                currentFunkoObject = Funko0;
                break;
            case 1:
                currentFunkoObject = Funko1;
                break;
            case 2:
                currentFunkoObject = Funko2;
                break;
            case 3:
                currentFunkoObject = Funko3;
                break;
            case 4:
                currentFunkoObject = Funko4;
                break;
            case 5:
                currentFunkoObject = Funko5;
                break;
        }
    }

    public int GetCurrentFunko() {
        return currentFunko;
    }

    private IEnumerator TestFunko() {
        while (true) {
            yield return new WaitForSeconds(2f);

            if (currentFunko == 5) {
                ResetFunko();
            } else {
                NextFunko();
            }
        }
    }
}
