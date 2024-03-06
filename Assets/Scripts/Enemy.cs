using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private bool _isDestroyed = false;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager.isCoopMode)
        {
            _player = GameObject.Find("Player_1").GetComponent<Player>();
        }
        else
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        _audioSource = GetComponent<AudioSource>();

        if (_player is null)
        {
            Debug.LogError("The Player is null");
        }

        _anim = GetComponent<Animator>();

        if (_anim is null)
        {
            Debug.LogError("The Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDestroyed)
        {
            CalculateMovement();

            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3f, 7f);
                _canFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
                foreach (var item in lasers)
                {
                    item.AssignEnemyLaser();
                }
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player is not null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _isDestroyed = true;
            _audioSource.Play();

            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject, 2.8f);

            if (_player is not null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _isDestroyed = true;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
}
