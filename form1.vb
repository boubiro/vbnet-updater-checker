Imports System.Net
Imports System.IO
Imports System.Threading.Tasks

Public Class Form1
    Dim WithEvents WC As New WebClient
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim address As String = "http://www.sobhansoft.com/products/atafs/latestVersion.txt"
        Label1.Text = "Checking for Update..."
        Try
            Using client = New WebClient()
                Using reader As StreamReader = New StreamReader(client.OpenRead(address))
                    TextBox1.Text = reader.ReadToEnd

                    Dim FILE_NAME As String = Application.StartupPath & "\softwareversion.ini"
                    If System.IO.File.Exists(FILE_NAME) = True Then
                        Dim value As String = File.ReadAllText(FILE_NAME)
                        If (value <> reader.ReadToEnd) Then
                            For Each p As Process In Process.GetProcesses
                                Label1.Text = "Closing Application..."
                                If p.ProcessName = "app.exe" Then 'If you don't know what your program's process name is, simply run your program, run windows task manager, select 'processes' tab, scroll down untill you find your programs name.
                                    p.Kill()
                                End If
                            Next
                            Label1.Text = "removeing old version..."
                            IO.File.Delete(Application.StartupPath & "\app.exe")
                            Label1.Text = "Downloading update version..."
                            WC.DownloadFileAsync(New Uri("http://www.sobhansoft.com/products/atafs/atafs-lastest.exe"), "atafs.exe")
                            Label1.Text = "App Updated Successfuly."
                        Else
                            Label2.Text = ("You have the latest version of app.")
                            Label2.ForeColor = Color.Red
                        End If
                    Else
                        Me.Close()
                    End If
                End Using
            End Using
        Catch
            Label1.Text = "Error Connecting to the Server..."
            Label2.Text = ("Please Check your Internet")
            Label2.ForeColor = Color.Red
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub



    Private Sub WC_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        p.Value = e.ProgressPercentage
    End Sub

   
End Class
