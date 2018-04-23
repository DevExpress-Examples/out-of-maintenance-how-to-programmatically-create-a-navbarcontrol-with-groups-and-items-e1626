Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Wpf.NavBar

Namespace CreateNavBarControlViaCode
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			CreateNavBarControl()
		End Sub

		Private Sub CreateNavBarControl()
			Dim navBar As New NavBarControl()
			'Add the NavBarControl to the control hierarchy
			dockPanel.Children.Add(navBar)
			DockPanel.SetDock(navBar, Dock.Left)

			CreateGroup1(navBar)
			CreateGroup2(navBar)

			AddHandler navBar.View.ItemSelecting, AddressOf navBar_ItemSelecting
		End Sub

		#Region "#Group_Item_Images"
		Private Sub CreateGroup1(ByVal navBar As NavBarControl)
			Dim group1 As New NavBarGroup()
			group1.Header = "Items"
			'Display an image within the group's header
			group1.ImageSource = New BitmapImage(New Uri("pack://application:,,,/Images/folder.png"))

			Dim item1 As New NavBarItem()
			item1.Content = "Home"
			group1.Items.Add(item1)

			Dim item2 As New NavBarItem()
			item2.Content = "Work"
			group1.Items.Add(item2)

			Dim item3 As New NavBarItem()
			item3.Content = "Private"
			'Display an image within the item
			item3.ImageSource = New BitmapImage(New Uri("pack://application:,,,/Images/private.png"))
			'Change item image layout
			Dim itemStyle As New Style()
			itemStyle.Setters.Add(New Setter(NavBarViewBase.LayoutSettingsProperty, New LayoutSettings() With {.ImageDocking = Dock.Right}))
			item3.VisualStyle = itemStyle
			group1.Items.Add(item3)

			navBar.Groups.Add(group1)
		End Sub
		#End Region ' #Group_Item_Images

		#Region "#Group_CustomContent"
		Private Sub CreateGroup2(ByVal navBar As NavBarControl)
			Dim group2 As New NavBarGroup()
			group2.Header = "Custom Content"
			'Specify that the group's content should be defined via the Content property
			group2.DisplaySource = DisplaySource.Content

			'Create a StackPanel and specify it as the group's content
			Dim stackPanel As New StackPanel() With {.Orientation = Orientation.Horizontal}
			group2.Content = stackPanel

			'Define the panel's content by creating two text blocks
			stackPanel.Children.Add(New TextBlock() With {.Text = "Selected Item: "})

			Dim textBlock2 As New TextBlock()
			Dim textBlockTextBinding As New Binding("SelectedItem.Content")
			textBlockTextBinding.Source = navBar 'textBlockTextBinding.Source = navBar.Groups[0];
			'The second text block displays the text of the NavBarControl's selected item
			textBlock2.SetBinding(TextBlock.TextProperty, textBlockTextBinding)
			stackPanel.Children.Add(textBlock2)

			navBar.Groups.Add(group2)
		End Sub
		#End Region ' #Group_CustomContent

		#Region "#ItemSelecting_Event"
		Private Sub navBar_ItemSelecting(ByVal sender As Object, ByVal e As NavBarItemSelectingEventArgs)
			If e.NewItem.Content.ToString() = "Private" Then
				Dim result As MessageBoxResult = MessageBox.Show("Are you sure to select the 'Private' item?", "Confirm Dialog", MessageBoxButton.YesNo)
				e.Cancel = (result = MessageBoxResult.No)
			End If
		End Sub
		#End Region ' #ItemSelecting_Event
	End Class
End Namespace
