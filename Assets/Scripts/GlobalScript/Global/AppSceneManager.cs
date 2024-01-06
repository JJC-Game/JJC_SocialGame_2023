using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AppSceneManager : MonoBehaviour
{
    public DefineParam.SCENE_ID currentSceneId;

    // Scenario再生用.
    public int currentScenarioId;
    public int currentCharaId;
    UnityAction scenarioPopOutCallbackFunc;
    public DefineParam.SCENE_ID scenarioPopOutSceneId;

    // UI更新用.
    UnityEvent refreshUserInfo = new UnityEvent();

    // Battle再生用.
    enum BattleResult
    {
        Win,
        Lose
    }
    BattleResult battleResult;
    public int currentBattleId = 1;
    public DefineParam.BATTLE_WAVE currentBattleWave = DefineParam.BATTLE_WAVE.WAVE_1;

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

    public void BattleWin()
    {
        battleResult = BattleResult.Win;
        ChangeScene(DefineParam.SCENE_ID.Result);
    }
    public void BattleLose()
    {
        battleResult = BattleResult.Lose;
        ChangeScene(DefineParam.SCENE_ID.Result);
    }

    public bool IsBattleWin()
    {
        return battleResult == BattleResult.Win;
    }
    public bool IsBattleLose()
    {
        return battleResult == BattleResult.Lose;
    }

    public void SetBattleId(int battleId)
    {
        currentBattleId = battleId;
    }

    public int GetBattleId()
    {
        return currentBattleId;
    }

    public void SetBattleWave(DefineParam.BATTLE_WAVE battleWave)
    {
        currentBattleWave = battleWave;
    }

    public DefineParam.BATTLE_WAVE GetBattleWave()
    {
        return currentBattleWave;
    }
}
