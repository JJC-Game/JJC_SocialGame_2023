﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AppSceneManager : MonoBehaviour
{
    public DefineParam.SCENE_ID currentSceneId;

    public int currentScenarioId;
    public int currentCharaId;

    UnityAction scenarioPopOutCallbackFunc;
    public DefineParam.SCENE_ID scenarioPopOutSceneId;

    UnityEvent refreshUserInfo = new UnityEvent();

    void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;

        currentScenarioId = DefineParam.SCENARIO_INVALID;
        currentCharaId = DefineParam.CHARA_INVALID;
        scenarioPopOutCallbackFunc = ScenarioPopOut;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScenarioId(int scenarioId){
        currentScenarioId = scenarioId;
    }

    public int GetScenarioId(){
        return currentScenarioId;
    }    

    public void ChangeScene(DefineParam.SCENE_ID sceneId){
        currentSceneId = sceneId;

        FadeManager.ChangeScene(sceneId.ToString());
    }

    public UnityAction GetScenarioPopOutCallbackFunc(){
        return scenarioPopOutCallbackFunc;
    }

    public void ScenarioPopOut(){
        Debug.Log("シナリオが終わったよ");
        ChangeScene(scenarioPopOutSceneId);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneId = SceneToSceneId(scene);
    }

    public void SetScenarioPopOutSceneId(DefineParam.SCENE_ID inputSceneId)
    {
        scenarioPopOutSceneId = inputSceneId;
    }

    private DefineParam.SCENE_ID SceneToSceneId(Scene scene)
    {
        for (int i = DefineParam.START_SCENE_ID; i < DefineParam.END_SCENE_ID; i++)
        {
            if(scene.name == ((DefineParam.SCENE_ID)i).ToString())
            {
                return (DefineParam.SCENE_ID)i;
            }
        }
        return DefineParam.SCENE_ID.Invalid;
    }

    public void AddRefreshUserInfoListener(UnityAction action)
    {
        refreshUserInfo.AddListener(action);
    }

    public void InvokeRefreshUserInfoListener(string publicProfile)
    {
        Application.userDataManager.RefreshProfile(publicProfile);
        refreshUserInfo.Invoke();
    }

    public void RemoveRefreshUserInfoListener(UnityAction action)
    {
        refreshUserInfo.RemoveListener(action);
    }
}