using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Mirror;

//Å³ ¹üÀ§, ETaskBar ¾ÈÇÔ
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
    
    //À¯ºZ 11 0:38ÃÊ
    [SyncVar(hook = nameof(SetRolePlayerCount_Hook))]
    private int rolePlayerCount; // À¯ºZ 11
    public void SetRolePlayerCount_Hook(int _, int value)
    {
        UpdateGameRuleOverview();
    }
    [SyncVar(hook = nameof(SetRoleTrainTime_Hook))]
    private int roleTrainTime; // À¯ºZ 11
    public void SetRoleTrainTime_Hook(int _, int value) {
        UpdateGameRuleOverview();
    }

    [SerializeField]
    private Text gameRuleOverview;

    public void UpdateGameRuleOverview() {
        var manager = NetworkManager.singleton as MafiaRoomManager;
        StringBuilder sb = new StringBuilder();
        sb.Append($"¿­Â÷ µµÂø ½Ã°£ : {roleTrainTime}\n");
        sb.Append($"ÃÖ´ë ÇÃ·¹ÀÌ¾î ¼ö : {rolePlayerCount}\n");  // À¯ºZ11¹ø manager.playerCount -> playerCount
        sb.Append($"ÃÑ¾Ë È¹µæ ÀÓ¹« ¼ö : {missionBullet}\n");
        sb.Append($"Ä¡·á¾à È¹µæ ÀÓ¹« ¼ö : {missionMedic}\n");
        sb.Append($"Å³ ÄðÅ¸ÀÓ : {killTime}\n");
        gameRuleOverview.text = sb.ToString();
    }

    private void SetDefaultGameRule() {
        missionBullet = 3;
        missionMedic = 2;
        killTime = 2;
    }
    // Start is called before the first frame update
    void Start() {
        //ÀÏ´Ü ÄÑÁÖ±â(È£½ºÆ® Á¶°Ç ¾ÈÁÜ)
        SetDefaultGameRule();
        UpdateGameRuleOverview();
        
        if (isServer) // À¯ºZ11 0:43 //¿À·ù
        {
            var manager = NetworkManager.singleton as MafiaRoomManager; //À¯ºZ 11 0:51
            rolePlayerCount = manager.playerCount; //À¯ºZ11 0:51
            roleTrainTime = manager.trainTime; //À¯ºZ11 0:51
            SetDefaultGameRule(); //SetRecommendGameRule(); // ¿À·ù
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
