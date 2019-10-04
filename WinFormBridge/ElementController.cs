using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using Rockyfi;

namespace WinFormBridge
{
    public class ElementController : System.Windows.Forms.Control
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            if (g != null)
            {
                g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), Floor(Rect));
            }
        }

        static Rectangle Floor(RectangleF r)
        {
            return new Rectangle(
                (int)Math.Floor(r.X),
                (int)Math.Floor(r.Y),
                (int)Math.Floor(r.Width),
                (int)Math.Floor(r.Height)
                );
        }


        public override string ToString()
        {
            return base.ToString() + " <" + TagName + ">" + Text;
        }


        public readonly string TagName;
        public readonly Dictionary<string, object> Attr = new Dictionary<string, object>();

        public readonly Transform2D<ElementController> transform;
        public SizeF localSize { private set; get; } = new SizeF();

        public ElementController(string tagName)
        {
            TagName = tagName;
            transform = new Transform2D<ElementController>(this);
        }

        public List<ElementController> Children => transform.Children.Select(item => item.Master).ToList();
        public ElementController ElementParent => transform.Parent != null ? transform.Parent.Master : null;

        /// <summary>
        /// 需要绘制区域
        /// </summary>
        public RectangleF Rect => new RectangleF(new PointF(transform.AbsolutePosition.X, transform.AbsolutePosition.Y), localSize);

        ///// <summary>
        ///// 本元素是否需要绘制
        ///// </summary>
        //public bool Visible = true;

        /// <summary>
        /// 是否需要自动导航
        /// </summary>
        public bool AutoNavigation = false;

        /// <summary>
        /// 是否可以交互
        /// </summary>
        public bool Hoverable = true;

        #region key to add or remove
        public virtual void OnSetParent(ElementController element)
        {
            transform.Parent = element.transform;
        }
        public virtual void OnRemoved()
        {
            transform.Parent = null;
        }
        #endregion

        /// <summary>
        /// return true if get an not null object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="tobj"></param>
        /// <returns></returns>
        public bool TryGetAttribute<T>(string name, out T tobj) where T : class
        {
            if (Attr.TryGetValue(name, out var obj))
            {
                tobj = obj as T;
            }
            else
            {
                tobj = null;
            }

            return tobj != null;
        }

        public bool TryGetBoolean(string name, out bool result)
        {
            result = false; 
            if (Attr.TryGetValue(name, out var obj) && obj is bool)
            {
                result = (bool)obj;
                return true;
            }

            return false;   
        }

        public virtual void OnChangeAttributes(string key, object value)
        {
            Attr[key] = value;  

            if (key == "display")
            {
                Visible = "flex".Equals(value);
            }

            if (key == "autoNavigation" && value is bool)
            {
                AutoNavigation = (bool)value;
            }
            if (key == "autoNavigation" && value is string)
            {
                AutoNavigation = (((string)value).ToLower() == "true");
            }
        }

        public virtual void OnUpdateLayout(float x, float y, float w, float h)
        {
            
            transform.UpdateAbsolute();
            transform.UpdateLocal();
            transform.Position = new Vector2(x, y);

            localSize = new SizeF(w, h);
        }

        float Math_Clamp(float v, float min, float max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

        public virtual void OnChangeText(string text)
        {
            this.Text = text;
        }

        #region 滚动判定区域
        public virtual void OnUpdateOverflow(Overflow overflow, RectangleF childBound)
        {
            switch(overflow)
            {
                default:
                case Overflow.Visible:
                    HasScrollOffset = false;
                    MaskScissor = false;
                    break;

                case Overflow.Hidden:
                    HasScrollOffset = false;
                    MaskScissor = true;
                    break;

                case Overflow.Scroll:
                    HasScrollOffset = true;
                    MaskScissor = true;
                    break;
            }

            // limit horizontal
            if (localSize.Width < childBound.Width)
            {
                m_scrollOffset.X = Math_Clamp(m_scrollOffset.X,  localSize.Width - childBound.Right, 0 );
                IsHorizontalOverflow = true;
            }
            else
            {
                m_scrollOffset.X = 0;
                IsHorizontalOverflow = false;   
            }

            // limit vertical
            if (localSize.Height < childBound.Height)
            {
                m_scrollOffset.Y = Math_Clamp(m_scrollOffset.Y, localSize.Height - childBound.Bottom, 0);
                IsVerticalOverflow = true;
            }
            else
            {
                m_scrollOffset.Y = 0;
                IsVerticalOverflow = false;
            }

        }

        /// <summary>
        /// 指针在本元素滚动时调用
        /// </summary>
        /// <param name="deltaHorizontal">水平滚动</param>
        /// <param name="deltaVertical">垂直滚动</param>
        public void UpdateInputScroll(float deltaHorizontal, float deltaVertical)
        {
            m_scrollOffset += new Vector2(deltaHorizontal, deltaVertical) * 20;
        }

        /// <summary>
        /// 手动设置滚动偏移
        /// </summary>
        /// <param name="scrollOffset"></param>
        public void SetScrollOffset(Vector2 scrollOffset)
        {
            m_scrollOffset = scrollOffset;
        }

        #endregion

        #region 给予调用可复写的方法

        /// <summary>
        /// 指针在本元素时调用（不包括在子节点的情况）
        /// </summary>
        public virtual void UpdateInputHoverVisible()
        {
        }

        /// <summary>
        /// 指针在本元素或其子元素时调用
        /// </summary>
        public virtual void UpdateInputHover()
        {
        }

        /// <summary>
        /// 指针没在本元素或其子元素时调用
        /// </summary>
        public virtual void UpdateInputNotHover()
        {
        }

        /// <summary>
        /// 指针进入本元素时（包括子元素效应）
        /// </summary>
        public virtual void UpdateInputHoverEnter()
        {
        }

        /// <summary>
        /// 指针退出本元素时（包括子元素效应）
        /// </summary>
        public virtual void UpdateInputHoverLeave()
        {
        }

        /// <summary>
        /// 指针自动导航时调用-开始的一次
        /// </summary>
        public virtual void UpdateInputAutoNavigationBegin()
        {
        }

        /// <summary>
        /// 指针自动导航时调用
        /// </summary>
        public virtual void UpdateInputAutoNavigation()
        {
        }
        /// <summary>
        /// 指针自动导航时调用-结束的一次
        /// </summary>
        public virtual void UpdateInputAutoNavigationEnd()
        {
        }


        #endregion


        /// <summary>
        /// 内部的配合 LayoutController 的 InternalUpdateInput 方法，每个元素每一帧都调用一次
        /// </summary>
        public virtual void InternalUpdateInput()
        {
            if (currentFrameIsHover == true)
            {
                UpdateInputHover();
            }

            if (lastFrameIsHover == true && currentFrameIsHover == true)
            {
            }
            else if (lastFrameIsHover == true && currentFrameIsHover == false)
            {
                UpdateInputHoverLeave();
            }
            else if (lastFrameIsHover == false && currentFrameIsHover == true)
            {
                UpdateInputHoverEnter();
            }

            if (currentFrameIsHover == false)
            {
                UpdateInputNotHover();
            }

            lastFrameIsHover = currentFrameIsHover;
            currentFrameIsHover = false;
        }

        bool lastFrameIsHover = false;
        bool currentFrameIsHover = false;

        /// <summary>
        /// 内部的配合 LayoutController 的 InternalUpdateInput 方法，指针在本元素或其子元素时调用
        /// </summary>
        public virtual void InternalUpdateInputHover()
        {
            currentFrameIsHover = true;
        }

        /// <summary>
        /// 内部的配合 LayoutController 的 InternalUpdateInput 方法，更新滚轮输入操作
        /// </summary>
        /// <param name="scrollUsed">代表此事件是否已经被响应</param>
        /// <returns></returns>
        public virtual void InternalUpdateInputScroll(ref bool scrollUsed, float scrollX, float scrollY)
        {
            if (scrollUsed == false &&
                ((IsHorizontalOverflow && Math.Abs(scrollX) > 0)
                || (IsVerticalOverflow && Math.Abs(scrollY) > 0)
                ))
            {
                UpdateInputScroll(scrollX, scrollY);
            }

        }


        /// <summary>
        /// 是否进行内容滚动
        /// </summary>
        public bool HasScrollOffset { get; protected set; } = false;

        /// <summary>
        /// 水平内容是否溢出？
        /// </summary>
        public bool IsHorizontalOverflow { get; private set; } = false;

        /// <summary>
        /// 垂直内容是否溢出？
        /// </summary>
        public bool IsVerticalOverflow { get; private set; } = false;

        /// <summary>
        /// 孩子节点是否遮罩剔除
        /// </summary>
        public bool MaskScissor { get; protected set; } = false;
        public Vector2 m_scrollOffset;
        public Vector2 ScrollOffset => HasScrollOffset ? m_scrollOffset : Vector2.Zero;

        public Element Element { get; internal set; }

        /// <summary>
        /// 绘制本元素
        /// </summary>
        public virtual void Draw()
        {

        }
    }

}
