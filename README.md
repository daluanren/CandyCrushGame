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
6. Candy缓动功能：使用itween.Moveto实现；
7. 消除Candy：Candy的消除条件检测、Candy的消除；
8. ** Candy相邻互换 **:特点条件：同行，列索引差的绝对值为1；同列，行索引差的绝对值为1；(Mathf.Abs(c1.rowIndex - c2.rowIndex) + Mathf.Abs(c1.rowIndex - c2.rowIndex) == 1。
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

### 问题记录： ###




- 使用插件itween，使Cany的移动能够有缓冲。

		itween.Moveto(thisCandy,itween.Hash("x",columnIndex,"y",rowIndxe,"time",0.5f));		

使用该方法更新Candy位置后，发现Candy偏移到屏幕外。解决方案：应该针对Candy的LocalPosition进行位移：

	itween.Moveto(thisCandy,itween.Hash("x",columnIndex,"y",rowIndxe,"time",0.5f,**"islocal",true**));


 

- 检测Candy有无消除的时候，横向和纵向分开检测：

		for (int rowIndex = rowNum - 1; rowIndex >= 0; rowIndex--)
		{
			for (int columnIndex = 0; columnIndex < columnNum ; columnIndex++)
			{
				if (GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 1).typeName &&GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 2).typeName)
				{
					Debug.Log(rowIndex + " " + columnIndex + " " + columnIndex + 1 + " " + columnIndex + 2);
					AddMatches(GetCandy(rowIndex, columnIndex));
					AddMatches(GetCandy(rowIndex, columnIndex + 1));
					AddMatches(GetCandy(rowIndex, columnIndex + 2));
				}
			}
		}
报错：数组超出范围，解决方法 columnIndex < columnNum ** -2 **：

		for (int rowIndex = rowNum - 1; rowIndex >= 0; rowIndex--)
		{
			for (int columnIndex = 0; columnIndex < columnNum ** -2 **; columnIndex++)
			{
				if (GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 1).typeName &&GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 2).typeName)
				{
					Debug.Log(rowIndex + " " + columnIndex + " " + columnIndex + 1 + " " + columnIndex + 2);
					AddMatches(GetCandy(rowIndex, columnIndex));
					AddMatches(GetCandy(rowIndex, columnIndex + 1));
					AddMatches(GetCandy(rowIndex, columnIndex + 2));
				}
			}
		}


- 4个相同的Candy，并排没有直接消除，要再操作一次任意交换，才会检测消除。原因：CheckMatches()函数是在Exchange()函数后，所以没有选择，游戏初始化的时候就没有检测消除。

- 与Candy相邻的Candy才能够进行互换，使用判断

		if((candy1.rowIndex == candy2.rowIndex && Abs(candy1.columnIndex candy2.columnIndex) == 1) || 
		(candy1.columnIndex == candy2.columnIndex && Abs(candy1.rowIndex - candy2.rowIndex))
上面这种逻辑判断，使得代码太过于复杂，通过网上学习的，数学归纳法：

		if(Mathf.Abs(c1.rowIndex - c2.rowIndex) + Mathf.Abs(c1.rowIndex - c2.rowIndex) == 1) 
这种方法也能确定相邻，行列差的绝对值相加为1去判断。