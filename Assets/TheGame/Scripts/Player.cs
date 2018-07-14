using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 0.05f;

    public float JumpPush = 6f;

    public float ExtraGravity = -20f;

    public GameObject Model;

    private float _towardsY = 0f;

    private Rigidbody _rigid;

    private Animator _animator;

    private bool _onGround = false;

    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        _animator.SetFloat("forward", Mathf.Abs(h));
        _animator.SetBool("grounded", _onGround);

        transform.position += h * Speed * transform.forward;

        if (h > 0f)
            _towardsY = 0f;
        else if (h < 0f)
            _towardsY = -180f;

        Model.transform.rotation =
            Quaternion.Lerp(Model.transform.rotation, Quaternion.Euler(0f, _towardsY, 0f), Time.deltaTime * 10f);

        RaycastHit hitInfo;
        _onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.12f);

        if (Input.GetAxis("Jump") > 0f && _onGround)
        {
            Vector3 power = _rigid.velocity;
            power.y = JumpPush;
            _rigid.velocity = power;
        }
        
        _rigid.AddForce(new Vector3(0f, ExtraGravity, 0f));
    }
}
