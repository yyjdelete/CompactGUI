﻿Imports Ookii.Dialogs.Wpf
Imports MethodTimer
Imports System.Windows.Media.Animation
Imports ModernWpf.Controls
Imports CompactGUI.Core

Class MainWindow

    Sub New()

        InitializeComponent()

        Me.DataContext = ViewModel

        ViewModel.State = "FreshLaunch"
    End Sub

    Public Property ViewModel As New MainViewModel

    Property activeFolder As ActiveFolder

    Private Sub SearchClicked(sender As Object, e As MouseButtonEventArgs)
        ViewModel.SelectFolder()
    End Sub

    Private Sub uiUpdateBanner_MouseUp(sender As Object, e As MouseButtonEventArgs)
        Process.Start(New ProcessStartInfo("https://github.com/IridiumIO/CompactGUI/releases/") With {.UseShellExecute = True})
    End Sub

    Private Sub uiBtnOptions_Click(sender As Object, e As RoutedEventArgs) Handles uiBtnOptions.Click

        Dim settingsDialog As New CustomContentDialog With {.Content = New SettingsControl}

        settingsDialog.PrimaryButtonText = "save and close"

        settingsDialog.ShowAsync()

    End Sub

    Private Sub Window_PreviewKeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.System Then e.Handled = True

    End Sub

    Private Sub Window_Drop(sender As Object, e As DragEventArgs)
        Dim xs As String() = e.Data.GetData(DataFormats.FileDrop, True)
        If xs.Length > 1 Then
            MessageBox.Show("You can only select one folder at a time")
            Return
        End If

        If IO.Directory.Exists(xs(0)) Then
            ViewModel.SelectFolder(xs(0))
        End If


    End Sub
End Class
