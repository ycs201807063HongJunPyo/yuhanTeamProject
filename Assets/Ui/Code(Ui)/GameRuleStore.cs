using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Mirror;

public struct GameRuleData {
    public int missionBullet;
    public int missionMedic;
    public int killTime;
}

public class GameRuleStore : NetworkBehaviour {
    [SyncVar(hook = nameof(SetMissionBulletText_Hook))]
    private int missionBullet;
    [SerializeField]
    private Text missionBulletText;
    public void SetMissionBulletText_Hook(int _, int value) {
        missionBulletText.text = string.Format("{0}", value);
        UpdateGameRuleOverview();
    }
    public void OnMissionBulletChange(bool isPlus) {
        if (isServer) {
            missionBullet = Mathf.Clamp(missionBullet + (isPlus ? 1 : -1), 1, 5);
        }
    }

    [SyncVar(hook = nameof(SetMissionMedicText_Hook))]
    private int missionMedic;
    [SerializeField]
    private Text missionMedicText;
    public void SetMissionMedicText_Hook(int _, int value) {
        missionMedicText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnMissionMedicChange(bool isPlus) {
        if (isServer) {
            missionMedic = Mathf.Clamp(missionMedic + (isPlus ? 1 : -1), 1, 3);
        }
    }

    [SyncVar(hook = nameof(SetKillTimeText_Hook))]
    private int killTime;
    [SerializeField]
    private Text killTimeText;
    public void SetKillTimeText_Hook(int _, int value) {
        killTimeText.text = string.Format("{0}", value);
        UpdateGameRuleOverview();
    }
    public void OnKillTimeChange(bool isPlus) {
        if (isServer) {
            killTime = Mathf.Clamp(killTime + (isPlus ? 1 : -1), 2, 5);
        }
    }
    
    //���Z 11 0:38��
    [SyncVar(hook = nameof(SetRolePlayerCount_Hook))]
    private int rolePlayerCount; // ���Z 11
    public void SetRolePlayerCount_Hook(int _, int value)
    {
        UpdateGameRuleOverview();
    }
    [SyncVar(hook = nameof(SetRoleTrainTime_Hook))]
    private int roleTrainTime; // ���Z 11
    public void SetRoleTrainTime_Hook(int _, int value) {
        UpdateGameRuleOverview();
    }

    [SerializeField]
    private Text gameRuleOverview;

    public void UpdateGameRuleOverview() {
        var manager = NetworkManager.singleton as MafiaRoomManager;
        StringBuilder sb = new StringBuilder();
        sb.Append($"���� ���� �ð� : {roleTrainTime}\n");
        sb.Append($"�ִ� �÷��̾� �� : {rolePlayerCount}\n");  // 영상 11장 manager.playerCount -> playerCount
        sb.Append($"�Ѿ� ȹ�� �ӹ� �� : {missionBullet}\n");
        sb.Append($"ġ��� ȹ�� �ӹ� �� : {missionMedic}\n");
        sb.Append($"ų ��Ÿ�� : {killTime}\n");
        gameRuleOverview.text = sb.ToString();
    }

    private void SetDefaultGameRule() {
        missionBullet = 3;
        missionMedic = 2;
        killTime = 2;
    }
    // Start is called before the first frame update
    void Start() {
        SetDefaultGameRule();
        UpdateGameRuleOverview();
        
        if (isServer) // 영상 11장 0:43
        {
            var manager = NetworkManager.singleton as MafiaRoomManager; // 영상 11장 0:51
            rolePlayerCount = manager.playerCount; //영상 11장 0:51
            roleTrainTime = manager.trainTime; //영상 11장 0:51
            SetDefaultGameRule(); //SetRecommendGameRule();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
