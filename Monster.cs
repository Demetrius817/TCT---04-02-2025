using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour {

    public static Monster Instance { get; private set; }

    [SerializeField] StageData stageData;


    [Header("MonsterMovement")]
    [SerializeField] private float MissMonsterRange;

    [SerializeField] private float downmovementSpeed;
    [SerializeField] private float MonstercooldownTime;
    private bool WaitingTimeFinish = false;
    public int dist;


    [Header("References")]
    [SerializeField] Collider playerCollider;
    [SerializeField] Collider monsterCollider;
    private UI_Manager uiManager;


    [SerializeField] public ScriptableRendererFeature _fullscreeneffectMonsterCloseSE;
    [SerializeField] private Material _materialMonsterCloseSE;
    private int _MonsterCLoseSE = Shader.PropertyToID("_Vignette_Intensity");
    private const int MonsterCloseSE_START_AMOUNT = 0;




    private void Awake() {
        Instance = this;
    } 


    private void Start() {
        uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        StartCoroutine(MonsterMovingCooldown());

     //   _materialMonsterCloseSE.SetFloat(_MonsterCLoseSE, MonsterCloseSE_START_AMOUNT);


    }

    public void Update() {
        //moves monster foward
        if (GameHandler.Instance.isGamePlaying())
        {
          //  if (WaitingTimeFinish == true) {
                transform.position -= transform.forward * Time.deltaTime * stageData.monsterSpeed;
                transform.position -= transform.up * Time.deltaTime * downmovementSpeed;
            Debug.Log(stageData.monsterSpeed);
          //  }
        }


        if (playerCollider == null || monsterCollider == null) return;

        Bounds bounds0 = playerCollider.bounds;
        Bounds bounds1 = monsterCollider.bounds;

        float sd0 = sdBounds(bounds0.center, bounds1, out Vector3 conjecture0);
        float sd1 = sdBounds(bounds1.center, bounds0, out Vector3 conjecture1);
         dist = (int)Vector3.Distance(conjecture0, conjecture1);

        //  Gizmos.color = new Color(0, 1, 1, 0.2f);
        //   Gizmos.DrawCube(bounds0.center, bounds0.size); Gizmos.DrawCube(bounds1.center, bounds1.size);
        //  Gizmos.DrawWireCube(bounds0.center, bounds0.size); Gizmos.DrawWireCube(bounds1.center, bounds1.size);
        // print("Distance to other: " + dist);
        uiManager.UpdateMonsterDistance(dist);

        //The MonsterCloseScreenEffect start coming in the closer the monster gets.
       /*  int Minmin = 0;
         int Maxmax = 45;

         int test = (int)Mathf.Lerp(Maxmax, Minmin, dist);
         float t = Mathf.InverseLerp(Maxmax, Minmin, dist);
         float lerpedMonsterCloseSE = Mathf.Lerp(MonsterCloseSE_START_AMOUNT, 1.5f, t);

        // float lerpedMonsterCloseSE = Mathf.Lerp(MonsterCloseSE_START_AMOUNT, 1.25f, test);

         _materialMonsterCloseSE.SetFloat(_MonsterCLoseSE, lerpedMonsterCloseSE);
         */


    }
    public void MissMonsterMove() {

        // move foward
        transform.position -= new Vector3(0, 0, MissMonsterRange);

    }



    static float sdBounds(Vector3 point, Bounds bounds, out Vector3 contact) {
        Vector3 dir = point - bounds.center;
        float sd = sdBox(dir, bounds.extents);

        contact = point - dir.normalized * sd;
        // note: we dont need to know the real contact point in this case, this is pure conjecture

        return sd;
    }

    // src: https://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm
    static float sdBox(Vector3 p, Vector3 b) {
        Vector3 q = new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z)) - b;
        return Vector3.Magnitude(Vector3.Max(q, Vector3.zero)) + Mathf.Min(Mathf.Max(q.x, Mathf.Max(q.y, q.z)), 0f);
    }

    private IEnumerator MonsterMovingCooldown() {
        WaitingTimeFinish = false;
        yield return new WaitForSeconds(MonstercooldownTime);
        WaitingTimeFinish = true;
    }


}


