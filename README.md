# <center>CandyCrush开发文档</center> #
### <p align="right">WellPan 2016/2/10</p> ###



## 简介 ##

### 开发环境 ： ###
　　Win 7 + VS2013 + Unity 4.6.3

###游戏简介： ###
　　消除类游戏，游戏玩法参考CandyCrush，当横排或者竖排出现相同糖果的时候会被消除，得分。

## 结构 ##

### 界面结构： ###
* 游戏开始界面
* 游戏界面
* 游戏配置界面（后期制作配套）

### 功能逻辑 ###
1. 生成新的Candy，并创建Candy类；
2. 按行列随机生成多个Candy；
3. ** Candy间的互换 ** :exchange(Candy c1,Candy c2)，；
4. ** Candy消除算法 **:在Candy中建立Dispose(),注意消除与GameController之间的联系；
5. ** Candy补充算法 **:需要建立逻辑二维数组,数组的增减与实际的销毁相对应;
6. Candy缓动功能：
5. ...（后期继续补充）

### 代码结构： ###
<table border="1">
	<tr>
		<td>类名</td>
		<td>功能</td>
		<td>备注</td>
	</tr>
	<tr>
		<td>Candy</td>
		<td>配置Candy的属性，包括行列索引，Candy的类型;功能：更新Candy坐标</td>
		<td></td>
	</tr>
	<tr>
		<td>GameController</td>
		<td>
			游戏流程和功能控制，主要包括：确定行列数、生成Candy、...（后期补充）
		</td>
		<td></td>
	</tr>
</table>