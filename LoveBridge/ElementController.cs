using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Love;
using Rockyfi;

namespace LoveBridge
{
    public class ElementController
    {
        public readonly Transform2D<ElementController> transform;
        public SizeF localSize { private set; get; } = new SizeF();
        public string Text { get; private set; } 

        public ElementController()
        {
            transform = new Transform2D<ElementController>(this);
        }

        /// <summary>
        /// 需要绘制区域
        /// </summary>
        public RectangleF Rect => new RectangleF(transform.AbsolutePosition, localSize);

        /// <summary>
        /// 本元素是否需要绘制
        /// </summary>
        public bool Visible = true;

        public virtual void OnSetParent(ElementController element)
        {
            transform.Parent = element.transform;
        }
        public virtual void OnRemoved()
        {
        }


        public void EmitEvent(string name, object data)
        {
            if (m_eventHandler != null)
            {
                m_eventHandler.Invoke(name, data);
            }
        }

        public delegate void EventHandler(string name, object data);
        public delegate void EventHandlerName(string name);
        private EventHandler m_eventHandler;

        public void BindEventHandler(object handler)
        {
            if (handler == null)
            {
                m_eventHandler = null;
                return;
            }

            if (handler is EventHandler)
            {
                m_eventHandler = (handler as EventHandler);
            }
            else if (handler is EventHandlerName)
            {
                m_eventHandler = ((name, data) =>
                {
                    (handler as EventHandlerName).Invoke(name);
                });
            }
            else if (handler is System.Action)
            {
                m_eventHandler = ((name, data) =>
                {
                    (handler as System.Action).Invoke();
                });
            }
            else if (handler is System.Action<object>)
            {
                m_eventHandler = ((name, data) =>
                {
                    (handler as System.Action<object>).Invoke(data);
                });
            }
        }

        public virtual void OnChangeAttributes(string key, object value)
        {
            if (key == "display")
            {
                Visible = "flex".Equals(value);
            }

            if (key == "eventHandler")
            {
                BindEventHandler(value);
            }
        }

        public virtual void OnUpdateLayout(float x, float y, float w, float h)
        {
            
            transform.UpdateAbsolute();
            transform.UpdateLocal();
            transform.Position = new Love.Vector2(x, y);

            localSize = new SizeF(w, h);
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
                    ChildScissor = false;
                    break;

                case Overflow.Hidden:
                    HasScrollOffset = false;
                    ChildScissor = true;
                    break;

                case Overflow.Scroll:
                    HasScrollOffset = true;
                    ChildScissor = true;
                    break;
            }

            // limit horizontal
            if (localSize.Width < childBound.Width)
            {
                m_scrollOffset.X = Mathf.Clamp(m_scrollOffset.X,  localSize.Width - childBound.Right, 0 );
            }
            else
            {
                m_scrollOffset.X = 0;
            }

            // limit vertical
            if (localSize.Height < childBound.Height)
            {
                m_scrollOffset.Y = Mathf.Clamp(m_scrollOffset.Y, localSize.Height - childBound.Bottom, 0);
            }
            else
            {
                m_scrollOffset.Y = 0;
            }

        }

        /// <summary>
        /// 指针在本元素滚动时调用
        /// </summary>
        /// <param name="deltaHorizontal">水平滚动</param>
        /// <param name="deltaVertical">垂直滚动</param>
        public void UpdateInputScroll(float deltaHorizontal, float deltaVertical)
        {
            m_scrollOffset -= new Vector2(deltaHorizontal, deltaVertical);
        }
        #endregion

        /// <summary>
        /// 指针在本元素按下时调用
        /// </summary>
        public void UpdateInputPressed()
        {
        }

        /// <summary>
        /// 指针在本元素释放时调用
        /// </summary>
        public void UpdateInputReleased()
        {
        }

        /// <summary>
        /// 指针在本元素时调用
        /// </summary>
        public void UpdateInputHoverVisible()
        {

        }

        /// <summary>
        /// 指针在本元素或其子元素时调用
        /// </summary>
        public void UpdateInputHover()
        {
        }

        /// <summary>
        /// 是否进行内容滚动
        /// </summary>
        public bool HasScrollOffset { get; protected set; } = false;

        /// <summary>
        /// 孩子节点是否遮罩剔除
        /// </summary>
        public bool ChildScissor { get; protected set; } = true;
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
