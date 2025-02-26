Public Class Form1
    Public machchoice(99) As Boolean
    Public Machinelist As New DataTable
    Public chosen As DataColumn = Machinelist.Columns.Add("Display", Type.GetType("System.Boolean"))
    Public ID As DataColumn = Machinelist.Columns.Add("ID", Type.GetType("System.String"))
    Public workcenter As DataColumn = Machinelist.Columns.Add("WCID", Type.GetType("System.String"))
    Public descript As DataColumn = Machinelist.Columns.Add("Description", Type.GetType("System.String"))

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Dim j%
            Do
                stringreader = filereader.ReadLine()
                j% = Val(Trim(stringreader))
                If j% < 1 Then Exit Do
                machchoice(j%) = True
            Loop
        End If

        Dim intCursor% = 0
        Do Until intCursor = DataSet1.workcenterlist.Rows.Count
            Dim workrow As DataRow = Machinelist.NewRow
            workrow(ID) = DataSet1.workcenterlist.Item(intCursor).ID
            If machchoice(Int(workrow(ID))) Then
                workrow(chosen) = True
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

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Machines" Then
            DataGridView1.Visible = True
            Button1.Text = "Select"
        Else

            ' write or overwrite MTCgrid.txt


            DataGridView1.Visible = False
            Button1.Text = "Machines"
        End If
    End Sub

End Class
