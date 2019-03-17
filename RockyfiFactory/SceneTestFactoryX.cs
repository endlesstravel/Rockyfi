using System.Collections.Generic;
using Rockyfi;


namespace RockyfiFactory
{
    class SceneTestFactoryX
    {
        static void Mainx(string[] args)
        {
            {
                ShadowPlay<LoveBridge.Element> userInterface = new ShadowPlay<LoveBridge.Element>();
                LoveBridge bridge = new LoveBridge();
                string tmpXML = @"
<div flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" el-bind:id="" 'root-id' "" >
    <div el-for=""item in list"" el-bind:id=""item"" width=""100px"" height=""100px"">
        my id is {{item}}
        <div el-for=""item in list"" el-bind:id=""item"" width=""100px"" height=""100px"">
            my id is {{item}}
        </div>
    </div>
</div>
";
                userInterface.Build(tmpXML, "list");
                userInterface.SetBridge(bridge);
                userInterface.SetData("list", new List<string> { "a3" });
                userInterface.Update();
                userInterface.Update();
                System.Console.WriteLine("--------------------------------");
                System.Console.WriteLine(userInterface.ToString());

                userInterface.SetData("list", new List<string> { });
                userInterface.Update();
                System.Console.WriteLine("--------------------------------");
                System.Console.WriteLine(userInterface.ToString());

                userInterface.SetData("list", new List<string> { "a3" });
                userInterface.Update();
                System.Console.WriteLine("--------------------------------");
                System.Console.WriteLine(userInterface.ToString());


                System.Console.Read();
            }
        }
    }
}
