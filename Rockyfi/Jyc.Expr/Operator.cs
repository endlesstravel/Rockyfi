using System; 
using System.Text;

namespace Rockyfi.Expr
{  
    abstract class Operator : Expression
    { 
        public Operator( )
        { 
        }

        public abstract OperatorPriority Priority
        {
            get;
        }    
    }
}
