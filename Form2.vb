Public Class Form2

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

    Public segmentend(999) As Single
    Public segmentcolor(999) As Integer
    Public segmentcount As Integer

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

        Dim rulepen As New Pen(Brushes.Violet, 1)
        For x% = 15 To 1215 Step 100
            e.Graphics.DrawLine(rulepen, x%, 0, x%, 45)
        Next

        Dim redpen As New Pen(Color.Red, 15)
        Dim yellowpen As New Pen(Color.Yellow, 15)
        Dim greenpen As New Pen(Color.LimeGreen, 15)
        Dim graypen As New Pen(Color.DimGray, 15)
        For x% = 1 To segmentcount
            Select Case segmentcolor(x%)
                Case 1
                    e.Graphics.DrawLine(redpen, segmentend(x% - 1) + 15, 23, segmentend(x%) + 15, 23)
                Case 2
                    e.Graphics.DrawLine(yellowpen, segmentend(x% - 1) + 15, 23, segmentend(x%) + 15, 23)
                Case 3
                    e.Graphics.DrawLine(greenpen, segmentend(x% - 1) + 15, 23, segmentend(x%) + 15, 23)
                Case 4
                    e.Graphics.DrawLine(graypen, segmentend(x% - 1) + 15, 23, segmentend(x%) + 15, 23)
                Case Else
                    ' nothing
            End Select
        Next

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

        'Dim pagegraphics As Graphics = FlowLayoutPanel1.CreateGraphics
        'Dim rulepen As New Pen(Brushes.Violet, 1)
        'For x% = 0 To 1200 Step 100
        '    pagegraphics.DrawLine(rulepen, x%, 10, x%, 700)
        'Next




        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.WorkcenterlistTableAdapter.Fill(Me.DataSet1.workcenterlist)
        Me.MtcMasterTableAdapter.ClearBeforeFill = True
        Me.MtcMasterTableAdapter.FillByToday(Me.DataSet1.MTCMaster)         ' - get today's master data
        CurrentDate.Clear()                                                 ' - clear memory datatable

        For i% = 0 To DataSet1.MTCMaster.Rows.Count - 1
            Dim workrow As DataRow = CurrentDate.NewRow                     ' - working datatable
            workrow(WorkCID) = Trim(DataSet1.MTCMaster.Item(i%).WCID)       ' - workcenter
            workrow(MarkTime) = DataSet1.MTCMaster.Item(i%).Nowtime         ' - data timestamp
            workrow(StatusChange) = DataSet1.MTCMaster.Item(i%).Running     ' - boolean - running or not (T/F)
            CurrentDate.Rows.Add(workrow)
        Next

        For z% = 1 To boxtally                                              ' - quantity of displayed machines
            segmentcount = 1                                                ' - line segment count
            Dim onflag As Boolean                                           ' - machine - running or not (T/F)
            Dim onmem As Boolean = True                                     ' - last running status
            'Dim onmachine As Boolean                                        ' - workcenter
            Dim lastime As Date = shiftstart                                ' - last time memory
            Dim machineOn As Boolean                                        ' - is the machine available (ON)
            Dim changetime As Date
            Dim tallytime As TimeSpan
            segmentend(segmentcount) = 0                                    ' - line segment endpoint - starts at zero
            segmentcolor(segmentcount) = 1                                  ' - default line segment color is red
            Dim colormem As Short = 1                                       ' - set line segment color red
            Dim countstart As Integer = 0
            Dim searchvalue = Trim(chosenmachines(z))                       ' - go thru all displayed machines
            If CurrentDate.Rows(0)(MarkTime) > shiftstart Then              ' - if first record time > shift start 
                segmentcolor(segmentcount) = 4                                                ' - make segment color gray 
                changetime = CurrentDate.Rows(0)(MarkTime)                  ' - timestamp for status change
                tallytime = changetime - lastime                            ' - calculate time since last status change
                segmentend(segmentcount) = segmentend(segmentcount - 1) + tallytime.TotalHours * 100    ' - calculate line segment end
                lastime = changetime                                        ' - set last staus change time memory
                segmentcount += 1                                           ' - increment line segment count
                countstart = 1
                colormem = 1
            End If
            Dim currentrow As DataRow() = DataSet1.workcenterlist.Select("WCID = '" + searchvalue + "'")
            If currentrow(0)("Available") = True Then
                colormem = 2                                                ' - set segment color yellow
                machineOn = True
            Else
                colormem = 1                                                ' - set segment color red
                machineOn = False
            End If
            For i% = countstart To CurrentDate.Rows.Count - 1
                'colormem = 1
                If CurrentDate.Rows(i%)(WorkCID) = searchvalue Then
                    'onmachine = True                                        ' - selected machine is available
                    If CurrentDate.Rows(i%)(StatusChange) Then              ' - read running or not
                        onflag = True                                       ' - set flag running
                    Else
                        onflag = False                                      ' - set flag not running
                    End If
                    changetime = CurrentDate.Rows(i%)(MarkTime)             ' - timestamp for status change
                    If changetime < shiftstart Then                         ' - if timestamp before shiftstart - ignore it
                        ' nothing
                    Else
                        If onflag Xor onmem Then                            ' - if running status changed then
                            onmem = onflag                                  ' - set last running status
                            If onmem Then                                   ' - if last segment running then new color is yellow - not running, green
                                segmentcolor(segmentcount) = 2              ' - set segment color yellow
                                colormem = 3
                            Else
                                segmentcolor(segmentcount) = 3              ' - set segment color green
                                colormem = 2
                            End If
                            tallytime = changetime - lastime                ' - calculate time since last status change
                            segmentend(segmentcount) = segmentend(segmentcount - 1) + tallytime.TotalHours * 100    ' - calculate line segment end
                            lastime = changetime                            ' - set last staus change time memory
                            segmentcount += 1                               ' - increment line segment count
                            'segmentcolor(segmentcount) = 1
                        Else
                            ' nothing
                        End If
                    End If
                End If
            Next
            If Not machineOn Then
                colormem = 1
            End If
            Dim Interval As TimeSpan = Now - shiftstart
            segmentend(segmentcount) = Interval.TotalHours * 100
            segmentcolor(segmentcount) = colormem
            machinebox(z).Refresh()
            'onmachine = False
            Array.Clear(segmentend, 0, 999)
            Array.Clear(segmentcolor, 0, 999)
        Next

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