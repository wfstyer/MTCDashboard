Public Class Frm_M3

    Dim machineping As String
    Dim machinecall As String

    Dim yPos% = 418
    Dim Spindleload% = 418
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
        e.Graphics.DrawLine(grnpen, 109, Spindleload, 109, 420)
        e.Graphics.DrawLine(blupen, 178, Xaxisload, 178, 420)
        e.Graphics.DrawLine(blupen, 247, Yaxisload, 247, 420)
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

        DataSet16.Clear()
        DataSet16.ReadXml(machinecall)
        'DataGridView1.DataSource = DataSet16

        TextBox1.Text = DataSet16.Tables("RotaryVelocity").Rows(0).Item(5)
        yPos = 419 - Int(DataSet16.Tables("RotaryVelocity").Rows(0).Item(5) / 13)
        TextBox2.Text = DataSet16.Tables("PartCount").Rows(0).Item(4)

        Dim opnofound As Integer = InStr(Trim(DataSet16.Tables("ProgramComment").Rows(0).Item(5)), ":", CompareMethod.Text) + 1
        TextBox10.Text = Mid((Trim(DataSet16.Tables("ProgramComment").Rows(0).Item(5))), opnofound, 4)
        Dim jobnofound As Integer = InStr(opnofound, Trim(DataSet16.Tables("ProgramComment").Rows(0).Item(5)), ":", CompareMethod.Text) + 1
        TextBox9.Text = Mid((Trim(DataSet16.Tables("ProgramComment").Rows(0).Item(5))), jobnofound, 5)



        'TextBox9.Text = Mid((Trim(DataSet16.Tables("ProgramComment").Rows(1).Item(4))), 1, 5) ' job number
        'TextBox10.Text = Mid((Trim(DataSet16.Tables("ProgramComment").Rows(1).Item(4))), 7, 4) ' operation number


        TextBox3.Text = DataSet16.Tables("Load").Rows(1).Item(4)
        Try
            Spindleload = 419 - Int(DataSet16.Tables("Load").Rows(1).Item(4) * 3)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        TextBox4.Text = DataSet16.Tables("Load").Rows(2).Item(4)
        Xaxisload = 419 - Int(DataSet16.Tables("Load").Rows(2).Item(4) * 3)
        TextBox5.Text = DataSet16.Tables("Load").Rows(3).Item(4)
        Yaxisload = 419 - Int(DataSet16.Tables("Load").Rows(3).Item(4) * 3)
        TextBox6.Text = DataSet16.Tables("Load").Rows(4).Item(4)
        Zaxisload = 419 - Int(DataSet16.Tables("Load").Rows(4).Item(4) * 3)
        TextBox7.Text = DataSet16.Tables("PathFeedrateOverride").Rows(0).Item(5)
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
        TextBox8.Text = DataSet16.Tables("PathFeedrateOverride").Rows(1).Item(5)
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
        TextBox13.Text = DataSet16.Tables("EmergencyStop").Rows(0).Item(4)
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

            'DataSet16.Clear()
            'DataSet16.ReadXml(machinecall)
            'DataGridView1.DataSource = DataSet16
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