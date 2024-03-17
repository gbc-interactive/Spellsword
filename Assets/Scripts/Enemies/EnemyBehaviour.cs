using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class AbilityForAI
{
    public AbilityBase ability;

    [HideInInspector] public float cooldownCurrentCount;
    public float cooldownMaxCount;

    [HideInInspector] public float chargeUpCurrentCount;
    public float chargeUpMaxCount;
}

namespace Spellsword
{
    public class EnemyBehaviour : CharacterBase, ITriggerCallbackable
    {
        private GameObject _playerTarget;
        public GameObject _getPlayerTarget { get { return _playerTarget; } }

        private BaseAIBehaviour _behaviour;
        [HideInInspector] public NavMeshAgent _navAgent;

        [HideInInspector] public Vector3 _homePosition;
        [HideInInspector] public Vector3 _randomPosition;
        [HideInInspector] public bool _moveClockwise;

        [Header("Stats")]
        [Range(0.1f, 1.0f)] public float _circleSpeedScale; //FIXME circleSpeedScale doesn't work right now
        [SerializeField] public List<AbilityForAI> _abilities = new List<AbilityForAI>();

        [Header("Zones Distances")]
        [SerializeField] public float _safeZoneDistanceMax;
        [SerializeField] public float _safeZoneDistanceMin;

        [SerializeField] Slider HPBar;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Start()
        {
            _homePosition = transform.position;
            _behaviour = BehavioursAI.idle;

            SetMaxHP(30.0f);
        }

        private void Update()
        {
            UpdateStatusEffects();

            if (_navAgent.velocity.x > 0.05f)
                SetFacingDirection(EDirection.Right);

            else if (_navAgent.velocity.x < -0.05f)
                SetFacingDirection(EDirection.Left);
        }

        protected virtual void FixedUpdate()
        {
            HPBar.transform.rotation = Camera.main.transform.rotation;
            ChargeCooldowns();
            DetermineBehaviour();
            RunBehaviour();
        }

        private void ChargeCooldowns()
        {
            foreach (AbilityForAI ability in _abilities)
            {
                if (ability.cooldownCurrentCount > ability.cooldownMaxCount)
                    return;

                ability.cooldownCurrentCount += Time.fixedDeltaTime;
            }
        }

        protected void RunBehaviour()
        {
            _behaviour.UpdateBehaviour(this);

        }

        protected void SwitchBehaviour(BaseAIBehaviour newBehaviour)
        {
            if (_behaviour == newBehaviour)
                return;

            if (_behaviour != null)
                _behaviour.ExitBehaviour(this);

            _behaviour = newBehaviour;
            _behaviour.EnterBehaviour(this);
        }

        protected virtual void DetermineBehaviour()
        {

        }

        public void ResetAttackCooldowns()
        {
            StunStatusEffect stunEffect = new StunStatusEffect();
            stunEffect.ApplyEffect(this, 0.5f, 0.5f);

            foreach (AbilityForAI ability in _abilities)
            {
                ability.cooldownCurrentCount = 0.0f;
                ability.chargeUpCurrentCount = 0.0f;
            }
        }

        public void SetMaxHP(float hp)
        {
            _currentHP = hp;
            HPBar.maxValue = hp;
            HPBar.value = HPBar.maxValue;
        }

        public override bool TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            HPBar.value = _currentHP;
            return true;
        }

        public void TriggerEnterCallback(Collider other)
        {
            if (other.tag != "Player") return;
            _playerTarget = other.gameObject;
        }


        public void TriggerExitCallback(Collider other)
        {
            if (other.tag == "Player")
                _playerTarget = null;
        }

        public override void RegenHP()
        {
            base.RegenHP();
            HPBar.value = _currentHP;
        }

        public override void Die()
        {
            base.Die();
            StartCoroutine(Death());
        }

        IEnumerator Death()
        {
            gameObject.GetComponent<EnemyBehaviour>().enabled = false;
            HPBar.gameObject.SetActive(false);
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }

    }

}