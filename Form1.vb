Public Class Form1
    Public Machinelist As New DataTable
    Public chosen As DataColumn = Machinelist.Columns.Add("Display", Type.GetType("System.Boolean"))
    Public workcenter As DataColumn = Machinelist.Columns.Add("WCID", Type.GetType("System.String"))
    Public descript As DataColumn = Machinelist.Columns.Add("Description", Type.GetType("System.String"))

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet1.workcenterlist' table. You can move, or remove it, as needed.
        Me.WorkcenterlistTableAdapter.ClearBeforeFill = True
        Me.WorkcenterlistTableAdapter.Fill(Me.DataSet1.workcenterlist)
        'DataGridView1.Columns.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        'Machinelist.Clear()
        Dim intCursor% = 0
        Do Until intCursor = DataSet1.workcenterlist.Rows.Count
            Dim workrow As DataRow = Machinelist.NewRow
            workrow(chosen) = False
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
            DataGridView1.Visible = False
            Button1.Text = "Machines"
        End If
    End Sub

End Class
