Public Class Frm_T2

    Dim machineping As String
    Dim machinecall As String

    Dim yPos% = 418
    Dim Spindleload% = 418
    Dim S2rpm% = 418
    Dim S2load% = 418
    Dim Xaxisload% = 418
    Dim Yaxisload% = 418
    Dim Zaxisload% = 418

    Dim redpen As New Pen(Brushes.Red, 20)
    Dim blupen As New Pen(Brushes.Blue, 20)
    Dim grnpen As New Pen(Brushes.Green, 20)
    Dim gridpen As New Pen(Brushes.DarkGray, 1)

    Private Sub Unit_Actual_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawLine(gridpen, 20, 120, 332, 120)
        e.Graphics.DrawLine(gridpen, 20, 270, 332, 270)
        e.Graphics.DrawLine(gridpen, 20, 195, 332, 195)
        e.Graphics.DrawLine(gridpen, 20, 345, 332, 345)
        e.Graphics.DrawLine(redpen, 40, yPos, 40, 420)
        e.Graphics.DrawLine(grnpen, 86, Spindleload, 86, 420)
        e.Graphics.DrawLine(redpen, 132, S2rpm, 132, 420)
        e.Graphics.DrawLine(grnpen, 178, S2load, 178, 420)
        e.Graphics.DrawLine(blupen, 224, Xaxisload, 224, 420)
        e.Graphics.DrawLine(blupen, 270, Yaxisload, 270, 420)
        e.Graphics.DrawLine(blupen, 316, Zaxisload, 316, 420)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If My.Computer.Network.Ping(machineping) Then
            ' nothing
        Else
            Exit Sub
        End If

        ' add predicted time to display ? ***************************************************************************************
        ' add partname to display ? *********************************************************************************************

        DataSet15.Clear()
        DataSet15.ReadXml(machinecall)
        'DataGridView1.DataSource = DataSet15

        TextBox1.Text = DataSet15.Tables("RotaryVelocity").Rows(0).Item(4)
        TextBox12.Text = DataSet15.Tables("RotaryVelocity").Rows(2).Item(4)
        yPos = 419 - Int(DataSet15.Tables("RotaryVelocity").Rows(0).Item(4) / 13)
        Try
            S2rpm = 419 - Int(DataSet15.Tables("RotaryVelocity").Rows(2).Item(4) / 13)
        Catch
            S2rpm = 419
            TextBox12.Text = "0"
        End Try
        TextBox2.Text = DataSet15.Tables("PartCount").Rows(0).Item(4)

        Dim opnofound As Integer = InStr(Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4)), ":", CompareMethod.Text) + 1
        TextBox10.Text = Mid((Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4))), opnofound, 4)
        Dim jobnofound As Integer = InStr(opnofound, Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4)), ":", CompareMethod.Text) + 1
        TextBox9.Text = Mid((Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4))), jobnofound, 5)



        'TextBox9.Text = Mid((Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4))), 1, 5) ' job number
        'TextBox10.Text = Mid((Trim(DataSet15.Tables("ProgramComment").Rows(1).Item(4))), 7, 4) ' operation number


        TextBox3.Text = DataSet15.Tables("Load").Rows(1).Item(3)
        TextBox11.Text = DataSet15.Tables("Load").Rows(5).Item(3)
        Try
            Spindleload = 419 - Int(DataSet15.Tables("Load").Rows(1).Item(3) * 3)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Try
            S2load = 419 - Int(DataSet15.Tables("Load").Rows(5).Item(3) * 3)
        Catch ex As Exception
            S2load = 419
            TextBox11.Text = "0"
            'MsgBox(ex.Message)
        End Try

        TextBox4.Text = DataSet15.Tables("Load").Rows(7).Item(3)
        Xaxisload = 419 - Int(DataSet15.Tables("Load").Rows(7).Item(3) * 3)
        TextBox5.Text = DataSet15.Tables("Load").Rows(8).Item(3)
        Yaxisload = 419 - Int(DataSet15.Tables("Load").Rows(8).Item(3) * 3)
        TextBox6.Text = DataSet15.Tables("Load").Rows(9).Item(3)
        Zaxisload = 419 - Int(DataSet15.Tables("Load").Rows(9).Item(3) * 3)
        TextBox7.Text = DataSet15.Tables("PathFeedrateOverride").Rows(0).Item(4)
        Select Case Val(TextBox7.Text)
            Case < 25
                TextBox7.BackColor = Color.Magenta
            Case < 50
                TextBox7.BackColor = Color.Red
            Case < 100
                TextBox7.BackColor = Color.Yellow
            Case Else
                TextBox7.BackColor = Color.LightGreen
        End Select
        TextBox8.Text = DataSet15.Tables("PathFeedrateOverride").Rows(1).Item(4)
        Select Case Val(TextBox8.Text)
            Case < 25
                TextBox8.BackColor = Color.Magenta
            Case < 50
                TextBox8.BackColor = Color.Red
            Case < 100
                TextBox8.BackColor = Color.Yellow
            Case Else
                TextBox8.BackColor = Color.LightGreen
        End Select
        TextBox13.Text = DataSet15.Tables("EmergencyStop").Rows(0).Item(3)
        If TextBox13.Text = "TRIGGERED" Then
            TextBox13.BackColor = Color.Red
        Else
            TextBox13.BackColor = Color.LimeGreen
        End If

        'Refresh()
        Invalidate()

    End Sub

    Private Sub Frm_Unit_Monitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Form1.machident
        machineping = Form1.netaddress
        machinecall = Form1.machdatasource
        If My.Computer.Network.Ping(machineping) Then

            'DataSet15.Clear()
            'DataSet15.ReadXml(machinecall)
            'DataGridView1.DataSource = DataSet15
            'DataGridView1.Refresh()

            Timer1.Enabled = True
        Else
            MsgBox("Machine Not Avilable.")
            Timer1.Enabled = False
            Me.Dispose()
        End If
    End Sub

    Private Sub TextBox9_DoubleClick(sender As Object, e As EventArgs) Handles TextBox9.DoubleClick
        ' doubleclick to get individual job screen
        FrmJobView.searchjobnumber = Trim(TextBox9.Text)
        FrmJobView.TextBox1.Text = Trim(TextBox9.Text)
        FrmJobView.Show()
        FrmJobView.Button1.Focus()
        FrmJobView.Button1.PerformClick()

    End Sub
End Class