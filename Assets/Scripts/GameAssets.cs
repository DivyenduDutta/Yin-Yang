using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Sprite shipTiltLeftLight;
    public Sprite shipNeutralLight;
    public Sprite shipTiltRightLight;

    public Sprite shipTiltLeftDark;
    public Sprite shipNeutralDark;
    public Sprite shipTiltRightDark;

    public Sprite point_5_light;
    public Sprite point_5_dark;
    public Sprite point_20_light;
    public Sprite point_20_dark;

    public Transform lightToDarkParSys;
    public Transform darkToLightParSys;
    public Transform enemyDamageParSys;
    public Transform enemyDeathParSys;

    public Transform whiteBullet;
    public Transform blackBullet;

    public Transform whiteEnemyBullet;
    public Transform blackEnemyBullet;

    public Transform whiteEnemyBulletType2;
    public Transform blackEnemyBulletType2;

    public Transform pointLight;
    public Transform pointDark;

    public Transform enemyType1LTR_light;
    public Transform enemyType1RTL_light;
    public Transform enemyType1LTR_dark;
    public Transform enemyType1RTL_dark;

    public Transform pointMultiplier;

    //game events
    public GameObject preGameEvent1Tutorial;
    public GameObject gameEvent1;
    public GameObject parallelEnemyEvent;
    public GameObject curtainEnemyEvent;

    public GameObject bossEnemyEvent;

    public Sprite bossGunLight;
    public Sprite bossGunDark;

    public Sprite bossP2EnemyLight;
    public Sprite bossP2EnemyDark;

    //Audio related assets
    [Serializable]
    public class SoundAudioClip
    {
        public AudioHandler.Sound sound;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] soundClipArray;


    public static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
}
