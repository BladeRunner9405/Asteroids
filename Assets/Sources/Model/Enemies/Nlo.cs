using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class Nlo : Enemy
    {
        private readonly float _speed;
        private Transformable _mainTarget;
        private readonly Transformable _sideTarget;

        private Transformable _target;

        public Nlo(Transformable mainTarget, Transformable sideTarget, Vector2 position, float speed) : base(position, 0)
        {
            _mainTarget = mainTarget;
            _sideTarget = sideTarget;
            _speed = speed;
            _target = _sideTarget;
        }

        public void SetMainTarget(Transformable target)
        {
            _mainTarget = target;
            _target = _mainTarget;
        }

        public override void Update(float deltaTime)
        {
            if (_target != null)
            {
                Vector2 nextPosition = Vector2.MoveTowards(Position, _target.Position, _speed * deltaTime);
                MoveTo(nextPosition);
                LookAt(_target.Position);
                if (!_target.isActive())
                {
                    ResetTarget();
                }
            }
            else
            {
                ResetTarget();
            }
        }

        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (Position - point)));
        }

        private void ResetTarget()
        {
            if (_mainTarget != null)
            {
                if (_mainTarget.isActive())
                {
                    _target = _mainTarget;
                }
            }
            if (!_target.isActive())
            {
                if (_sideTarget != null)
                {
                    if (_sideTarget.isActive())
                    {
                        _target = _sideTarget;
                    }
                }
            }
        }
    }
}
