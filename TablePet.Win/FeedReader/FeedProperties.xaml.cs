﻿using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TablePet.Services.Controllers;
using TablePet.Services.Models;

namespace TablePet.Win.FeedReader
{
    /// <summary>
    /// FeedProperties.xaml 的交互逻辑
    /// </summary>
    public partial class FeedProperties : Window
    {
        private FeedReaderService feedReaderService;
        private Feed feed;
        private FeedExt feedOriginal;
        private bool update = false;

        public FeedProperties()
        {
            InitializeComponent();
        }


        public FeedProperties(FeedReaderService service)
        {
            InitializeComponent();
            this.feedReaderService = service;
            update = false;
            LoadFolders(feedReaderService.Folders);
        }


        public FeedProperties(FeedReaderService service, FeedExt f)
        {
            InitializeComponent();
            this.feedReaderService = service;
            this.feedOriginal = f;
            this.feed = f.Feed;
            update = true;
            UpdateFeedText(f.Feed, f.Url, f.FolderID);
            LoadFolders(feedReaderService.Folders);
        }


        private void UpdateUrl(List<string> urls)
        {
            cb_url.Dispatcher.Invoke(new Action(() =>
            {
                if (urls == null || urls.Count <= 0)
                {
                    cb_url.Text = string.Empty;
                }
                else if (urls.Count == 1)
                {
                    cb_url.Text = urls[0];
                }
                else
                {
                    foreach (string url in urls)
                    {
                        cb_url.Items.Add(url);
                        cb_url.IsDropDownOpen = true;
                    }
                }
            }));
        }


        private void UpdateFeedText(Feed f, string url, int folderID=0)
        {
            if (f == null) return;
            Dispatcher.Invoke(new Action(() =>
            {
                if (update) cb_url.Text = url;
                feed = f;
                tb_feedTitle.Text = feed.Title;
                if (feed.LastUpdatedDate != null)
                {
                    lb_lastDate.Content = feedReaderService.GetTimeSpanTilNow((DateTime)feed.LastUpdatedDate);
                }
                lb_state.Content = "OK";
                cb_folders.SelectedIndex = folderID;
            }));
        }


        private void bt_loadFeed_Click(object sender, RoutedEventArgs e)
        {
            string url = cb_url.Text;
            Task findTask = Task.Run(() =>
            {
                List<string> urls = feedReaderService.FindFeed(url);
                UpdateUrl(urls);
                if (urls.Count == 1)
                {
                    Feed f = feedReaderService.ReadFeed(urls[0]);
                    UpdateFeedText(f, url);
                }
            });
        }


        private void cb_url_DropDownClosed(object sender, EventArgs e)
        {
            string url = cb_url.Text;
            Task readTask = Task.Run(() =>
            {
                Feed f = feedReaderService.ReadFeed(url);
                UpdateFeedText(f, url);
            });
        }


        public void LoadFolders(List<string> folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                cb_folders.Items.Add(folders[i]);
            }
        }


        private void bt_feedSave_Click(object sender, RoutedEventArgs e)
        {
            if (lb_state.Content.ToString() != "OK") return;
            int idx = cb_folders.SelectedIndex;
            if (idx == -1) return;
            FeedExt node = new FeedExt(Feed: feed, Title: tb_feedTitle.Text, Url: cb_url.Text, FolderID: idx, service:feedReaderService);
            
            if (update)
            {
                feedReaderService.UpdateFeed(node, feedOriginal);
            }
            else
            {
                feedReaderService.AddFeed(node);
            }

            this.Close();
        }


        private void bt_feedPreview_Click(object sender, RoutedEventArgs e)
        {

        }


        private void bt_feedCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
