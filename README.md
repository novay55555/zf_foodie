# zf_foodie(咒法次货)
咒法学派蜜汁程序员出品, 源自咱们军师的点子. 通过这个app, 帮助选择困难症患者决定今天的午餐/晚餐的着落, 妈妈再也不用担心我次神马了!

## 技术栈
前端使用`Hbuilder`作为壳, 和喜闻乐见的`jQuery`库

后端使用`.NET`实现业务逻辑

## 需要的工具和环境
- Hbuidler 
- Visual Studio 2010+ 
- Mysql 5.6+

## 如何使用?
下文是在windows环境运行, linux环境还在研究ing...

1. 克隆仓库
> git clone git@github.com:novay55555/zf_foodie.git

2. 导入数据库, 数据库文件在`/front_back/zf_foodie.sql`

3. 增加`mysql`连接配置, 在`/front_back/WebUI/XmlConfig`下新建一个`Config.xml`文件, 写入如下代码, 注意修改`key`为`MySQl_Foodie`的`value`值, 根据自身开发的`mysql`环境填写**用户名**, **密码**, **数据库名**
  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <appSettings>
    <!-- ================== 1：数据库连接相关配置 ================== -->
    <!--数据库软件类型：SQLServer-->
    <add key="ComponentDbType" value="MySQl_Foodie" />
    <!-- 当前数据库名称-->
    <add key="DBName" value="ZF_Foodie" />
    <!-- SqlServer连接字符串:Server=服务器地址;Database=库名;Uid=用户;Pwd=密码-->
    <add key="SqlServer_Foodie" value="Server=.;Database=ZF_Foodie;Uid=sa;Pwd=123456" />
    <!-- MySQl连接字符串:Server=服务器地址;Database=库名;Uid=用户;Pwd=密码-->
    <add key="MySQl_Foodie" value="server=localhost;user id=root;password=;database=foodie" />
  </appSettings>
  ``` 

4. 配置**IIS**服务器, 并把`/front_back/WebUI/`作为站点发布到**IIS**上(这个过程很坑爹, 无法用言语表达), 如果你成功配置了, 在浏览器以`IP`+`端口`的形式访问出现**弹窗1**, `Congratulations!!!!!`

5. 以**管理员的身份**开启你的重量级选手**Visual Studio201x**, 打开`front_back`项目, 按`F5`启动项目

6. 修改`/front_end/js/main.js`下的`MyApp.ajax.url`的值, 修改为你自身配置的**IIS**服务器的`IP`和`端口`

7. 使用Hbuilder编辑器打开`front_end`项目(如有需要, 需要项目`右键`-->`转换成移动app`), USB连接手机后, 快捷键`ctrl` + `r`使用手机调试

8. JUST HF! :)

## 配置IIS遇到的二三坑
我的操作系统的win 7, 据有关专业的童鞋说, 不同的windows系统配置**IIS**遇到的坑不一样, 我先记录我自己遇到的坑
 >  提示无法执行ASP.NET

以管理员的身份运行CMD, 执行如下指令`%windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe -i`, 重新安装

![enter image description here](http://ww2.sinaimg.cn/large/6b7ebbf5gw1f8ru0687zzj20u103kab6.jpg) 

这类`Temporary ASP.NET files`的问题, 主要是因为windows内部的权限问题, 需要对`C:\Windows\temp`和`C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files`这两个目录增加`IIS_IUSRS`和`NETWORK SERVICE`这两个对象, 并且给这两个对象**全权控制**

参考文章: [猛戳我一下](http://www.tugberkugurlu.com/archive/local-iis-7-0-cs0016--could-not-write-to-output-file-microsoft-net-framework-v4-0-30319-temporary-asp-net-files)
