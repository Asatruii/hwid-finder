Imports System.Management

Public Class Form1
    ' Apply dark theme on form load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set dark theme colors
        Me.BackColor = Color.FromArgb(30, 30, 30) ' Dark background

        ' Apply to all controls
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Or TypeOf ctrl Is Button Then
                ctrl.ForeColor = Color.Black ' White text
            End If
            If TypeOf ctrl Is TextBox Then
                ctrl.BackColor = Color.FromArgb(45, 45, 45) ' Dark gray
                ctrl.ForeColor = Color.White ' White text for readability
            End If
        Next
    End Sub

    ' Button click event to retrieve HWID
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnHWID.Click
        Try
            Dim hwid As String = GetHWID()
            txtOutput.Text = hwid
        Catch ex As Exception
            txtOutput.Text = "Error retrieving HWID: " & ex.Message
        End Try
    End Sub

    ' Function to get HWID using WMI
    Private Function GetHWID() As String
        Try
            Dim mc As New ManagementClass("Win32_ComputerSystemProduct")
            Dim moc As ManagementObjectCollection = mc.GetInstances()

            For Each mo As ManagementObject In moc
                Dim uuid = mo.GetPropertyValue("UUID")
                If uuid IsNot Nothing Then
                    Return uuid.ToString()
                End If
            Next

        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try

        Return "Unknown HWID"
    End Function
End Class
