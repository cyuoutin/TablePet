﻿using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;
using XamlAnimatedGif;

namespace TablePat_Win
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
        private NotificationManager notificationManager = new NotificationManager();
        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer timerMove = new DispatcherTimer();

        public Uri[] ResourceOnce = {
            new Uri("pack://application:,,,/Resources/headpicture.gif"),       // 0 headpicture
            new Uri("pack://application:,,,/Resources/profilepicture.gif"),    // 1 profilepicture
            new Uri("pack://application:,,,/Resources/start.gif"),             // 2 start
        };
        public Uri[] Resource = {
            new Uri("pack://application:,,,/Resources/movel.gif"),             // 0 movel
            new Uri("pack://application:,,,/Resources/mover.gif"),             // 1 mover
            new Uri("pack://application:,,,/Resources/relax.gif"),             // 2 relax
            new Uri("pack://application:,,,/Resources/relaxl.gif"),            // 3 relaxl
            new Uri("pack://application:,,,/Resources/interact.gif"),          // 4 interact
            new Uri("pack://application:,,,/Resources/interactl.gif"),         // 5 interactl
            new Uri("pack://application:,,,/Resources/sit.gif"),               // 6 sit
            new Uri("pack://application:,,,/Resources/sitl.gif"),              // 7 sitl
            new Uri("pack://application:,,,/Resources/sleep.gif"),             // 8 sleep
            new Uri("pack://application:,,,/Resources/sleepl.gif"),            // 9 sleepl
        };

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timerMove.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timerMove.Tick += timerMove_Tick;
            timer.Start();
            timerMove.Start();
        }

        private int threshold = 7;
        private void timer_Tick(object sender, EventArgs e)     // 1s时钟: 随机改变动画
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) return;
            if (Mouse.RightButton == MouseButtonState.Pressed) return;
            int ranTrigger = new Random().Next(0, 50);
            int ranType = new Random().Next(0, 44);
            int ranAdd = new Random().Next(0, 2);
            if (ranTrigger < threshold)
            {
                AnimationBehavior.SetSourceUri(pet, Resource[ranType % 10]);
                threshold = threshold > 0 ? threshold - 5 : 0;
                threshold = threshold < 8 ? threshold : 7;
            }
            else
            {
                threshold = threshold < 23 ? threshold + ranAdd : 23;
            }
        }

        private void timerMove_Tick(object sender,  EventArgs e)    // 1ms时钟: 移动窗口
        {
            Uri state = AnimationBehavior.GetSourceUri(pet);
            if (state == null) return;

            int width = screen.Bounds.Width;    // 获取屏幕的宽度     
            int height = screen.Bounds.Height;  // 获取屏幕的高度
            Point ptLeftUp = this.PointToScreen(new Point(0, 0));
            Point ptRightDown = this.PointToScreen(new Point(this.ActualWidth, this.ActualHeight));

            if (state == Resource[0])
            {
                if (ptLeftUp.X > -225)  this.Left -= 0.275;
            }
            if (state == Resource[1])
            {
                if (width - ptRightDown.X > -225) mainWin.Left += 0.275;
            }
        }

        private void mainWin_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)    // 鼠标拖动
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                AnimationBehavior.SetSourceUri(pet, Resource[7]);
                DragMove();
            }
        }

        private void mainWin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)     // 松开鼠标, 播放动画"start.gif"
        {
            AnimationBehavior.AddAnimationCompletedHandler(pet, animationCompleted);
            AnimationBehavior.SetRepeatBehavior(pet, new RepeatBehavior(1));
            AnimationBehavior.SetSourceUri(pet, ResourceOnce[2]);
        }

        public void animationCompleted(object sender, AnimationCompletedEventArgs e)    // 动画播放完毕时, 回到初始状态"relax.gif"
        {

            AnimationBehavior.SetSourceUri(pet, Resource[2]);
            AnimationBehavior.SetRepeatBehavior(pet, RepeatBehavior.Forever);
            AnimationBehavior.RemoveAnimationCompletedHandler(pet, animationCompleted);
        }

        private void pet_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)  // 单击触发随机对话, 通过通知显示, 后续需改进
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 7);

            string title = "TablePet";
            string message = "";

            switch (randomNumber)
            {
                case 1:
                    message = "在想我的事？";
                    break;
                case 2:
                    message = "别紧张，我只是来查看你的身体状况。";
                    break;
                case 3:
                    message = "你醒了吗，还是还在梦中？";
                    break;
                case 4:
                    message = "现在你只需要安心制定行动计划。";
                    break;
                case 5:
                    message = "Mon3tr，采集好我需要的组织样本。";
                    break;
                case 6:
                    message = "你似乎更加适应自己的工作和职责了，更像一个领导者了。";
                    break;
            }
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Information
            });
        }

    }
}