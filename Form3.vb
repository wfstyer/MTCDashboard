Public Class Form3


    Public shifttime As Date = TimeValue(" 5:00 AM")
    Public showdated As Date = DateValue("1999-01-01")
    Public showdate2 As Date = DateValue("1999-01-01")

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

    Public segmentend(9999) As Single
    Public segmentcolor(9999) As Integer
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

        Dim redpen As New Pen(Color.Red, 24)
        Dim yellowpen As New Pen(Color.Yellow, 24)
        Dim greenpen As New Pen(Color.LimeGreen, 24)
        Dim graypen As New Pen(Color.DimGray, 24)
        For x% = 1 To segmentcount
            Select Case segmentcolor(x%)
                Case 1
                    e.Graphics.DrawLine(redpen, segmentend(x% - 1) + 15, 27, segmentend(x%) + 15, 27)
                Case 2
                    e.Graphics.DrawLine(yellowpen, segmentend(x% - 1) + 15, 27, segmentend(x%) + 15, 27)
                Case 3
                    e.Graphics.DrawLine(greenpen, segmentend(x% - 1) + 15, 27, segmentend(x%) + 15, 27)
                Case 4
                    e.Graphics.DrawLine(graypen, segmentend(x% - 1) + 15, 27, segmentend(x%) + 15, 27)
                Case Else
                    ' nothing
            End Select
        Next

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MonthCalendar1.Visible = True
        'showdated = Form2.showdate

        Me.WorkcenterlistTableAdapter.ClearBeforeFill = True
        Me.WorkcenterlistTableAdapter.Fill(Me.DataSet1.workcenterlist)
        showdate2 = showdated.AddDays(1)

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


                boxcount += 1
            End If
            intCursor += 1
        Loop
        boxtally = boxcount - 1

    End Sub

    Private Sub ApplyDate()
        Me.MtcMasterTableAdapter.ClearBeforeFill = True
        Me.MtcMasterTableAdapter.FillBy(Me.DataSet1.MTCMaster, showdated, showdate2)         ' - get today's master data
        CurrentDate.Clear()                                                 ' - clear memory datatable
        Dim shiftstart As New DateTime(showdated.Year, showdated.Month, showdated.Day, shifttime.Hour, shifttime.Minute, shifttime.Second)

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
            Dim onmem As Boolean                                            ' - last running status
            Dim lastime As Date = shiftstart                                ' - last time memory
            Dim machineOn As Boolean                                        ' - is the machine available (ON)
            Dim changetime As Date
            Dim tallytime As TimeSpan
            segmentend(segmentcount) = 0                                    ' - line segment endpoint - starts at zero
            segmentcolor(segmentcount) = 1                                  ' - default line segment color is red
            Dim colormem As Short = 1                                       ' - set line segment color red
            Dim countstart As Integer = 0
            Dim searchvalue = Trim(chosenmachines(z))                       ' - go thru all displayed machines
            Try
                If CurrentDate.Rows(0)(MarkTime) > shiftstart Then              ' - if first record time > shift start 
                    segmentcolor(segmentcount) = 4                              ' - make segment color gray 
                    changetime = CurrentDate.Rows(0)(MarkTime)                  ' - timestamp for status change
                    tallytime = changetime - lastime                            ' - calculate time since last status change
                    segmentend(segmentcount) = tallytime.TotalHours * 100       ' - calculate line segment end
                    lastime = changetime                                        ' - set last status change time memory
                    segmentcount = 2                                            ' - increment line segment count
                    countstart = 1
                End If
                For i% = countstart To CurrentDate.Rows.Count - 1
                    If CurrentDate.Rows(i%)(WorkCID) = searchvalue Then
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
                                lastime = changetime                            ' - set last status change time memory
                                segmentcount += 1                               ' - increment line segment count
                                'segmentcolor(segmentcount) = 1
                            Else
                                ' nothing
                            End If
                        End If
                    End If
                Next
            Catch
            End Try
            If Not machineOn Then
                colormem = 1                                                ' - set segment color red
            End If
            Dim Interval As TimeSpan = shiftstart.AddHours(12) - shiftstart

            segmentend(segmentcount) = Interval.TotalHours * 100
            segmentcolor(segmentcount) = colormem
            machinebox(z).Refresh()
            Array.Clear(segmentend, 0, 9999)
            Array.Clear(segmentcolor, 0, 9999)

        Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Form1.Timer1.Enabled = True
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MonthCalendar1.Visible = True
    End Sub

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        showdated = e.Start.ToShortDateString
        If showdated >= Today Then
            MsgBox("Pick a Date Before Today")
            Exit Sub
        End If
        TextBox1.Text = showdated
        showdate2 = showdated.AddDays(1)
        MonthCalendar1.Visible = False
        ApplyDate()
    End Sub

End Class