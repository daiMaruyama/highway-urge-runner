using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class RaycastFPS : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 100f;
    [SerializeField] private LayerMask _rayLayerMask;
    [SerializeField] EnumTest _currentState;
    private int _missCount = 0;
    private int _successCount = 0;
    [SerializeField] private int _maxMissCount = 5;
    [SerializeField] private int _maxSuccessCount = 10;
    [SerializeField] private Text _statusText;

    void Update()
    {
        if (_currentState != EnumTest.Playing) return; // Playing状態以外は処理しない
    
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
            _successCount++;
        }
        else
        {
            _missCount++;
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _rayLayerMask))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            hit.collider.gameObject.SetActive(false);
            StartCoroutine(Respawn(hit.collider.gameObject, 1f));
        }
    }

    IEnumerator Respawn(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.SetActive(true); // OnEnableでランダム位置に移動する
        _currentState = EnumTest.Playing; // 状態をPlayingに変更
    }
}