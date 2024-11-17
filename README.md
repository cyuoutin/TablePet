# <center>TablePet</center>

### LOG
---
##### 功能：

- [x] 透明窗口
- [x] 鼠标任意拖拽：拖拽时显示sitl，松开鼠标播放一遍start，后默认回到relax
- [x] 自动切换动画：10种随机类型，1s时钟触发，使用自适应阈值调节切换频率
- [x] 窗口移动：动画为movel和mover时，1ms时钟改变窗口x坐标，到达左/右屏幕边缘时停止
- [x] 固定对白：单击时发送系统通知，内容为6种对白随机，通知用NuGget的包实现
- [x] 改进大小：适配不同屏幕尺寸
- [x] 右键菜单：
    - 置于顶层 (默认选中)
    - 调整大小 (小/中/大)
    - 禁止走动
    - 坐下 & 睡觉 (固定动作) 睡觉时无法坐下
    - 退出
- [x] 读取系统异常信息 (内存占用、CPU) 并提示 （为方便展示效果改成定时通报）
- [x] 改进拖拽：左右方向不同
- [x] 系统托盘图标 完成图标，后续考虑是否加入右键菜单
- [x] 改进固定对白：人物头上气泡
- [x] 便签/备忘录
    - 单个新建(便签外观), Listbox绑定数据, ObservableCollection<\T>实现实时刷新, Icon按钮, Item双击打开
- [x] GPT对话
    - 输入输出框，可选气泡模式
    - 上下文连续
    - 角色扮演
    - 使用pythonnet连接Python和.NET, 用Python实现OpenAI API的调用
    - 像Siri一样, 通过意图提取，用对话调用功能
- [x] rss订阅 & 推送
    - 用包获取Feed & 读取Feed & 解析Feed
    - Feed的Properties：新建和修改设置
    - Feed列表和Entry列表, 双层联动
    - 美观的Entry列表, 包含标题(链接到原网页)、Feed的名称和作者、最后更新时间、序号、从HTML解析到xaml格式的正文、显示图片、Icon功能按钮, richtextbox宽度自适应
    - Listbox放于ScrollViewer内, 并重写滚动处理方法, bubble到外层, 避免被Listbox截断。效果是可以随意滚动, 而不是只能滚动到下一个Item在最顶端的显示。
    - Feed的文件夹
    - 收藏
- [x] 闹钟
- [x] 日历

---

##### 计划：

- [ ] 右键菜单：
    - [ ] 双屏的适配
    - [ ] 语音与动作or固定对白对应，或AI语音
- [ ] 扩展功能：
    - [ ] <font color=red>便签/备忘录</font>
        - [ ] 设置选项：提供几个背景颜色方案，字体和字号，透明度
        - [ ] 连接数据库: 因为本地数据库连接冲突所以不做了
    - [ ] GPT对话
        - [ ] 保留输入文本
        - [ ] 设计并利用实体识别
    - [ ] 交互培养、心情值、投喂互动 (需要更多动画)
        - [x] 心情条
    - [ ] 置顶的图片查看器
    - [x] 快捷启动
    - [ ] rss订阅 & 推送
        - [ ] 转发
        - [ ] Tag
    - [ ] 和桌面图标互动
- [ ] 数据模型和数据库
- [ ] 改进透明窗口：提升显存效率
- [ ] 改进行走：上下 & 斜方向 (需要更多动画)
- [ ] 改进动画切换：不是随机，而是有一定逻辑顺序的，或者更好的概率分布
- [ ] 使用爬虫，推送微博热搜，随机推荐电影或音乐
