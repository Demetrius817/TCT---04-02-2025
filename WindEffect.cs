using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WindEffect : MonoBehaviour {

    [Header("Player")]
    private Rigidbody rb;
    [SerializeField] private GameObject ObjRB;


    [Header("References")]
    [SerializeField] public ScriptableRendererFeature _fullscreeneffectWindEffect;
    [SerializeField] private Material _materialWindEffect;
    private int _WindEffectAlpha = Shader.PropertyToID("_Alpha");
    private int _WindEffectMaskSize = Shader.PropertyToID("_Mask_Size");
    IEnumerator myCoroutineTwo;


    [Header("Amounts")]
    private const int WindEffectAlpha_START_AMOUNT = 0;
    private const int WindEffectMaskSize_START_AMOUNT = 1;
    [SerializeField] private float windTimeChange = 0;
    [SerializeField] private float _WindAlphaFadeInTime = 1f;


    [SerializeField] float idlesetSpeed = 16f;

    [Header("BOOL")]
    [SerializeField] private bool isFalling = false;
    private bool startWindEffect;
    [SerializeField] private bool windScreenEffectDone = false;
    [SerializeField] private bool stopWindOnPlayer = false;




    void Start() {
        _fullscreeneffectWindEffect.SetActive(true);

        myCoroutineTwo = startWindonPlayer();
        rb = ObjRB.GetComponent<Rigidbody>();
        _materialWindEffect.SetFloat(_WindEffectAlpha, WindEffectAlpha_START_AMOUNT);
        _materialWindEffect.SetFloat(_WindEffectMaskSize, WindEffectMaskSize_START_AMOUNT);

    }

    // Update is called once per frame
    void Update() {

        // For Player Wind
        if (rb.velocity.magnitude > idlesetSpeed) {
            isFalling = true;
        } else {
            isFalling = false;
        }

        if (Vector3.Angle(Camera.main.transform.forward, Vector3.down) <= 90.0 && isFalling == true || Vector3.Angle(Camera.main.transform.forward, Vector3.down) <= 90.0 && PlayerSlide.Instance.isSliding) {
            // Do stuff
            startWindEffect = true;
            Debug.Log(" working");
        } else {
            startWindEffect = false;

            _materialWindEffect.SetFloat(_WindEffectAlpha, WindEffectAlpha_START_AMOUNT);
            _materialWindEffect.SetFloat(_WindEffectMaskSize, WindEffectMaskSize_START_AMOUNT);

        }
        if (startWindEffect == true && windScreenEffectDone == false) {
            StartCoroutine(myCoroutineTwo);
            windScreenEffectDone = true;
        }

        if (startWindEffect == false) {
            StopCoroutine(myCoroutineTwo);
            windTimeChange = 0;
            windScreenEffectDone = false;
            stopWindOnPlayer = false;
            _materialWindEffect.SetFloat(_WindEffectAlpha, WindEffectAlpha_START_AMOUNT);
            _materialWindEffect.SetFloat(_WindEffectMaskSize, WindEffectMaskSize_START_AMOUNT);
        } else if (stopWindOnPlayer == true) {
            StopCoroutine(myCoroutineTwo);
        }


    }
    private IEnumerator startWindonPlayer() {
        while (true) {


            stopWindOnPlayer = false;
            while (windTimeChange < _WindAlphaFadeInTime) {

                windTimeChange += Time.deltaTime;

                float lerpedAlpha = Mathf.Lerp(WindEffectAlpha_START_AMOUNT, 0.15f, (windTimeChange / _WindAlphaFadeInTime));
                float lerpedMaskSize = Mathf.Lerp(WindEffectMaskSize_START_AMOUNT, 0.65f, (windTimeChange / _WindAlphaFadeInTime));
                _materialWindEffect.SetFloat(_WindEffectAlpha, lerpedAlpha);
                _materialWindEffect.SetFloat(_WindEffectMaskSize, lerpedMaskSize);

                yield return null;
            }

            yield return new WaitForSeconds(_WindAlphaFadeInTime);
            stopWindOnPlayer = true;

        }
    }
    void OnApplicationQuit() {
        _fullscreeneffectWindEffect.SetActive(false);
    }

}
