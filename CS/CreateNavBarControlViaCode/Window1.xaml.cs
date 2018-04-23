using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.NavBar;

namespace CreateNavBarControlViaCode
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(){
            InitializeComponent();
            CreateNavBarControl();
        }

        private void CreateNavBarControl() {
            NavBarControl navBar = new NavBarControl();
            //Add the NavBarControl to the control hierarchy
            dockPanel.Children.Add(navBar);
            DockPanel.SetDock(navBar, Dock.Left);

            CreateGroup1(navBar);
            CreateGroup2(navBar);

            navBar.View.ItemSelecting += new NavBarItemSelectingEventHandler(navBar_ItemSelecting);
        }

        #region #Group_Item_Images
        private void CreateGroup1(NavBarControl navBar){
            NavBarGroup group1 = new NavBarGroup();
            group1.Header = "Items";
            //Display an image within the group's header
            group1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/folder.png"));

            NavBarItem item1 = new NavBarItem();
            item1.Content = "Home";
            group1.Items.Add(item1);

            NavBarItem item2 = new NavBarItem();
            item2.Content = "Work";
            group1.Items.Add(item2);

            NavBarItem item3 = new NavBarItem();
            item3.Content = "Private";
            //Display an image within the item
            item3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/private.png"));
            //Change item image layout
            Style itemStyle = new Style();
            itemStyle.Setters.Add(new Setter(NavBarViewBase.LayoutSettingsProperty, new LayoutSettings() { ImageDocking = Dock.Right }));
            item3.VisualStyle = itemStyle;
            group1.Items.Add(item3);

            navBar.Groups.Add(group1);
        }
        #endregion #Group_Item_Images

        #region #Group_CustomContent
        private void CreateGroup2(NavBarControl navBar){
            NavBarGroup group2 = new NavBarGroup();
            group2.Header = "Custom Content";
            //Specify that the group's content should be defined via the Content property
            group2.DisplaySource = DisplaySource.Content;

            //Create a StackPanel and specify it as the group's content
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            group2.Content = stackPanel;
            
            //Define the panel's content by creating two text blocks
            stackPanel.Children.Add(new TextBlock() {Text = "Selected Item: "});

            TextBlock textBlock2 = new TextBlock();
            Binding textBlockTextBinding = new Binding("SelectedItem.Content");
            textBlockTextBinding.Source = navBar; //textBlockTextBinding.Source = navBar.Groups[0];
            //The second text block displays the text of the NavBarControl's selected item
            textBlock2.SetBinding(TextBlock.TextProperty, textBlockTextBinding);
            stackPanel.Children.Add(textBlock2);

            navBar.Groups.Add(group2);
        }
        #endregion #Group_CustomContent

        #region #ItemSelecting_Event
        private void navBar_ItemSelecting(object sender, NavBarItemSelectingEventArgs e){
            if (e.NewItem.Content.ToString() == "Private"){
                MessageBoxResult result = MessageBox.Show("Are you sure to select the 'Private' item?", "Confirm Dialog", MessageBoxButton.YesNo);
                e.Cancel = (result == MessageBoxResult.No);
            }
        }
        #endregion #ItemSelecting_Event
    }
}
