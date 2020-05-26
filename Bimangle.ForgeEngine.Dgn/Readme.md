
# BimAngle Engine DGN 快速开始

## 一. 环境准备
* 开发工具: Visual Studio 2017 (下文简称 VS2017)
* 软件平台: Bentley MicroStation CE (下文简称为 MSCE)


## 二. 下载编译

### 1. 下载源码
* 从 https://github.com/bimangle/forge-engine-samples 下载例子源码；
* 用 VS2017 打开解决方案 **ForgeEngine.Samples.Dgn.sln**；

### 2. 更新组件
* 打开“程序包管理器控制台”: 菜单->工具->NuGet 包管理器->程序包管理器控制台
* 将文件 NugetCommands.txt 中的脚本全部拷贝粘贴到 “程序包管理器控制台” 执行；

### 3. 编译源码
* 重新生成解决方案: 菜单->生成->重新生成解决方案;
* 记录解决方案的输出文件夹路径，例如：“*D:\Test\Bimangle.ForgeEngine.Samples\Bimangle.ForgeEngine.Dgn\bin\Debug*”
> 如果 MSCE 的安装文件夹不在 **C:\Program Files\Bentley\MicroStation CONNECT Edition\MicroStation** 下，则需要修改"引用"下的相关组件指向到正确路径，否则编译过程将会报错。

## 三. 部署运行
* 启动 MSCE, 菜单->文件->设置->配置（列表项）->配置变量（按钮)；
* 在弹出的对话框“**配置变量：用户 [Personal]**”中，搜索变量: **MS_ADDINPATH**, 点“编辑”按钮；
* 在弹出的对话框“**编辑环境变量**”的“新值”输入框中，追加之前记录的解决方案输出文件夹路径（分号分隔），点确定；
* 按 F9 打开“键入命令”对话框，输入命令 “**mdl load BimAngleEngineSamples;BimAngleEngineSamples ExportGltf**” 即可弹出导出对话框；

## 四. 调试测试
* 如果需要调试，可以设置断点后，在 VS2017 的菜单->附加到进程->选择附加到进程 microstation.exe；




