Public Class FrmJobView


    Public searchjobnumber As String
    Public searchID$
    Public keynum%
    Public partstable As New DataTable
    Dim opno As DataColumn = partstable.Columns.Add("OP #", Type.GetType("System.String"))
    Dim opermemo As DataColumn = partstable.Columns.Add("Description", Type.GetType("System.String"))
    Dim wcent As DataColumn = partstable.Columns.Add("WorkCent", Type.GetType("System.String"))
    Dim opdate As DataColumn = partstable.Columns.Add("OpDate", Type.GetType("System.String"))
    Dim ddate As DataColumn = partstable.Columns.Add("Last Labor", Type.GetType("System.String"))
    Dim qtyfin As DataColumn = partstable.Columns.Add("Qty Finished", Type.GetType("System.Single"))
    Dim opdone As DataColumn = partstable.Columns.Add("OpDone", Type.GetType("System.String"))
    Dim partfind As New DataTable
    Dim partnum As DataColumn = partfind.Columns.Add("Part", Type.GetType("System.String"))
    Dim workcent As DataColumn = partfind.Columns.Add("Workcenter", Type.GetType("System.String"))
    Dim timein As DataColumn = partfind.Columns.Add("Time In", Type.GetType("System.String"))
    Dim buyitem As New DataTable
    Dim pono As DataColumn = buyitem.Columns.Add("PO#", Type.GetType("System.String"))
    Dim partno As DataColumn = buyitem.Columns.Add("Part Number", Type.GetType("System.String"))
    Dim descript As DataColumn = buyitem.Columns.Add("Description", Type.GetType("System.String"))
    Dim ordqty As DataColumn = buyitem.Columns.Add("Ordered", Type.GetType("System.String"))
    Dim rcptqty As DataColumn = buyitem.Columns.Add("Rcvd", Type.GetType("System.String"))

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' search job data
        Dim intCursor% = 0
        Dim i% = 0
        'Dim keynum%
        Dim searchjo As String
        partstable.Clear()
        searchjo = UCase(TextBox1.Text.ToString) + "-0000"
        DataGridView1.RowHeadersVisible = False
        DataGridView2.RowHeadersVisible = False
        DataGridView3.RowHeadersVisible = False
        DataGridView1.DataSource = partstable
        DataGridView1.Columns.Item(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        DataGridView1.Columns.Item(1).Width = 280
        DataGridView1.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns.Item(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns.Item(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns.Item(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns.Item(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns.Item(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells


        'Me.JodrtgTableAdapter1.FillByfjobno(Me.DataSet1.Jodrtg, searchjo)


        'Do Until intCursor = DataSet1.jodrtg.Rows.Count
        '    TextBox1.Text = DataSet1.Jodrtg.Rows(intCursor)(0).ToString
        '    TextBox3.Text = Trim(DataSet1.jodrtg.Item(intCursor).fsono)
        '    TextBox4.Text = Year(DataSet1.jodrtg.Item(intCursor).fact_rel)
        '    TextBox24.Text = Trim(DataSet1.jodrtg.Item(intCursor).fpartno)
        '    TextBox25.Text = Trim(DataSet1.jodrtg.Item(intCursor).fcompany)
        '    Dim jobqty As Integer = DataSet1.jodrtg.Item(intCursor).fquantity
        '    TextBox26.Text = jobqty
        '    TextBox27.Text = Trim(DataSet1.jodrtg.Item(intCursor).fddue_date)
        '    TextBox28.Text = Trim(DataSet1.jodrtg.Item(intCursor).fstatus)
        '    keynum% = DataSet1.jodrtg.Item(intCursor).identity_column

        '    Dim workrow As DataRow = partstable.NewRow()
        '    Dim duedate As Date = DataSet1.jodrtg.Item(intCursor).flastlab.ToShortDateString
        '    workrow(ddate) = duedate.ToShortDateString
        '    workrow(wcent) = Trim(DataSet1.jodrtg.Item(intCursor).fpro_id)
        '    workrow(opno) = Trim(DataSet1.jodrtg.Item(intCursor).foperno)
        '    workrow(opermemo) = Trim(DataSet1.jodrtg.Item(intCursor).fopermemo)
        '    Dim doneqty As Integer = DataSet1.jodrtg.Item(intCursor).fnqty_comp
        '    Dim identkey As String = Trim(DataSet1.jodrtg.Item(intCursor).Expr1)
        '    workrow(qtyfin) = doneqty

        '    workrow(opdone) = "N"
        '    If doneqty = jobqty Then
        '        workrow(opdone) = "Y"
        '        DataGridView1.Rows(i%).DefaultCellStyle.BackColor = Color.LightGreen
        '    Else
        '        Dim findkey As String = "FKey_ID = '" + identkey + "'"
        '        Me.Jodrtg_extTableAdapter1.FillBykey(Me.DataSet1.jodrtg_ext, identkey)
        '        Dim foundrow() As DataRow
        '        foundrow = DataSet1.jodrtg_ext.Select(findkey)
        '        Try
        '            If Not IsDBNull(foundrow(0)("CreatedDate")) Then
        '                Dim operationdate As DateTime = foundrow(0)("CreatedDate")
        '                workrow(opdate) = operationdate.ToShortDateString
        '            End If
        '        Catch ex As Exception
        '        End Try
        '        Try
        '            If Not IsDBNull(foundrow(0)("ModifiedDate")) Then
        '                If foundrow(0)("ModifiedDate") > "01/01/2000" Then
        '                    workrow(opdone) = "Y"
        '                End If
        '            End If
        '        Catch
        '        End Try
        '    End If
        '    partstable.Rows.Add(workrow)

        '    If doneqty < 1 And workrow(opdone) = "N" Then
        '        DataGridView1.Rows(i%).DefaultCellStyle.BackColor = Color.Pink
        '    ElseIf doneqty < jobqty And workrow(opdone) = "N" Then
        '        DataGridView1.Rows(i%).DefaultCellStyle.BackColor = Color.LightYellow
        '    Else
        '        DataGridView1.Rows(i%).DefaultCellStyle.BackColor = Color.LightGreen
        '    End If
        '    i% += 1
        '    intCursor += 1
        'Loop


        'Dim myRow() As DataRow
        'searchID$ = keynum.ToString

        'Me.Jomast_extTableAdapter1.FillBykey(Me.DataSet1.jomast_ext, keynum%)

        'Try
        '    myRow = DataSet1.jomast_ext.Select("FKey_ID = " + searchID$)
        '    RichTextBox1.Text = myRow(0)("ShopMemo").ToString
        'Catch ex As Exception
        'End Try

        'searchjo = UCase(TextBox1.Text.ToString)

        'intCursor = 0
        'Dim parttype As String
        'partfind.Clear()
        'DataGridView2.DataSource = partfind

        'Me.PartrackerTableAdapter1.FillByjobno(Me.DataSet1.Partracker, "C" + searchjo)

        'Do Until intCursor = DataSet1.Partracker.Rows.Count
        '    'If Mid((DataSet1.Partracker.Item(intCursor).jobno.ToString), 2, 10) = searchjo Then
        '    Dim newRow() As DataRow
        '    Dim searchPN$ = Trim(DataSet1.Partracker.Item(intCursor).partno)
        '    Dim searchfilter$ = "PartNum = '" + searchPN + "'"
        '    newRow = DataSet1.Partmap.Select(searchfilter)
        '    If newRow.Length > 0 Then
        '        parttype = newRow(0)("Descript")
        '    Else
        '        parttype = "OTHER" + Trim(DataSet1.Partracker.Item(intCursor).partno)
        '    End If

        '    Dim lastrow% = partfind.Rows.Count

        '    If lastrow = 0 Then
        '        Dim workrow As DataRow = partfind.NewRow()
        '        workrow(partnum) = parttype
        '        workrow(workcent) = Trim(DataSet1.Partracker.Item(intCursor).workcenter)
        '        workrow(timein) = Trim(DataSet1.Partracker.Item(intCursor).arrive)
        '        partfind.Rows.Add(workrow)
        '    Else
        '        If Trim(partfind.Rows.Item(lastrow - 1)(partnum)) = Trim(parttype) Then
        '            partfind.Rows.Item(lastrow - 1)(workcent) = Trim(DataSet1.Partracker.Item(intCursor).workcenter)
        '            partfind.Rows.Item(lastrow - 1)(timein) = Trim(DataSet1.Partracker.Item(intCursor).arrive)
        '        Else
        '            Dim workrow As DataRow = partfind.NewRow()
        '            workrow(partnum) = parttype
        '            workrow(workcent) = Trim(DataSet1.Partracker.Item(intCursor).workcenter)
        '            workrow(timein) = Trim(DataSet1.Partracker.Item(intCursor).arrive)
        '            partfind.Rows.Add(workrow)
        '        End If
        '    End If
        '    'End If
        '    intCursor += 1
        'Loop

        'intCursor = 0
        'buyitem.Clear()
        'DataGridView3.DataSource = buyitem
        'DataGridView3.Columns.Item(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'DataGridView3.Columns.Item(0).Width = 50
        'DataGridView3.Columns.Item(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'DataGridView3.Columns.Item(2).Width = 310
        'DataGridView3.Columns.Item(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'DataGridView3.Columns.Item(3).Width = 50
        'DataGridView3.Columns.Item(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        'DataGridView3.Columns.Item(4).Width = 50

        'Me.PoitemTableAdapter1.FillByfjokey(Me.DataSet1.poitem, searchjo)

        'Do Until intCursor = DataSet1.poitem.Rows.Count
        '    Dim buyqty As Single
        '    Dim recptqty As Single
        '    If Trim(DataSet1.poitem.Item(intCursor).fjokey) = searchjo Then
        '        buyqty = Val(DataSet1.poitem.Item(intCursor).fordqty)
        '        recptqty = Val(DataSet1.poitem.Item(intCursor).frcpqty)
        '        Dim workrow As DataRow = buyitem.NewRow
        '        workrow(pono) = Trim(DataSet1.poitem.Item(intCursor).fpono)
        '        workrow(partno) = Trim(DataSet1.poitem.Item(intCursor).fpartno)
        '        workrow(descript) = Trim(DataSet1.poitem.Item(intCursor).fdescript)
        '        workrow(ordqty) = buyqty.ToString
        '        workrow(rcptqty) = recptqty.ToString
        '        buyitem.Rows.Add(workrow)
        '    End If
        '    intCursor += 1
        'Loop

        Button2.Focus()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Try
        '    Me.JomastTableAdapter1.Fill(Me.DataSet1.jomast)
        'Catch
        'End Try
        'Try
        '    Me.PartmapTableAdapter1.Fill(Me.DataSet1.Partmap)
        'Catch ex As Exception

        'End Try
        'Try
        '    Me.PasswordsTableAdapter.Fill(Me.DataSet1.Passwords)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        ' enter key on jobnumber
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' clear screen
        partstable.Clear()
        DataGridView1.DataSource = partstable
        partfind.Clear()
        DataGridView2.DataSource = partfind
        buyitem.Clear()
        DataGridView3.DataSource = buyitem
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox24.Text = ""
        TextBox25.Text = ""
        TextBox26.Text = ""
        TextBox27.Text = ""
        TextBox28.Text = ""
        RichTextBox1.Text = ""
        TextBox1.Focus()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' get pdf from PDM
        If Mid(UCase(TextBox1.Text.ToString), 1, 1) = "I" Then
            TextBox3.Text = "INTERNAL"
        End If

        Dim searchjobno As String = Mid(UCase(TextBox1.Text.ToString), 1, 5)
        Dim searchsono As String = Trim(TextBox3.Text)
        Dim searchdate As String = Trim(TextBox4.Text)
        Dim filename As String
        Dim searchpath As String = "C:\Yates_Vault\GA-AUS\Documents\Jobs\" + searchdate + "\" + searchsono + "\" + searchjobno + "\Engineering\"

        OpenFileDialog1.InitialDirectory = searchpath
        OpenFileDialog1.Filter = Nothing '"PDFs (*.pdf)|*.pdf"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = DialogResult.Cancel Then
            Exit Sub
        Else
            filename = OpenFileDialog1.FileName
        End If
        'FrmViewer.Show()
        'FrmViewer.AxAcroPDF1.src = filename

    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        ' look at individual job
        Dim operdone As String
        If e.ColumnIndex = 6 Then
            operdone = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If operdone = "N" Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Y"
                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
            Else
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "N"
                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Pink
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' save changes to individual jobs - routing
        'FrmPassBox.ShowDialog()
        'Dim something As String = Trim(FrmPassBox.Passreturn())
        'Dim foundrow() As DataRow
        'Dim searchvalue As String = "ID = 2"
        'foundrow = DataSet1.Passwords.Select(searchvalue)
        'Dim passvalue As String = foundrow(0)("password")
        'If something = passvalue Then
        '    Savegrid()
        'End If
        'FrmPassBox.Dispose()

    End Sub
    Private Sub Savegrid()
        ' save changes

        ' jodrtg_ext table
        ' CreatedDate = scheduled finish date
        ' ModifiedDate = operation done date
        ' LBDate = scheduled start date


        'Dim IntCursor% = 0
        'Dim searchjo$ = Trim(UCase(TextBox1.Text))
        'Dim findkey As String

        'Do Until IntCursor% = partstable.Rows.Count
        '    Dim myRow() As DataRow
        '    Dim searchrout$ = Trim(partstable.Rows(IntCursor)(opno))
        '    Dim tablefilter$ = "fjobno = '" + searchjo + "' AND foperno = '" + searchrout + "'"
        '    myRow = DataSet1.Jodrtg.Select(tablefilter)

        '    Dim identkey As String = myRow(0)("Expr1")
        '    findkey = "FKey_ID = '" + identkey + "'"

        '    Me.Jodrtg_extTableAdapter1.FillBykey(Me.DataSet1.jodrtg_ext, identkey)

        '    Dim foundrow() As DataRow
        '    Select Case partstable.Rows(IntCursor)(opdone)
        '        Case "Y"
        '            Try
        '                foundrow = DataSet1.jodrtg_ext.Select(findkey)
        '                If IsDBNull(foundrow(0)("ModifiedDate")) Then
        '                    foundrow(0)("ModifiedDate") = Now
        '                    Jodrtg_extTableAdapter1.Update(foundrow)
        '                End If
        '                If foundrow(0)("ModifiedDate") < "01/01/2000" Then
        '                    foundrow(0)("ModifiedDate") = Now
        '                    Jodrtg_extTableAdapter1.Update(foundrow)
        '                End If
        '            Catch ex As Exception

        '            End Try
        '        Case "N"
        '            Try
        '                foundrow = DataSet1.jodrtg_ext.Select(findkey)
        '                If IsDBNull(foundrow(0)("ModifiedDate")) Then
        '                    ' nothing - it already thinks the part isn't done
        '                End If
        '                If foundrow(0)("ModifiedDate") > "01/01/2000" Then
        '                    foundrow(0)("ModifiedDate") = "01/01/1900"
        '                    Jodrtg_extTableAdapter1.Update(foundrow)
        '                End If
        '            Catch ex As Exception

        '            End Try
        '        Case Else
        '            ' something went wrong
        '    End Select

        '    IntCursor += 1
        'Loop
        'Button2.Focus()

        'Dim findrow() As DataRow

        'Me.Jomast_extTableAdapter1.FillBykey(Me.DataSet1.jomast_ext, keynum%)
        'findrow = DataSet1.jomast_ext.Select("FKey_ID = " + searchID$)
        'If findrow.Length > 0 Then
        '    findrow(0)("ShopMemo") = Trim(RichTextBox1.Text)
        '    Jomast_extTableAdapter1.Update(findrow)
        'Else
        '    ' nothing
        'End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim oldjobhead$
        Dim jobvalue%
        oldjobhead$ = Mid(TextBox1.Text, 1, 2)
        jobvalue% = Int(Val(Mid(TextBox1.Text, 3, 3)))
        jobvalue += 1
        If jobvalue > 999 Then
            jobvalue = 0
        End If
        TextBox1.Text = oldjobhead + Format(jobvalue, "000")
        Button1.Focus()
        Button1.PerformClick()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim oldjobhead$
        Dim jobvalue%
        oldjobhead$ = Mid(TextBox1.Text, 1, 2)
        jobvalue% = Int(Val(Mid(TextBox1.Text, 3, 3)))
        jobvalue -= 1
        If jobvalue < 0 Then
            jobvalue = 999
        End If
        TextBox1.Text = oldjobhead + Format(jobvalue, "000")
        Button1.Focus()
        Button1.PerformClick()
    End Sub


End Class