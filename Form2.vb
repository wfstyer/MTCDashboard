﻿Public Class Form2

    Public shifttime As Date = TimeValue(" 5:59 AM")
    Public shiftstart As New DateTime(Today.Year, Today.Month, Today.Day, shifttime.Hour, shifttime.Minute, shifttime.Second)

    Public machchoice(99) As Boolean

    Public machinebox(-1) As GroupBox

    Public z As Integer
    Public boxtally As Integer
    Public chosenmachines(99) As String

    Public infobox1(-1) As TextBox
    Public infobox2(-1) As TextBox
    Public infobox3(-1) As TextBox
    Public infobox4(-1) As TextBox
    Public infobox5(-1) As TextBox
    Public infobox6(-1) As TextBox

    Public cmdbtn(-1) As Button

    Public infolbl1(-1) As Label
    Public infolbl2(-1) As Label
    Public infolbl3(-1) As Label
    Public infolbl4(-1) As Label
    Public infolbl5(-1) As Label
    Public infolbl6(-1) As Label

    Public grnAngle(99) As Single
    Public yelAngle(99) As Single
    Public redAngle(99) As Single

    Public linestart As Integer
    Public linend As Integer
    Public linecolor As Integer

    Public Machinelist As New DataTable
    Public chosen As DataColumn = Machinelist.Columns.Add("Display", Type.GetType("System.Boolean"))
    Public ID As DataColumn = Machinelist.Columns.Add("ID", Type.GetType("System.String"))
    Public workcenter As DataColumn = Machinelist.Columns.Add("WCID", Type.GetType("System.String"))
    Public descript As DataColumn = Machinelist.Columns.Add("Description", Type.GetType("System.String"))

    Public CurrentDate As New DataTable
    Public WorkCID As DataColumn = CurrentDate.Columns.Add("WCID", Type.GetType("System.String"))
    Public MarkTime As DataColumn = CurrentDate.Columns.Add("MarkTime", Type.GetType("System.DateTime"))
    Public StatusChange As DataColumn = CurrentDate.Columns.Add("OnOff", Type.GetType("System.Boolean"))

    Private Sub GroupBox_Paint(sender As Object, e As PaintEventArgs)
        'Dim rect As New Rectangle(30, 55, 140, 140)
        'Create start and sweep angles on ellipse.
        'Dim firstAngle As Single = 0F
        'Using grnbrush As New SolidBrush(Color.LimeGreen)
        '    e.Graphics.FillPie(grnbrush, rect, 0, grnAngle(z))
        'End Using
        'Using yelbrush As New SolidBrush(Color.Yellow)
        '    e.Graphics.FillPie(yelbrush, rect, grnAngle(z), yelAngle(z))
        'End Using

        Select Case linecolor
            Case 1
                Using redline As New Pen(Color.Red, 15)
                    e.Graphics.DrawLine(redline, linestart + 15, 23, linend + 15, 23)
                End Using
            Case 2
                Using redline As New Pen(Color.Yellow, 15)
                    e.Graphics.DrawLine(redline, linestart + 15, 23, linend + 15, 23)
                End Using
            Case 3
                Using redline As New Pen(Color.LimeGreen, 15)
                    e.Graphics.DrawLine(redline, linestart + 15, 23, linend + 15, 23)
                End Using
            Case Else
                ' nothing
        End Select
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WorkcenterlistTableAdapter.ClearBeforeFill = True
        Me.WorkcenterlistTableAdapter.Fill(Me.DataSet1.workcenterlist)

        For i = 0 To 99
            machchoice(i) = False
        Next

        Dim machselect As String = Environ("USERPROFILE") & "\AppData\Roaming\MTCgrid.txt"

        If System.IO.File.Exists(machselect) Then
            Dim filereader As System.IO.StreamReader
            filereader = My.Computer.FileSystem.OpenTextFileReader(machselect)
            Dim stringreader As String
            Dim j$
            Dim i% = 1
            Do
                stringreader = filereader.ReadLine()
                j$ = Trim(stringreader)
                If Val(j$) < 1 Then Exit Do
                machchoice(Val(j$)) = True
                i% += 1
            Loop
            filereader.Dispose()
        End If

        Dim k% = 1
        Dim intCursor% = 0
        Do Until intCursor = DataSet1.workcenterlist.Rows.Count
            Dim workrow As DataRow = Machinelist.NewRow
            workrow(ID) = DataSet1.workcenterlist.Item(intCursor).ID
            If machchoice(Int(workrow(ID))) Then
                workrow(chosen) = True
                chosenmachines(k%) = DataSet1.workcenterlist.Item(intCursor).WCID
                k% += 1
            Else
                workrow(chosen) = False
            End If
            workrow(workcenter) = DataSet1.workcenterlist.Item(intCursor).WCID
            workrow(descript) = DataSet1.workcenterlist.Item(intCursor).DESCRIPTION
            Machinelist.Rows.Add(workrow)
            intCursor += 1
        Loop
        DataGridView1.DataSource = Machinelist
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        Dim wc As String
        Dim desc As String

        intCursor% = 0
        Dim boxcount As Integer = 1
        Do Until intCursor = Machinelist.Rows.Count
            If Machinelist.Rows.Item(intCursor)(chosen) Then
                wc = Machinelist.Rows.Item(intCursor)(workcenter)
                desc = Machinelist.Rows.Item(intCursor)(descript)

                ReDim Preserve machinebox(boxcount)
                machinebox(boxcount) = New GroupBox With {
                    .Width = 1230,
                    .Height = 40,
                    .Text = wc + " - " + desc
                }
                FlowLayoutPanel1.Controls.Add(machinebox(boxcount))

                AddHandler machinebox(boxcount).Paint, AddressOf GroupBox_Paint

                'ReDim Preserve infolbl1(boxcount)
                'infolbl1(boxcount) = New Label With {
                '    .Text = "Off",
                '    .Left = 18,
                '    .Top = 200,
                '    .Width = 21,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl1(boxcount))

                'ReDim Preserve infolbl2(boxcount)
                'infolbl2(boxcount) = New Label With {
                '    .Text = "Idle",
                '    .Left = 66,
                '    .Top = 200,
                '    .Width = 24,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl2(boxcount))

                'ReDim Preserve infolbl3(boxcount)
                'infolbl3(boxcount) = New Label With {
                '    .Text = "Run",
                '    .Left = 114,
                '    .Top = 200,
                '    .Width = 27,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl3(boxcount))

                'ReDim Preserve infolbl4(boxcount)
                'infolbl4(boxcount) = New Label With {
                '    .Text = "Job#",
                '    .Left = 15,
                '    .Top = 39,
                '    .Width = 31,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl4(boxcount))

                'ReDim Preserve infolbl5(boxcount)
                'infolbl5(boxcount) = New Label With {
                '    .Text = "Op#",
                '    .Left = 86,
                '    .Top = 39,
                '    .Width = 28,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl5(boxcount))

                'ReDim Preserve infolbl6(boxcount)
                'infolbl6(boxcount) = New Label With {
                '    .Text = "Count",
                '    .Left = 152,
                '    .Top = 39,
                '    .Width = 35,
                '    .Height = 13
                '}
                'machinebox(boxcount).Controls.Add(infolbl6(boxcount))

                'ReDim Preserve infobox1(boxcount)
                'infobox1(boxcount) = New TextBox With {
                '    .Left = 4,
                '    .Top = 215,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox1(boxcount))

                'ReDim Preserve infobox2(boxcount)
                'infobox2(boxcount) = New TextBox With {
                '    .Left = 54,
                '    .Top = 215,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox2(boxcount))

                'ReDim Preserve infobox3(boxcount)
                'infobox3(boxcount) = New TextBox With {
                '    .Left = 104,
                '    .Top = 215,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox3(boxcount))

                'ReDim Preserve infobox4(boxcount)
                'infobox4(boxcount) = New TextBox With {
                '    .Left = 6,
                '    .Top = 17,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox4(boxcount))

                'ReDim Preserve infobox5(boxcount)
                'infobox5(boxcount) = New TextBox With {
                '    .Left = 76,
                '    .Top = 17,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox5(boxcount))

                'ReDim Preserve infobox6(boxcount)
                'infobox6(boxcount) = New TextBox With {
                '    .Left = 145,
                '    .Top = 17,
                '    .Width = 48,
                '    .Height = 20
                '}
                'machinebox(boxcount).Controls.Add(infobox6(boxcount))

                'ReDim Preserve cmdbtn(boxcount)
                'cmdbtn(boxcount) = New Button With {
                '    .Text = "Show",
                '    .Left = 154,
                '    .Top = 213,
                '    .Width = 42,
                '    .Height = 23
                '}
                'machinebox(boxcount).Controls.Add(cmdbtn(boxcount))

                boxcount += 1
            End If
            intCursor += 1
        Loop
        boxtally = boxcount - 1
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.WorkcenterlistTableAdapter.Fill(Me.DataSet1.workcenterlist)
        Me.MtcMasterTableAdapter.ClearBeforeFill = True
        Me.MtcMasterTableAdapter.FillByToday(Me.DataSet1.MTCMaster)
        CurrentDate.Clear()

        For i% = 0 To DataSet1.MTCMaster.Rows.Count - 1
            Dim workrow As DataRow = CurrentDate.NewRow
            workrow(WorkCID) = Trim(DataSet1.MTCMaster.Item(i%).WCID)
            workrow(MarkTime) = DataSet1.MTCMaster.Item(i%).Nowtime
            workrow(StatusChange) = DataSet1.MTCMaster.Item(i%).Running
            CurrentDate.Rows.Add(workrow)
        Next


        'Dim rulepen As New Pen(Brushes.Violet, 1)



        'Dim totaltime As Double
        'Dim idletime As Int32
        'Dim cycletime As Int32

        'totaltime = DateDiff(DateInterval.Hour, shiftstart, Now)

        '        TextBox1.Text = linend

        For z% = 1 To boxtally
            Dim Interval As TimeSpan = Now - shiftstart
            linend = Int(Interval.TotalHours * 100)
            Dim onflag As Boolean
            Dim onmem As Boolean = True
            Dim onmachine As Boolean
            Dim lastime As Date = shiftstart
            Dim changetime As Date
            Dim tallytime As TimeSpan
            linestart = 0

            Dim searchvalue = Trim(chosenmachines(z))
            For i% = 0 To CurrentDate.Rows.Count - 1
                If CurrentDate.Rows(i%)(WorkCID) = searchvalue Then
                    onmachine = True
                    changetime = CurrentDate.Rows(i%)(MarkTime)
                    If changetime < shiftstart Then
                        ' nothing
                    Else
                        If CurrentDate.Rows(i%)(StatusChange) Then
                            onflag = True
                        Else
                            onflag = False
                        End If
                        If onflag Xor onmem Then
                            onmem = onflag
                            If onmem Then
                                linecolor = 3
                            Else
                                linecolor = 2
                            End If
                            tallytime = changetime - lastime
                            linend = Int(tallytime.TotalHours * 100)
                            machinebox(z).Refresh()
                            lastime = changetime
                            linestart = linend
                        Else
                            ' nothing
                        End If
                    End If
                End If
            Next
            If onmachine Then
                ' nothing
            Else
                linecolor = 1
                machinebox(z).Refresh()
            End If
            onmachine = False
        Next

        'Dim searchtext = "WCID = '" + searchvalue + "'"
        'Dim myrow() As DataRow
        'myrow = DataSet1.MTCMaster.Select(searchtext)
        '    idletime = Val(myrow(0)("Idle_Time"))
        '    cycletime = Val(myrow(0)("Run_Time"))
        '    Dim machineon As Boolean = myrow(0)("Available")
        '    Dim machinerunning As Boolean = myrow(0)("Running")
        '    If machinerunning Then
        '        ' running
        '        infobox1(z).BackColor = Color.White
        '        infobox2(z).BackColor = Color.White
        '        infobox3(z).BackColor = Color.LimeGreen
        '    Else
        '        If machineon Then
        '            ' idle
        '            infobox1(z).BackColor = Color.White
        '            infobox2(z).BackColor = Color.Yellow
        '            infobox3(z).BackColor = Color.White
        '        Else
        '            ' off
        '            infobox1(z).BackColor = Color.Red
        '            infobox2(z).BackColor = Color.White
        '            infobox3(z).BackColor = Color.White
        '        End If
        '    End If

        '    Dim offtime As Integer
        '    offtime = totaltime - idletime - cycletime

        '    grnAngle(z) = 360 * (cycletime / totaltime)
        '    yelAngle(z) = 360 * (idletime / totaltime)
        '    redAngle(z) = 360 - grnAngle(z) - yelAngle(z)

        '    Dim iminutestoseconds As Int32 = idletime
        '    Dim ihms = TimeSpan.FromSeconds(iminutestoseconds)
        '    Dim ihr = Format(ihms.Hours, "#0")
        '    Dim imin = Format(ihms.Minutes, "00")
        '    Dim isec = Format(ihms.Seconds, "00")

        '    Dim cminutestoseconds As Int32 = cycletime
        '    Dim chms = TimeSpan.FromSeconds(cminutestoseconds)
        '    Dim chr = Format(chms.Hours, "#0")
        '    Dim cmin = Format(chms.Minutes, "00")
        '    Dim csec = Format(chms.Seconds, "00")

        '    Dim ominutestoseconds As Int32 = offtime
        '    Dim ohms = TimeSpan.FromSeconds(ominutestoseconds)
        '    Dim ohr = Format(ohms.Hours, "#0")
        '    Dim omin = Format(ohms.Minutes, "00")
        '    Dim osec = Format(ohms.Seconds, "00")

        '    infobox1(z).Text = ohr + ":" + omin + ":" + osec
        '    infobox2(z).Text = ihr + ":" + imin + ":" + isec
        '    infobox3(z).Text = chr + ":" + cmin + ":" + csec
        '    infobox4(z).Text = myrow(0)("Jobno")
        '    infobox5(z).Text = myrow(0)("Opno")
        '    infobox6(z).Text = myrow(0)("Count")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Me.Hide()
        'Timer1.Enabled = False
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Machines" Then
            DataGridView1.Visible = True
            Button1.Text = "Select"
        Else
            Button1.BackColor = Color.Red
            For i = 1 To 99
                machchoice(i) = False
            Next

            Dim machselect As String = Environ("USERPROFILE") & "\AppData\Roaming\MTCgrid.txt"

            If System.IO.File.Exists(machselect) Then
                System.IO.File.WriteAllText(machselect, "")
            Else
                System.IO.File.Create(machselect).Dispose()
            End If

            Dim filewriter As System.IO.StreamWriter
            filewriter = My.Computer.FileSystem.OpenTextFileWriter(machselect, True)
            Dim intCursor% = 0
            Do Until intCursor = DataSet1.workcenterlist.Rows.Count
                If Machinelist.Rows.Item(intCursor)(chosen) Then
                    filewriter.WriteLine(Machinelist.Rows.Item(intCursor)(ID))
                End If
                intCursor += 1
            Loop
            filewriter.Dispose()
            DataGridView1.Visible = False
            Button1.Text = "Machines"
            Button1.BackColor = Color.LightGreen
        End If

    End Sub
End Class