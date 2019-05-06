
namespace Rockyfi.Expr {
    using System.Collections.Generic;


    internal class SR {

        readonly static Dictionary<string, string> defaultDict = new Dictionary<string, string>()
        {
            "ErrorFormat" = "{0} at {1},code {2}",
            "InternalError" = "internal error",
            "NoParenBefore" = "not has {0} Before,code {1}",
            "NotHexChar" = "Not a hexadcimal Character",
            "NotSupportGlobalFunction" = "not support global function",
            "ParenNotMatch" = "{0} not Match,code{1}",
            "UnrecogniseChar" = "unrecognise Character",
            "VariableNotExist" = "Variable '{0}' not exist",
        };

        readonly static Dictionary<string, Dictionary<string, string>> localization = new Dictionary<string, Dictionary<string, string>>()
        {
            "zh" = new Dictionary<string, string> {

            }
        };

        static Dictionary<string, string> currentLocalization;

        SR() {
            currentLocalization = defaultDict;
        }

        public bool SwitchToLocalization(string key)
        {
            if (localization.TryGetValue(key, out Dictionary<string, string> dict))
            {
                currentLocalization = dict;
                return true;
            }

            return false;
        }

        public string GetString(string key)
        {
            if (currentLocalization != null && currentLocalization.TryGetValue(key, out string result))
                return result;

            if (defaultDict.TryGetValue(key, out result))
                return result;

            return null;
        }

        public

        internal static string ErrorFormat => GetString("ErrorFormat");

        internal static string InternalError => GetString("InternalError");

        internal static string NoParenBefore => GetString("NoParenBefore");

        internal static string NotHexChar => GetString("NotHexChar");

        internal static string NotSupportGlobalFunction => GetString("NotSupportGlobalFunction");

        internal static string ParenNotMatch => GetString("ParenNotMatch");

        internal static string UnrecogniseChar => GetString("UnrecogniseChar");

        internal static string VariableNotExist => GetString("VariableNotExist");
    }
}
