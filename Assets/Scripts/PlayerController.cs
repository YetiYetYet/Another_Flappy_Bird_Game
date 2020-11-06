using System.Collections;
using Audio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 1;
    private GameManager _gameManager;
    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _isDead;
    
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _isDead = false;
    }
    
    void Update()
    {
        if (!_isDead && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.Play("Plop");
            _rb.velocity = Vector2.up * velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) return;
        if(_isDead) return;
        _isDead = !_isDead;
        _animator.Play("Die");
        AudioManager.Instance.Play("Lost");
        AudioManager.Instance.MuteSoundUntilOtherIsFinish("MainTheme", "Lost");
        StartCoroutine(DieAnimation());
        
    }

    IEnumerator DieAnimation()
    {
        _animator.Play("Die");
        yield return 0; // Need to wait 1 frame before GetCurrentAnimatorClipInfo gets updated
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _gameManager.GameOver();
    }
}