using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _isApplicationQuitting = false;

    public static T Instance
    {
        get
        {
            // 1. 애플리케이션이 종료 중이면 인스턴스를 새로 만들지 않음
            if (_isApplicationQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        // 2. [핵심] 게임이 실행 중일 때만 새 오브젝트를 생성함
                        // 에디터 모드이거나 종료 중일 때는 생성하지 않도록 방어
                        if (Application.isPlaying)
                        {
                            GameObject singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            // 3. 실행 중일 때만 파괴 방지 설정
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (transform.parent == null) // 부모가 없을 때만 DontDestroyOnLoad 가능
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 앱 종료 시 플래그 설정
    protected virtual void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        // 종료 중이 아닐 때만 인스턴스 해제
        if (_instance == this)
        {
            _instance = null;
        }
    }
}