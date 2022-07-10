using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{
    public float walkAmount;
    public float stepSpeed;

    private int stepsTaken = 0;

    [Header("Flags")]
    public bool hasBuilding;
    public bool hasEntity;
    public bool rightStep;
    public static bool isRaging;
    [Header("add on")]
    public DinoAnimation dinoAnimation;
    public DinoHealth dinoHealh;
    public GameObject dinoFire;
    [Space]
    public BuildingInWorld BuildingInWorld;
    public CarController carControllerInWorld;

    private Transform _transform;
    private bool canWalk = false;

    public static EventHandler onRageFinished;
    public static EventHandler onRageStarted;

    private void Start()
    {
        RhythmController.onCorrectHit += HandleAction;
        ComboManager.onFeverEnter += handleRage;
        ComboManager.onFeverEnter += delegate { dinoFire.SetActive(true); };
        ComboManager.onFeverExit += delegate { dinoFire.SetActive(false); };

        _transform = transform;

        //RhythmController.onStartGame += handleRage;
        //handleRage(this, null);
    }

    private void HandleAction(object sender, EventArgs e)
    {
        if (isRaging) return;

        if (!hasBuilding && !hasEntity)
        {
            Walk();
        }
        else if (hasBuilding && !hasEntity)
        {
            DestroyBuilding();
        }
        else if(!hasBuilding && hasEntity)
        {
            Walk();
            carControllerInWorld.ExplodeCar();
        }
    }

    private void Update()
    {
        dinoAnimation.setIdleState(hasBuilding);
    }

    private void DestroyBuilding()
    {
        dinoAnimation.PlayTargetAnimation("attack");
        hasBuilding = BuildingInWorld.BreakBuilding();
        CameraShake.instance.Shake(.3f, .2f);
    }

    private void Walk()
    {
        //Vector3 newPos = _transform.position + new Vector3(0, 0, walkAmount);
        //_transform.Translate(new Vector3(0, 0, walkAmount));
        stepsTaken++;

        StartCoroutine(walkLerp());
        CameraShake.instance.Shake(.1f, .06f);
        rightStep = !rightStep;
        if (rightStep)
            dinoAnimation.PlayTargetAnimation("R_step");
        else
            dinoAnimation.PlayTargetAnimation("L_step");
    }

    IEnumerator walkLerp()
    {
        canWalk = true;
        Vector3 finalPos = new Vector3(0, 0, walkAmount * stepsTaken);
        while(_transform.position.z < finalPos.z - 0.1f)
        {
            _transform.position = Vector3.Lerp(_transform.position, finalPos, stepSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _transform.position = finalPos;
        canWalk = false;
    }

    public void handleRage(object sender, EventArgs e)
    {
        dinoAnimation.PlayTargetAnimation("rage");
        onRageStarted?.Invoke(this, e);
        isRaging = true;
        AudioManager.instance.Play("rage");
        Invoke(nameof(finishedRage), .75f);
    }
    private void finishedRage()
    {
        onRageFinished?.Invoke(this, null);
        isRaging = false;
    }
}
