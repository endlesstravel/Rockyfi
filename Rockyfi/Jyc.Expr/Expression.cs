using System; 
using System.Text;

namespace Rockyfi.Expr
{
    abstract class Expression
    {
        internal abstract Result Eval(Evaluator evaluater, Result[] argArray);
    }
}
