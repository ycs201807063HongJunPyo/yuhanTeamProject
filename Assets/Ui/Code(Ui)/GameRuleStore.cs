using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Mirror;

//킬 범위, ETaskBar 안함
public struct GameRuleData {
    public int missionBullet;
    public int missionMedic;
    public int killTime;
}

public class GameRuleStore : NetworkBehaviour {
    [SyncVar]
    private int missionBullet;
    [SerializeField]
    private Text missionBulletText;
    public void SetMissionBulletText(int value) {
        //missionBulletText.text = value.ToString();
        missionBulletText.text = string.Format("{0}", value);
        UpdateGameRuleOverview();
    }
    public void OnMissionBulletChange(bool isPlus) {
        missionBullet = Mathf.Clamp(missionBullet + (isPlus ? 1 : -1), 1, 5);
        SetMissionBulletText(missionBullet);
    }
    [SyncVar]
    private int missionMedic;
    [SerializeField]
    private Text missionMedicText;
    public void SetMissionMedicText(int value) {
        missionMedicText.text = value.ToString();
        //missionMedicText.text = string.Format("{0}", value);
        UpdateGameRuleOverview();
    }
    public void OnMissionMedicChange(bool isPlus) {
        missionMedic = Mathf.Clamp(missionMedic + (isPlus ? 1 : -1), 1, 3);
        SetMissionMedicText(missionMedic);
    }
    [SyncVar]
    private int killTime;
    [SerializeField]
    private Text killTimeText;
    public void SetKillTimeText(int value) {
        //killTimeText.text = value.ToString();
        killTimeText.text = string.Format("{0}", value);
        UpdateGameRuleOverview();
    }
    public void OnKillTimeChange(bool isPlus) {
        killTime = Mathf.Clamp(killTime + (isPlus ? 1 : -1), 2, 5);
        SetKillTimeText(killTime);
    }
    [SerializeField]
    private Text gameRuleOverview;

    public void UpdateGameRuleOverview() {
        var manager = NetworkManager.singleton as MafiaRoomManager;
        StringBuilder sb = new StringBuilder();
        sb.Append($"열차 도착 시간 : {manager.trainTime}\n");
        sb.Append($"최대 플레이어 수 : {manager.playerCount}\n");
        sb.Append($"총알 획득 임무 수 : {missionBullet}\n");
        sb.Append($"치료약 획득 임무 수 : {missionMedic}\n");
        sb.Append($"킬 쿨타임 : {killTime}\n");
        gameRuleOverview.text = sb.ToString();
    }

    private void SetDefaultGameRule() {
        missionBullet = 3;
        missionMedic = 2;
        killTime = 2;
    }
    // Start is called before the first frame update
    void Start() {
        //일단 켜주기(호스트 조건 안줌)
        SetDefaultGameRule();
        UpdateGameRuleOverview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
