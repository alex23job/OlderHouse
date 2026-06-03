using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SwitchCamera _switchCamera;
    [SerializeField, Range(0, 180f)] private float _rotationSmoothness;    // Коэффициент плавности поворота
    [SerializeField] private float _moveSpeed = 5f;              //                  
    [SerializeField] private float _jumpForce = 10f;             //            
    private Rigidbody _rb;                       // Rigidbody
    private Animator _anim;
    private bool _isGrounded;
    private bool _isJump = false;
    private bool _isCarriasBox = false;
    private bool _isCarriasTrash = false;
    private Vector3 _movement = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _oldPos = Vector3.zero;

    private float _hor, _ver;
    private float _timer = 0.25f;
    private float _myVelocity = 0;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hor = UnityEngine.Input.GetAxis("Horizontal");
        _ver = UnityEngine.Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _switchCamera.CameraSwitch();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.currentPlayer.myCommand != null)
            {

                GameManager.Instance.currentPlayer.oldPosition = transform.position;
                GameManager.Instance.currentPlayer.oldRotation = transform.rotation.eulerAngles;
                GameManager.Instance.SaveGame();
                GameManager.Instance.currentPlayer.myCommand.Execute();
            }
        }
        if (_timer > 0) { _timer -= Time.deltaTime; }
        else
        {
            _timer = 0.25f;
            if (_isCarriasBox == false && _isCarriasTrash == false)
            {
                if (_myVelocity > 0.2f)
                {
                    _anim.SetBool("IsWalk", true);
                }
                else
                {
                    _anim.SetBool("IsWalk", false);
                }
            }
            _myVelocity = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(movement, ForceMode.Impulse);
        Move(_ver);
        Turn(_hor);
        Jump();
    }
    private void Move(float input)
    {
        //if (input > 0.99f) runStart += Time.deltaTime;
        //else runStart = 0;
        //if (runStart < 3f) input = (input > 0.95f) ? 0.9f : input;
        float mult = 1f;
        //if (runStart >= 3f) mult = 2f;
        //transform.Translate(Vector3.forward * input * moveSpeed * mult * Time.fixedDeltaTime);//Можно добавить Time.DeltaTime
        //_rb.AddForce(transform.forward * input * _moveSpeed * mult * Time.fixedDeltaTime);
        _rb.AddForce(transform.forward * input * _moveSpeed * mult);
        //anim.SetFloat("speed", Mathf.Abs(input));
        _myVelocity += Mathf.Abs(input);
        LimitVelocity(_moveSpeed);
    }
    void LimitVelocity(float maxVel)
    {
        // Ограничиваем скорость до заданного максимума
        if (_rb.linearVelocity.magnitude > maxVel)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxVel;
        }
    }

    private void Turn(float input)
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, input * _rotationSmoothness * Time.fixedDeltaTime, 0));
        //transform.Rotate(0, input * _rotationSmoothness * Time.deltaTime, 0);
    }

    private void Jump()
    {
        if (IsGrounded() && _isJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _isJump = false;
    }

    //                             
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, ~0, QueryTriggerInteraction.Ignore);
    }

    public void SetQuestObject()
    {

    }
}
