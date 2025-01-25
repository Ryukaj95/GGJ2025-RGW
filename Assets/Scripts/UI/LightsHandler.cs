using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsHandler : MonoBehaviour
{
    [SerializeField] private Light missionFailedLight;
    [SerializeField] private Light reloadingLight;
    [SerializeField] private Light hitLight;
    [SerializeField] private Light criticalLight;
    [SerializeField] private Light dangerLight;

    private void Awake() {
        TurnOffAllLights();
    }

    public void TurnOffAllLights() {
        missionFailedLight.TurnOff();
        reloadingLight.TurnOff();
        hitLight.TurnOff();
        criticalLight.TurnOff();
        dangerLight.TurnOff();
    }

    public void TurnOnAllLights() {
        missionFailedLight.TurnOn();
        reloadingLight.TurnOn();
        hitLight.TurnOn();
        criticalLight.TurnOn();
        dangerLight.TurnOn();
    }

    public void ToggleMissionFailedLight() {
        missionFailedLight.ToggleLight();
    }

    public void TurnOnMissionFailedLight() {
        missionFailedLight.TurnOn();
    }

    public void TurnOffMissionFailedLight() {
        missionFailedLight.TurnOff();
    }

    public void ToggleReloadingLight() {
        reloadingLight.ToggleLight();
    }

    public void TurnOnReloadingLight() {
        reloadingLight.TurnOn();
    }

    public void TurnOffReloadingLight() {
        reloadingLight.TurnOff();
    }

    public void ToggleHitLight() {
        hitLight.ToggleLight();
    }

    public void TurnOnHitLight() {
        hitLight.TurnOn();
    }

    public void TurnOffHitLight() {
        hitLight.TurnOff();
    }

    public void ToggleCriticalLight() {
        criticalLight.ToggleLight();
    }

    public void TurnOnCriticalLight() {
        criticalLight.TurnOn();
    }

    public void TurnOffCriticalLight() {
        criticalLight.TurnOff();
    }

    public void ToggleDangerLight() {
        dangerLight.ToggleLight();
    }

    public void TurnOnDangerLight() {
        dangerLight.TurnOn();
    }

    public void TurnOffDangerLight() {
        dangerLight.TurnOff();
    }
}
