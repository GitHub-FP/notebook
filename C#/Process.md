### Process

作用：用于执行windows和linux的命令

```
// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Process process = new Process();//实例
process.StartInfo.CreateNoWindow = false;//设定不显示窗口
process.StartInfo.UseShellExecute = false;
process.StartInfo.FileName = "cmd.exe"; //设定程序名
process.StartInfo.RedirectStandardInput = true; //重定向标准输入
process.StartInfo.RedirectStandardOutput = true; //重定向标准输出
process.StartInfo.RedirectStandardError = true;//重定向错误输出
process.Start();
process.StandardInput.WriteLine("ipconfig");//执行的命令
process.StandardInput.WriteLine("exit");
process.WaitForExit();
var z = process.StandardOutput.ReadToEnd();
Console.WriteLine(z);
Console.ReadLine();
//process.Close();
```

