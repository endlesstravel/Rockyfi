using Love;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveBridge
{
    public class Transform2D<T> where T : class
    {
        #region Constructors

        readonly public T Master;

        public Transform2D(T master)
        {
            Master = master;
            this.Position = Vector2.Zero;
            this.Rotation = 0;
            this.Scale = Vector2.One;
        }

        #endregion

        #region Fields

        private Transform2D<T> parent;

        private List<Transform2D<T>> children = new List<Transform2D<T>>();

        private Matrix44 absolute, invertAbsolute, local;

        private float localRotation, absoluteRotation;

        private Vector2 localScale, absoluteScale, localPosition, absolutePosition;

        private bool needsAbsoluteUpdate = true, needsLocalUpdate = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parent transform.
        /// </summary>
        /// <value>The parent.</value>
        public Transform2D<T> Parent
        {
            get => this.parent;
            set
            {
                if (this.parent != value)
                {
                    if (this.parent != null)
                        this.parent.children.Remove(this);

                    this.parent = value;

                    if (this.parent != null)
                        this.parent.children.Add(this);

                    this.SetNeedsAbsoluteUpdate();
                }
            }
        }

        /// <summary>
        /// Gets all the children transform.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<Transform2D<T>> Children => this.children.ToList();

        /// <summary>
        /// Gets the absolute rotation.
        /// </summary>
        /// <value>The absolute rotation.</value>
        public float AbsoluteRotation => this.UpdateAbsoluteAndGet(ref this.absoluteRotation);

        /// <summary>
        /// Gets the absolute scale.
        /// </summary>
        /// <value>The absolute scale.</value>
        public Vector2 AbsoluteScale => this.UpdateAbsoluteAndGet(ref this.absoluteScale);

        /// <summary>
        /// Gets the absolute position.
        /// </summary>
        /// <value>The absolute position.</value>
        public Vector2 AbsolutePosition => this.UpdateAbsoluteAndGet(ref this.absolutePosition);

        /// <summary>
        /// Gets or sets the rotation (relative to the parent, absolute if no parent).
        /// </summary>
        /// <value>The rotation.</value>
        public float Rotation
        {
            get => this.localRotation;
            set
            {
                if (this.localRotation != value)
                {
                    this.localRotation = value;
                    this.SetNeedsLocalUpdate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the position (relative to the parent, absolute if no parent).
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position
        {
            get => this.localPosition;
            set
            {
                if (this.localPosition != value)
                {
                    this.localPosition = value;
                    this.SetNeedsLocalUpdate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the scale (relative to the parent, absolute if no parent).
        /// </summary>
        /// <value>The scale.</value>
        public Vector2 Scale
        {
            get => this.localScale;
            set
            {
                if (this.localScale != value)
                {
                    this.localScale = value;
                    this.SetNeedsLocalUpdate();
                }
            }
        }

        /// <summary>
        /// Gets the matrix representing the local transform.
        /// </summary>
        /// <value>The relative matrix.</value>
        public Matrix44 Local => this.UpdateLocalAndGet(ref this.absolute);

        /// <summary>
        /// Gets the matrix representing the absolute transform.
        /// </summary>
        /// <value>The absolute matrix.</value>
        public Matrix44 Absolute => this.UpdateAbsoluteAndGet(ref this.absolute);

        /// <summary>
        /// Gets the matrix representing the invert of the absolute transform.
        /// </summary>
        /// <value>The absolute matrix.</value>
        public Matrix44 InvertAbsolute => this.UpdateAbsoluteAndGet(ref this.invertAbsolute);

        #endregion

        #region Methods

        public void ToLocalPosition(ref Vector2 absolute, out Vector2 local)
        {
            Vector2Transform(ref absolute, ref this.invertAbsolute, out local);
        }

        public void ToAbsolutePosition(ref Vector2 local, out Vector2 absolute)
        {
            Vector2Transform(ref local, ref this.absolute, out absolute);
        }


        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        public static void Vector2Transform(ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
        {
            var x = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
            var y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
            result.X = x;
            result.Y = y;
        }

        public Vector2 ToLocalPosition(Vector2 absolute)
        {
            Vector2 result;
            ToLocalPosition(ref absolute, out result);
            return result;
        }

        public Vector2 ToAbsolutePosition(Vector2 local)
        {
            Vector2 result;
            ToAbsolutePosition(ref local, out result);
            return result;
        }

        private void SetNeedsLocalUpdate()
        {
            this.needsLocalUpdate = true;
            this.SetNeedsAbsoluteUpdate();
        }

        private void SetNeedsAbsoluteUpdate()
        {
            this.needsAbsoluteUpdate = true;

            foreach (var child in this.children)
            {
                child.SetNeedsAbsoluteUpdate();
            }
        }

        public void UpdateLocal()
        {
            var result = Matrix44.CreateScale(this.Scale.X, this.Scale.Y, 1);
            result *= Matrix44.CreateRotationZ(this.Rotation);
            result *= Matrix44.CreateTranslation(this.Position.X, this.Position.Y, 0);
            this.local = result;

            this.needsLocalUpdate = false;
        }

        public void UpdateAbsolute()
        {
            if (this.Parent == null)
            {
                this.absolute = this.local;
                this.absoluteScale = this.localScale;
                this.absoluteRotation = this.localRotation;
                this.absolutePosition = this.localPosition;
            }
            else
            {
                var parentAbsolute = this.Parent.Absolute;
                Matrix44.Multiply(ref this.local, ref parentAbsolute, out this.absolute);
                this.absoluteScale = this.Parent.AbsoluteScale * this.Scale;
                this.absoluteRotation = this.Parent.AbsoluteRotation + this.Rotation;
                this.absolutePosition = Vector2.Zero;
                this.ToAbsolutePosition(ref this.absolutePosition, out this.absolutePosition);
            }

            Matrix44.Invert(ref this.absolute, out this.invertAbsolute);

            this.needsAbsoluteUpdate = false;
        }

        private T UpdateLocalAndGet<T>(ref T field)
        {
            if (this.needsLocalUpdate)
            {
                this.UpdateLocal();
            }

            return field;
        }

        private T UpdateAbsoluteAndGet<T>(ref T field)
        {
            if (this.needsLocalUpdate)
            {
                this.UpdateLocal();
            }

            if (this.needsAbsoluteUpdate)
            {
                this.UpdateAbsolute();
            }

            return field;
        }

        #endregion

    }

}
