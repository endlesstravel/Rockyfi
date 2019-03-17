//using System;
//using System.Collections.Generic;
//using System.Xml;
//using System.Text.RegularExpressions;
//using System.IO;

//namespace Rockyfi
//{
//    /// <summary>
//    /// Provide a convenient way to render tree
//    /// </summary>
//    public partial class ShadowPlay<T> where T: BridgeElement<T><T> where T: Bridge<BridgeElement<T>>
//    {


//        #region re-render
//        Node ReRenderTemplateTreeSingle(TemplateNode tnode, Node original, Node newborn, ContextStack contextStack, object forContext)
//        {
//            if (original == newborn)
//            {
//                return original;
//            }

//            if (newborn == null)
//            {
//                // remove original

//            }

//            var ra = GetNodeRuntimeAttribute(original);

//            // set el-for value
//            ra.forExpressItemCurrentValue = forContext;

//            // process node el-bind
//            var originalAttrs = ra.attributes;
//            foreach (var attr in tnode.attributeDataBindExpressList)
//            {
//                if (attr.TryEvaluate(contextStack, out var attrValue))
//                {
//                    if (originalAttrs.TryGetValue(attr, out var oldValue) && oldValue == attrValue)
//                    {
//                        // equal and not
//                        // Console.WriteLine("no need to process style");
//                    }
//                    else
//                    {
//                        ra.attributes[attr] = attrValue;
//                        ProcessNodeStyle(original, attr.TargetName, attrValue != null ? attrValue.ToString() : "");
//                    }
//                }
//                else
//                {
//                    ra.attributes.Remove(attr);
//                }
//            }

//            // process innerText
//            if (tnode.textDataBindExpress != null)
//            {
//                ra.textDataBindExpressCurrentValue = tnode.textDataBindExpress.Evaluate(contextStack);
//            }

//            // render children
//            foreach (var vchild in tnode.Children)
//            {
//                foreach (var child in ReRenderTemplateTree(vchild, contextStack))
//                {
//                    original.AddChild(child);
//                }
//            }
//            return original;
//        }
//        LinkedList<Node> ReRenderTemplateTree(TemplateNode tnode, Node original, Node newborn, ContextStack contextStack)
//        {
//            LinkedList<Node> nodeList = new LinkedList<Node>();
//            bool useFor = tnode.forExpress != null;
//            IEnumerable<object> forList = useFor ? tnode.forExpress.Evaluate(contextStack) : new List<object> { 0 };
//            if (forList != null)
//            {
//                foreach (object forContext in forList)
//                {
//                    if (useFor)
//                    {
//                        contextStack.EnterScope();
//                        contextStack.Set(tnode.forExpress.IteratorName, forContext);
//                    }
//                    bool skipElement = (tnode.ifExpress != null
//                        && tnode.ifExpress.TryEvaluate(contextStack, out var result)
//                        && result == false);
//                    if (!skipElement)
//                    {
//                        var fc = useFor ? forContext : null;
//                        var node = RenderTemplateTree(tnode, contextStack, fc);
//                        nodeList.AddLast(node);
//                    }
//                    if (useFor)
//                    {
//                        contextStack.LeaveScope();
//                    }
//                }
//            }
//            return nodeList;
//        }

//        void ReRenderTemplateTreeRoot(TemplateNode tnode)
//        {
//            var newbornRoot = RenderTemplateTreeRoot(templateRoot);
//            ReRenderTemplateTreeSingle(templateRoot, root, newbornRoot, GenerateContextStack(), null);
//        }
//        #endregion
//    }
//}
