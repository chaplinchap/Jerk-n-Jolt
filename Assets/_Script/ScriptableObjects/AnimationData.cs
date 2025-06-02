using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Animations")]
public class AnimationData : ScriptableObject
{
	[SerializeField] private AnimationClip _idle;
	[SerializeField] private AnimationClip _jumping;
	[SerializeField] private AnimationClip _running;
	[SerializeField] private AnimationClip _attack;
	[SerializeField] private AnimationClip _charge;
	[SerializeField] private AnimationClip _chargeJumping;
	[SerializeField] private AnimationClip _chargeRunning;
	[SerializeField] private AnimationClip _attackJumping;
	[SerializeField] private AnimationClip _stun;
	[SerializeField] private AnimationClip _dashing;
	[SerializeField] private AnimationClip _chargeRunningPenalty;
	[SerializeField] private AnimationClip _spawning;
	[SerializeField] private AnimationClip[] _beenHit;
	[SerializeField] private AnimationClip[] _winning;

	private int _jumpingHash;
	private int _idleHash;
	private int _runningHash;
	private int _attackHash;
	private int _chargeHash;
	private int _chargeJumpingHash;
	private int _chargeRunningHash;
	private int _attackJumpingHash;
	private int _stunHash;
	private int _dashingHash;
	private int _chargeRunningPenaltyHash;
	private int _spawningHash;
	private int[] _beenHitHash;
	private int[] _winningHash;

	public int Idle { get => _idleHash; }
	public int Jumping { get => _jumpingHash; }
	public int Running { get => _runningHash; }
	public int Attack { get => _attackHash; }
	public int Charge { get => _chargeHash; }
	public int ChargeJumping { get => _chargeJumpingHash; }
	public int ChargeRunning { get => _chargeRunningHash; }
	public int AttackJumping { get => _attackJumpingHash; }
	public int Stun { get => _stunHash; }
	public int Dashing { get => _dashingHash; }
	public int ChargeRunningPenalty { get => _chargeRunningPenaltyHash; }
	public int Spawning { get => _spawningHash; }
	public int BeenHit { get => _beenHitHash[Random.Range(0, _beenHitHash.Length)]; }
	public int Winning { get => _winningHash[Random.Range(0, _winningHash.Length)]; }

	public void Init()
	{
		_jumpingHash = HashAnimation(_jumping);
		_idleHash = HashAnimation(_idle);
		_runningHash = HashAnimation(_running);
		_attackHash = HashAnimation(_attack);
		_chargeHash = HashAnimation(_charge);
		_chargeJumpingHash = HashAnimation(_chargeJumping);
		_chargeRunningHash = HashAnimation(_chargeRunning);
		_attackJumpingHash = HashAnimation(_attackJumping);
		_chargeRunningPenaltyHash = HashAnimation(_chargeRunningPenalty); 
		_stunHash = HashAnimation(_stun);
		_dashingHash = HashAnimation(_dashing);
		_spawningHash = HashAnimation(_spawning);
		_beenHitHash = HashAnimation(_beenHit);
		_winningHash = HashAnimation(_winning);
	}


	private int HashAnimation(AnimationClip clip)
	{
		if (clip == null) return 0;
		return Animator.StringToHash(clip.name);
	}

	private int[] HashAnimation(AnimationClip[] clips) 
	{
		int[] result = new int[clips.Length];
		for (int i = 0; i < clips.Length; i++) 
		{
			result[i] = HashAnimation(clips[i]);
		}
		return result;
	}

}



