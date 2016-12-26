﻿using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

namespace MoreMountains.CorgiEngine
{	
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class LevelMapCharacter : MonoBehaviour 
	{
		public float CharacterSpeed;
		public LevelMapPathElement StartingPoint;
		public bool CollidingWithAPathElement { get; set; }
		public bool CharacterIsFacingRight=true;
		public LevelMapPathElement LastVisitedPathElement { get; set; }

		protected LevelMapPathElement _destination;
		protected bool _shouldMove=false;
		protected float _horizontalMove;
		protected float _verticalMove;
		protected float _threshold=0.1f;
		protected Vector3 _offset;
		protected Vector3 _positionLastFrame;
		protected float _currentSpeed=0f;

		protected string _movement;
		protected LevelMapPathElement _currentPathElement;

		protected Rigidbody2D _rigidbody2D;
		protected BoxCollider2D _boxCollider2D;
		protected Animator _animator;

		/// <summary>
		/// Initialization
		/// </summary>
		protected virtual void Start () 
		{
			// we get our various components
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_boxCollider2D = GetComponent<BoxCollider2D>();
			_animator = GetComponent<Animator>();

			// we define the offset based on the distance between our object's position and the center of the boxcollider
			_offset = _boxCollider2D.bounds.center - transform.position;

			if (GameManager.Instance.StoredLevelMapPosition)
			{
				transform.position=GameManager.Instance.LevelMapPosition;
			}
			else
			{
				// we move our character to its starting point
				transform.position=StartingPoint.transform.position-_offset;
			}
		}

		/// <summary>
		/// On update, we get the input, decide if we should move or not, and if needed move the character and animate it
		/// </summary>
		protected virtual void Update () 
		{
			InputMovement();

			MoveCharacter();

			AnimateCharacter();

		}

		/// <summary>
		/// Handles input and decides if we can move or not
		/// </summary>
		/// <param name="movement">Movement.</param>
		public virtual void InputMovement()
		{
			// we get both direction axis
			_horizontalMove = CrossPlatformInputManager.GetAxis ("Horizontal");
			_verticalMove = CrossPlatformInputManager.GetAxis ("Vertical");

			if (!CollidingWithAPathElement)
			{
				return;
			}

			if (CrossPlatformInputManager.GetButtonDown ("Jump")) 
			{
				if (_currentPathElement.GetComponent<LevelSelector>()!=null)
				{
					if (GUIManager.Instance.GetComponent<LevelSelectorGUI>() != null)
					{
						GUIManager.Instance.GetComponent<LevelSelectorGUI>().TurnOffLevelName();
					}

					// we store the position for next time
					GameManager.Instance.StoredLevelMapPosition=true;
					GameManager.Instance.LevelMapPosition=transform.position;

					_currentPathElement.GetComponent<LevelSelector>().GoToLevel();
				}
			}

			_movement="";
			// if one or both axis values is above a small value
			if ( (Mathf.Abs(_horizontalMove)>_threshold) || (Mathf.Abs(_verticalMove)>_threshold) )
			{
				if (_horizontalMove>_threshold)
					_movement="Right";
				if (_horizontalMove<-_threshold)
					_movement="Left";
				if (_verticalMove>_threshold)
					_movement="Up";
				if (_verticalMove<-_threshold)
					_movement="Down";
			}

			// if we haven't registered any input, we do nothing and exit
			if (_movement==""){return;}

			// if the path element we're on right now is automated, we do nothing and exit
			if (_currentPathElement.AutomaticMovement) { return; }

			if ( (_movement=="Up") && (_currentPathElement.CanGoUp()) )
			{
				_destination=_currentPathElement.Up; _shouldMove=true;
			}
			if ( (_movement=="Right") && (_currentPathElement.CanGoRight()) )
			{
				_destination=_currentPathElement.Right; _shouldMove=true;
			}
			if ( (_movement=="Down") && (_currentPathElement.CanGoDown()) )
			{
				_destination=_currentPathElement.Down; _shouldMove=true;
			}
			if ( (_movement=="Left") && (_currentPathElement.CanGoLeft()) )
			{
				_destination=_currentPathElement.Left; _shouldMove=true;
			}
		}

		/// <summary>
		/// Moves the character to the set destination
		/// </summary>
		public virtual void MoveCharacter()
		{
			if (!_shouldMove) { return; }
			transform.position = Vector3.MoveTowards(transform.position,_destination.transform.position-_offset,Time.deltaTime*CharacterSpeed);
		}

		/// <summary>
		/// Animates the character if it has an animator set
		/// Also flips the character if needed
		/// </summary>
		public virtual void AnimateCharacter()
		{
			if (_destination==null)
			{	return; }

			if ( _destination.transform.position.x -_offset.x <  transform.position.x)
			{
				if (CharacterIsFacingRight)
				{
					Flip();
				}
			}
			if ( _destination.transform.position.x -_offset.x >  transform.position.x)
			{
				if (!CharacterIsFacingRight)
				{
					Flip();
				}
			}

			// if we have an animator we'll want to let it know if our character is moving or not
			if (_animator!=null)
			{
				// if we've moved since last frame, we haven't reached our destination yet, so we set our current speed at 1
				if (_positionLastFrame!=transform.position)
				{
					_currentSpeed=1f;
				}
				else
				{
					// if we haven't moved last frame, we're static
					_currentSpeed=0f;
				}
				// we pass that parameter to our animator
				CorgiTools.UpdateAnimatorFloat(_animator,"Speed",_currentSpeed);
			}

			_positionLastFrame=transform.position;
		}

		/// <summary>
		/// Sets the current path element.
		/// </summary>
		/// <param name="pathElement">Path element.</param>
		public virtual void SetCurrentPathElement(LevelMapPathElement pathElement)
		{
			_currentPathElement=pathElement;
		}

		/// <summary>
		/// Sets the destination.
		/// </summary>
		/// <param name="newDestination">New destination.</param>
		public virtual void SetDestination(LevelMapPathElement newDestination)
		{
			// if we haven't reached our destination yet, we do nothing and exit
			if (transform.position != _destination.transform.position-_offset) 
			{ 
				return; 
			}
			// otherwise we set our new destination
			_destination=newDestination;
			_shouldMove=true;
		}

		protected virtual void Flip()
		{
			transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			CharacterIsFacingRight = transform.localScale.x > 0;
		}
	}
}