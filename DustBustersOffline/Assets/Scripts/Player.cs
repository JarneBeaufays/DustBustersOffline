using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("-------Movement Settings-------")]
    [SerializeField] private float _movementSpeed;
    [SerializeField, Range(0, 1)] private float _InstantMovementPercentage = 0.1f;

    [Header("-------Mesh Settings-------")]
    [SerializeField] private GameObject[] _meshStates;
    [SerializeField] private int[] _meshStateScores;
    [SerializeField] private float _meshGrowth = 0.6f;
    [SerializeField] private float _meshGrowthSpeed = 1f;
    [SerializeField] private int _scoreToStopGrowing = 15;

    [Header("-------Projectile Settings-------")]
    [SerializeField] private GameObject _lightningProjectilePrefab = null;
    [SerializeField] private Transform _projectileSocket = null;

    [Header("-------Other Settings-------")]
    [SerializeField] private int _maxSCore = 10;
    [SerializeField] private int _playerId = 0;
    [SerializeField] private GameObject _electricParticleParent = null;
    [SerializeField] private Material _carpetMaterial = null;
    [SerializeField] private PlayerColors _playerColors = null;
    [SerializeField] private List<SpriteRenderer> _bodySprites = new List<SpriteRenderer>();
    [SerializeField] private GameObject _uiElement = null;
    [SerializeField] private int _winScore = 2;

    //private TextMeshProUGUI _totalScoreText = null;
    //private TextMeshProUGUI _currentScoreText = null;
    private List<ParticleSystem> _particles = new List<ParticleSystem>();

    private CharacterController _characterController = null;

    private Animator[] _animator;

    private TextMeshProUGUI _curScore;
    private TextMeshProUGUI _totScore;

    private Vector3 _faceDirection = Vector3.zero;
    private Vector2 _input = Vector2.zero;
    private Vector2 _lookInput = Vector2.zero;
    private int _score = 0;
    private int _totalScore = 0;
    private int _currentMeshStateIndex = 0;
    private int _colorId = 0;
    private bool _isCharged = false;
    private bool _firstFrame = true;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int TotalScore { get { return _totalScore; } }
    public bool IsBeingVacuumed { get; set; }

    private void Start()
    {
        _animator = GetComponentsInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        _faceDirection = transform.forward;

        _particles.Add(_electricParticleParent.GetComponent<ParticleSystem>());
        foreach (ParticleSystem ps in _electricParticleParent.GetComponentsInChildren<ParticleSystem>()) _particles.Add(ps);

        _meshStates[0].SetActive(true);
        for (int i = 1; i < _meshStates.Length; i++)
        {
            _meshStates[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (_firstFrame) 
        {
            GameObject ui = Instantiate(_uiElement);
            GameObject scoreListing = GameObject.Find("ScoreListing");
            ui.transform.SetParent(scoreListing.transform);
            TextMeshProUGUI[] texts = ui.GetComponentsInChildren<TextMeshProUGUI>();

            _totScore = texts[1];
            _totScore.color = _playerColors._colors[_colorId];
            _curScore = texts[3];
            _curScore.color = _playerColors._colors[_colorId];

            _firstFrame = false;
            return;
        }

        _totScore.text = _totalScore.ToString();
        _curScore.text = _score.ToString();
    }

    void FixedUpdate()
    {
        HandleMovement();
        UpdateMaterial();

        _animator[_currentMeshStateIndex].SetBool("isRunning", _input != Vector2.zero);
    }
    
    public void UpdateColors(int colorId) 
    {
        foreach (SpriteRenderer sprite in _bodySprites)
            sprite.color = _playerColors._colors[colorId];

        _colorId = colorId;
    }

    public void Movement(InputAction.CallbackContext context) 
    {
        if (!context.performed) return;

        _input = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _lookInput = context.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector2 movementInput = _input;
        if (movementInput != Vector2.zero)
            _faceDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        Vector3 currentVel = _characterController.velocity;
        Vector3 movement = Vector3.Lerp(new Vector3(currentVel.x, 0, currentVel.z), new Vector3(movementInput.x, 0, movementInput.y) * _movementSpeed, _InstantMovementPercentage);

        _characterController.SimpleMove(movement);
    }

    private void UpdateMaterial()
    {
        _carpetMaterial.SetVector("_PlayerPosition" + _playerId, _characterController.transform.position);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (_isCharged)
        {
            if(_lookInput == Vector2.zero) 
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                Physics.Raycast(ray, out hitInfo);

                Vector3 mousePosOnBoard = hitInfo.point;
                mousePosOnBoard.y = _projectileSocket.transform.position.y;

                GameObject obj = Instantiate(_lightningProjectilePrefab, _projectileSocket.position, Quaternion.LookRotation(mousePosOnBoard - _projectileSocket.transform.position));
                obj.GetComponent<LightningProjectile>().Shooter = this.gameObject;
            }
            else 
            {
                Vector3 target = new Vector3();
                target.x = _projectileSocket.position.x + _lookInput.x;
                target.y = _projectileSocket.position.y;
                target.z = _projectileSocket.position.z + _lookInput.y;

                GameObject obj = Instantiate(_lightningProjectilePrefab, _projectileSocket.position, Quaternion.LookRotation(target - _projectileSocket.transform.position));
                obj.GetComponent<LightningProjectile>().Shooter = this.gameObject;
            }

            _isCharged = false;
            foreach (ParticleSystem ps in _particles) ps.Stop();
        }     
    }

    public void DustPickedUp(int amount)
    {
        if (_score >= _maxSCore)
            return;

        int amountAdded = _score + amount > _maxSCore ? _maxSCore - _score : amount;
        _score = Mathf.Min(_score + amount, _maxSCore);

        while (_currentMeshStateIndex != _meshStateScores.Length - 1 && _meshStateScores[_currentMeshStateIndex + 1] <= _score)
        {
            _meshStates[_currentMeshStateIndex].SetActive(false);
            Vector3 scale = _meshStates[_currentMeshStateIndex].transform.localScale;
            ++_currentMeshStateIndex;
            _meshStates[_currentMeshStateIndex].SetActive(true);
            _meshStates[_currentMeshStateIndex].transform.localScale = scale;
        }

        _meshStates[_currentMeshStateIndex].transform.localScale += new Vector3(_meshGrowth, _meshGrowth, _meshGrowth) * amountAdded;  
    }

    public void TakeDustOff(int amount)
    {
        if (_score <= 0)
            return;

        int amountSubtracted = _score < amount ? _score : amount;
        _score = Mathf.Max(0, _score - amount);

        while (_currentMeshStateIndex != 0 && _meshStateScores[_currentMeshStateIndex] > _score)
        {
            _meshStates[_currentMeshStateIndex].SetActive(false);
            Vector3 scale = _meshStates[_currentMeshStateIndex].transform.localScale;
            --_currentMeshStateIndex;
            _meshStates[_currentMeshStateIndex].SetActive(true);
            _meshStates[_currentMeshStateIndex].transform.localScale = scale;
        }
   
        _meshStates[_currentMeshStateIndex].transform.localScale -= new Vector3(_meshGrowth, _meshGrowth, _meshGrowth) * amountSubtracted;      
    }

    public void Vacuum() 
    {
        _totalScore += _score;
        TakeDustOff(_score);

        IsBeingVacuumed = false;

        transform.position = new Vector3(0f, 10f, 0f);
        transform.rotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) Destroy(rb);

        _characterController.enabled = true;

<<<<<<< HEAD
        //if (_totalScore >= _winScore)
            //GameObject.FindObjectOfType<EndGame>().WinnerWinnerChickenDinner();
=======
        if (_totalScore >= _winScore)
            GameObject.FindObjectOfType<EndGame>().WinnerWinnerChickenDinner(GetComponent<PlayerColor>().PlayerId, _playerColors._colors[_colorId]);
<<<<<<< HEAD
>>>>>>> 07d64f15e1f6336b803e7740e0c9296680826328
=======
>>>>>>> 07d64f15e1f6336b803e7740e0c9296680826328
    }

    public void Charge() 
    {
        foreach (ParticleSystem ps in _particles) ps.Play();
        _isCharged = true;
    }

    IEnumerator Shrink(int amount)
    {        
        int score = _score + amount;
        for (int i = 0; i < amount; i++)
        {
            if (score < 0)
                yield return null;

            --score;
            if (_currentMeshStateIndex != 0 && _meshStateScores[_currentMeshStateIndex] > score)
            {
                _meshStates[_currentMeshStateIndex].SetActive(false);
                Vector3 scale = _meshStates[_currentMeshStateIndex].transform.localScale;
                --_currentMeshStateIndex;
                _meshStates[_currentMeshStateIndex].SetActive(true);
                _meshStates[_currentMeshStateIndex].transform.localScale = scale;
            }
            else
            {
                float elapsedTime = 0;
                Vector3 initialScale = _meshStates[_currentMeshStateIndex].transform.localScale;
                while (elapsedTime < _meshGrowthSpeed / amount)
                {
                    _meshStates[_currentMeshStateIndex].transform.localScale = Vector3.Lerp(initialScale * (1 - _meshGrowth), initialScale,
                        1 - elapsedTime / _meshGrowthSpeed * amount);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                _meshStates[_currentMeshStateIndex].transform.localScale = initialScale * (1 - _meshGrowth);
            }
        }  

        yield return null;
    }

    //Increases scale when not needing to change sprite, changes sprite when score threshhold is reached
    IEnumerator Grow(int amount)
    {
        if (_score < _scoreToStopGrowing)
        {
            int score = _score - amount;
            for (int i = 0; i < amount; i++)
            {

                ++score;
                if (_currentMeshStateIndex != _meshStateScores.Length - 1 && _meshStateScores[_currentMeshStateIndex + 1] <= score)
                {
                    _meshStates[_currentMeshStateIndex].SetActive(false);
                    Vector3 scale = _meshStates[_currentMeshStateIndex].transform.localScale;
                    ++_currentMeshStateIndex;
                    _meshStates[_currentMeshStateIndex].SetActive(true);
                    _meshStates[_currentMeshStateIndex].transform.localScale = scale;
                }
                else
                {
                    float elapsedTime = 0;
                    Vector3 initialScale = _meshStates[_currentMeshStateIndex].transform.localScale;
                    while (elapsedTime < _meshGrowthSpeed / amount)
                    {
                        _meshStates[_currentMeshStateIndex].transform.localScale = Vector3.Lerp(initialScale, initialScale * (1 + _meshGrowth),
                            elapsedTime / _meshGrowthSpeed * amount);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    _meshStates[_currentMeshStateIndex].transform.localScale = initialScale * (1 + _meshGrowth);
                }
            }
        }    
        
        yield return null;
    }
}
