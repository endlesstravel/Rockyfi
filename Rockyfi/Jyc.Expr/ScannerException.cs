using System; 
using System.Text;

namespace Rockyfi.Expr
{
    public class ScannerException:Exception
    {
        public ScannerException(Error errorCode ) 
        {
        }

        public ScannerException(Error errorCode,int pos)
        {
        } 
    }
}
