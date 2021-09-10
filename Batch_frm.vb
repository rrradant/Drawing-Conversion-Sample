Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.FileIO
Imports System.Drawing.Printing
Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Friend Class frmBatch
    Inherits System.Windows.Forms.Form

    ' Flag used to indicate the stop button was selected during processing.
    Private StopProcess As Boolean
    Private LastLogFilename As String
    Private UserSelectedMode As Boolean
    'T95686 : Dev Deviation Task for Modify Batch.exe in the SE Custom folder.
    'Added new button "Output Folder" where the output files will get generated.
    'Sumant Deshpande		10/06/2015
    Private FolderPath, outfolder, Filter, Filters(5) As String
    Private MyTestCode As Boolean
    Private nFilters As Integer
    Public strDate As String
    Public SpecExt As Boolean
    Public LogFile As Integer
    Public objApp As SolidEdgeFramework.Application = Nothing
    Public DftInactive As Boolean
    Public bIsAdapterOn As Boolean

    Private Sub cboFromType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFromType.SelectedIndexChanged
        ' Call the convert from option button click event
        ' since it already does the changing of filters.
        If optConvertFrom.Checked = True Then
            optConvertFrom_CheckedChanged(optConvertFrom, New System.EventArgs())
        End If
    End Sub

    Private Sub cboToType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToType.SelectedIndexChanged
        ' Call the convert to option button click event
        ' since it already does the changing of filters.
        If optConvertTo.Checked = True Then
            optConvertTo_CheckedChanged(optConvertTo, New System.EventArgs())
        End If
    End Sub

    Private Sub chkAssembly_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAssembly.CheckStateChanged
        optConvertTo_CheckedChanged(optConvertTo, New System.EventArgs())
    End Sub

    Private Sub chkDraft_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDraft.CheckStateChanged
        optConvertTo_CheckedChanged(optConvertTo, New System.EventArgs())
    End Sub

    Private Sub chkPart_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPart.CheckStateChanged
        optConvertTo_CheckedChanged(optConvertTo, New System.EventArgs())
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        If cmdCancel.Text = "Stop" Then
            StopProcess = True
            cmdCancel.Text = "Exit"
        Else
            FileClose(LogFile)
            End
        End If
    End Sub


    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        Dim i, j As Integer
        Dim objDoc As Object
        Dim strFileNames() As String = Nothing
        Dim intFileNameCount As Integer
        Dim KillLogFile As Boolean
        Dim NewFilename As String
        'Dim NewFilename1 As String
        Dim splitIndex As Integer
        Dim FileExtension As String = Nothing
        Dim TemplateFile As String = Nothing
        Dim TrimExt, Pass As Integer
        Dim strSpec As String
        Dim SaveAs3DPDF As Boolean = False
        Dim DraftPrinter As SolidEdgeDraft.DraftPrintUtility = Nothing
        Dim retry As Boolean

        '******* Added because of .NET
        Try
            MessageFilter.Register()
        Catch ex As Exception
            PrintLine("Error registering message filter.")
        End Try
        '******* Added because of .NET

        strDate = System.DateTime.Now.Hour.ToString + "_" + System.DateTime.Now.Minute.ToString + "_" + System.DateTime.Now.Second.ToString + " " + System.DateTime.Now.Month.ToString + "-" + System.DateTime.Now.Day.ToString + "-" + System.DateTime.Now.Year.ToString
        txtLogfile.Text = outfolder
        LastLogFilename = outfolder & "\Batch" & strDate & ".log"

        If lstFiles.Items.Count = 0 And optAllFiles.Checked = False Then
            MsgBox("Please select/provide input file")
            Exit Sub
        End If

        If txtLogfile.Text = "" Then
            MsgBox("Please provide output folder path")
            Exit Sub
        End If

        'This is just put in here to make it work better.


        ' Change the caption on the cancel button and set the stop processing flag.
        cmdCancel.Text = "Stop"
        StopProcess = False

        ' Initialize flag to indicate if the user wants to stop processing files.
        StopProcess = False

        ' Build up list of files to process depending on which option was selected.
        intFileNameCount = 0

        txtStatus.Text = "Preprocessing..."

        If lstFiles.Items.Count > 0 Or optAllFiles.Checked Then
            ' Only process the selected files so build up the array so it
            ' contains the currently selected files.
            If optSelected.Checked Then
                ' Allocate space to store the filenames.
                ReDim strFileNames(lstFiles.Items.Count)
                If lstFiles.SelectedItems.Count = 0 Then
                    txtStatus.Text = "No files selected, please select file(s) or switch options"
                    GoTo EndProc
                End If

                ' Load the selected filenames into the array.
                For i = 0 To lstFiles.Items.Count - 1
                    If lstFiles.GetSelected(i) Then
                        intFileNameCount = intFileNameCount + 1
                        strFileNames(intFileNameCount - 1) = FolderPath & "\" & lstFiles.Items(i)
                    End If
                Next
                ' Process all the files in the current directory.
            ElseIf optAllInDirectory.Checked Then

                ' Allocate space to store the filenames.
                ReDim strFileNames(lstFiles.Items.Count)

                ' Load the selected filenames into the array.
                For i = 0 To lstFiles.Items.Count - 1
                    intFileNameCount = intFileNameCount + 1
                    strFileNames(intFileNameCount - 1) = FolderPath & "\" & lstFiles.Items(i)
                Next
                ' Process all files in the current directory and subdirectories.
            ElseIf optAllFiles.Checked Then
                ' Call function to get all files in the current directory and subdirectories.
                txtStatus.Text = "Creating list of files to process ..."
                'Call ReadFileNamesFromDirectory(dirDirectory.Path, intFileNameCount, strFileNames, (filFiles.Pattern))
                Call ReadFileNamesFromDirectory(FolderPath, intFileNameCount, strFileNames)
                txtStatus.Text = ""
            End If
        End If
        '********
        'Radant Modification
        'Empties strFileNames
        'Adds my own list of files

        For i = 0 To strFileNames.GetUpperBound(0)
            strFileNames(i) = ""
        Next
        'strFileNames(0)


        '********


        ' Attempt to connect to a running instance of Solid Edge.
        Try
            objApp = GetObject(, "SolidEdge.Application")
            retry = False
        Catch ex As Exception
            txtStatus.Text = "Starting Solid Edge ..."
            txtStatus.Update()
            retry = True
        End Try

        If retry = True Then

            ' Start Solid Edge.
            Try
                objApp = CreateObject("SolidEdge.Application")

            Catch ex As Exception
                Activate()
                MsgBox("Cannot start Solid Edge.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Open & Save")
                FileClose(LogFile)
                End

            End Try

        End If

        ' Make Solid Edge visible.
        objApp.Visible = True

        ' PR 7294338: Batch processing is not working for STEP translation
        ' Removed old code to load sestep.dll and use SetResetAdapterKey API. Added new global constant
        ' seApplicationGlobalSTEPAdapterKey and based on this constant implemented the code to remember
        ' the current active STEP translator (Adapter/Interop) and then switch to STEP Adapter for
        ' STEP translation using batch tool. After translation process completes then revert back to
        ' previously active Translator.
        If (cboToType.Text = "STEP (*.stp)" Or cboFromType.Text = "STEP (*.stp)") Then
            objApp.Application.GetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSTEPAdapterKey, bIsAdapterOn)
            objApp.Application.SetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSTEPAdapterKey, True)
        End If
        objApp.Application.GetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive, DftInactive)

        ' set to open inactive if draft file
        If optConvertTo.Checked Then
            If chkLoadInactive.Checked Then
                objApp.Application.SetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive, True)
            Else
                objApp.Application.SetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive, False)
            End If
        End If

        If KillLogFile Then
            Kill(LastLogFilename)
        End If

        Try
            LogFile = FreeFile()
            FileOpen(LogFile, LastLogFilename, OpenMode.Output)
        Catch ex As Exception
            Activate()
            MsgBox("Error opening specified logfile: '" & LastLogFilename & "'" & Chr(13) & "Make sure you have write access", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Open & Save")
            Exit Sub
        End Try

        ' Write header information into log file.
        PrintLine(LogFile, "Batch Log: " & Today & ", " & TimeOfDay)
        PrintLine(LogFile, "")


        If optPrintDocuments.Checked Then
            ' Create DraftPrinterUtility if going to print
            DraftPrinter = objApp.GetDraftPrintUtility()
        End If

        ' Loop through the list of files and process.
        For i = 0 To intFileNameCount - 1
            ' Check the global variable to see if the stop process command button has been selected
            System.Windows.Forms.Application.DoEvents()
            If StopProcess Then
                MsgBox("Processing aborted.")
                GoTo RegularEnd
            End If

            ' PR 7470844: Gagetable problems with office 2013. - Satyenjit Bagal [02/11/2016]
            ' For PSM files turn OFF display allerts.
            ' This is done so that we can avoid opening gage excel files during automation.
            'If strFileNames(i).EndsWith(".psm") Then
            'objApp.DisplayAlerts = False
            ' Else
            'objApp.DisplayAlerts = True
            'End If

            'PR#9090402  - Massive translation with batch.exe, Many times process is stopped by a popup that warns about multiple bodies.
            'Since it is a batch processing so disabling all kind of pop for all the supported format.
            objApp.DisplayAlerts = False


            ' Display current status
            txtStatus.Text = "Processing file " & i + 1 & " of " & intFileNameCount & ": '" & strFileNames(i) & "'"

            ' Perform the correct task based on what was selected.
            If optPrintDocuments.Checked Then

                ' Open the document.
                objDoc = Nothing
                Try
                    objDoc = objApp.Documents.Open(strFileNames(i))
                    objApp.DoIdle()
                Catch ex As Exception
                    PrintLine(LogFile, "Error opening document: " & strFileNames(i))
                    GoTo SkipToNext
                End Try

                Try
                    DraftPrinter.AddDocument(objDoc)
                    DraftPrinter.PrintOut()
                    PrintLine(LogFile, "Successfully printed document: " & strFileNames(i))

                Catch ex As Exception
                    PrintLine(LogFile, "Error printing document: " & strFileNames(i))
                    PrintLine(LogFile, "Make sure printer in online and that required papersize is available")

                End Try

                DraftPrinter.RemoveAllDocuments()

                ' Close the document without saving.
                If Not objDoc Is Nothing Then
                    objDoc.Close(False)
                    objDoc = Nothing
                    ' Give the application time to process the document closure
                    objApp.DoIdle()
                End If
            ElseIf optReplaceBackground.Checked Then
                Dim strTemplatePath As String = tbNewTemplatePath.Text
                Dim objSections As SolidEdgeDraft.Sections
                Dim objSection As SolidEdgeDraft.Section
                Dim objSectionSheets As SolidEdgeDraft.SectionSheets
                Dim objSheet As SolidEdgeDraft.Sheet

                ' Open the document.
                objDoc = Nothing
                Try
                    objDoc = objApp.Documents.Open(strFileNames(i))
                    objApp.DoIdle()
                Catch ex As Exception
                    PrintLine(LogFile, "Error opening document: " & strFileNames(i))
                    GoTo SkipToNext
                End Try

                Try
                    objSections = objDoc.Sections
                    objSection = objSections.BackgroundSection
                    objSectionSheets = objSection.Sheets

                    For iSectionSheet As Int32 = 1 To objSectionSheets.Count
                        objSheet = objSectionSheets.Item(iSectionSheet)
                        objSheet.ReplaceBackground(strTemplatePath, objSheet.Name)
                    Next

                    PrintLine(LogFile, "Successfully replaced backgrounds for document: " & strFileNames(i))
                Catch ex As Exception
                    PrintLine(LogFile, "Error replacing background for document: " & strFileNames(i))
                End Try

            ElseIf optConvertFrom.Checked Then
                ' Determine the template to use.
                If optDraft.Checked Then
                    TemplateFile = "Normal.dft"
                    FileExtension = "dft"
                ElseIf optPart.Checked Then
                    TemplateFile = "Normal.par"
                    FileExtension = "par"
                ElseIf optSheetmetal.Checked Then
                    TemplateFile = "Normal.psm"
                    FileExtension = "psm"
                ElseIf optAssembly.Checked Then
                    TemplateFile = "Normal.asm"
                    FileExtension = "asm"
                Else
                    txtStatus.Text = "Select Template"
                    GoTo EndProc
                End If

                ' Build up the new file name.  Need to handle extensions that are not 3 characters in length
                Pass = 1
                strSpec = ""
                If SpecExt Then
                    TrimExt = -1
                    For j = 0 To strFileNames(i).Length - 1
                        TrimExt = TrimExt + 1
                        If strFileNames(i).Substring(strFileNames(i).Length - j - 1, 1) = "." Then
                            ' ProE needs special treatment due to the extra "." in the extension.
                            ' As this can lead to top level asm issues on naming, need to special handle the final SE name by adding an "_#"
                            If (cboFromType.Text = "ProE Asm (*.asm.*)" Or cboFromType.Text = "ProE Part (*.prt.*)") And Pass = 1 Then
                                TrimExt = TrimExt + 1
                                strSpec = "_" & strFileNames(i).Substring(strFileNames(i).Length - j, TrimExt - 1) & "."
                                Pass = 2
                            Else
                                Exit For
                            End If
                        End If
                    Next
                Else
                    TrimExt = 3
                End If

                'T95686 : Dev Deviation Task for Modify Batch.exe in the SE Custom folder.
                'Added new button "Output Folder" where the output files will get generated.
                'Sumant Deshpande		10/06/2015
                'NewFilename = strFileNames(i).Substring(0, strFileNames(i).Length - TrimExt) & strSpec & FileExtension
                Dim strArr() As String
                Dim str1 As String = strFileNames(i).ToString()
                strArr = str1.Split("\")
                splitIndex = strArr.Length
                NewFilename = strArr(splitIndex - 1)
                NewFilename = NewFilename.Substring(0, NewFilename.Length - TrimExt)
                NewFilename = outfolder & "\" & NewFilename & strSpec & FileExtension
                ' Open the existing file.
                objDoc = Nothing

                Try
                    objDoc = objApp.Documents.OpenWithTemplate(strFileNames(i), TemplateFile)
                    objApp.DoIdle()

                Catch ex As Exception
                    PrintLine(LogFile, "Error opening document: " & strFileNames(i))
                    GoTo SkipToNext
                End Try

                ' Save the new document.
                Try
                    objDoc.SaveAs(NewFilename)
                    PrintLine(LogFile, "Successfully saved '" & strFileNames(i) & " ' as '" & NewFilename & "'.")

                Catch ex As Exception
                    PrintLine(LogFile, "Error saving document '" & strFileNames(i) & "' to '" & NewFilename & "'.")
                    GoTo SkipToNext
                End Try

                If Not objDoc Is Nothing Then
                    objDoc.Close(False)
                    objDoc = Nothing
                    ' Give the application time to process the document closure
                    objApp.DoIdle()
                End If

            ElseIf optConvertTo.Checked Then

                ' Build up the file name of the file to create.
                Select Case cboToType.Text
                    Case "Parasolid (*.x_t)"
                        FileExtension = "x_t"
                        'Case "EMS (*.ems)"
                        '    FileExtension = "ems"
                    Case "STL File (*.stl)"
                        FileExtension = "stl"
                    Case "Microstation (*.dgn)"
                        FileExtension = "dgn"
                    Case "AutoCAD (*.dwg)"
                        FileExtension = "dwg"
                    Case "AutoCAD (*.dxf)"
                        FileExtension = "dxf"
                    Case "JT (*.jt)"
                        FileExtension = "jt"
                    Case "Catia V4 (*.model)"
                        FileExtension = "model"
                    Case "Catia V5 Part (*.catpart)"
                        FileExtension = "catpart"
                    Case "IGES (*.igs)"
                        FileExtension = "igs"
                    Case "STEP (*.stp)"
                        FileExtension = "stp"
                    Case "IFC (*.IFC)"
                        FileExtension = "IFC"
                    Case "OBJ (*.obj)"
                        FileExtension = "obj"
                    Case "FBX (*.fbx)"
                        FileExtension = "fbx"
                        ' Newly supported in Solid Edge
                    Case "Adobe (*.pdf)"
                        FileExtension = "pdf"
                    Case "3D Adobe PDF (*.pdf)"
                        FileExtension = "pdf"
                        SaveAs3DPDF = True
                    Case "ACIS (*.sat)"
                        FileExtension = "sat"
                    Case "XML (*.plmxml)"
                        FileExtension = "plmxml"
                    Case "NX Bookmark (*.bkm)"
                        FileExtension = "bkm"
                    Case "XGL (*.xgl)"
                        FileExtension = "xgl"
                        ' Newly supported in Solid Edge
                    Case "iPad Viewer (*.sev)"
                        FileExtension = "sev"
                    Case "Keyshot (.bip)"
                        FileExtension = "bip"
                    Case "Universal 3D (.u3d)"
                        FileExtension = "u3d"
                End Select

                'NewFilename = FolderPath1 & FileExtension

                'T95686 : Dev Deviation Task for Modify Batch.exe in the SE Custom folder.
                'Added new button "Output Folder" where the output files will get generated.
                'Sumant Deshpande		10/06/2015
                Dim strArr() As String
                Dim str1 As String = strFileNames(i).ToString()
                strArr = str1.Split("\")
                splitIndex = strArr.Length
                NewFilename = strArr(splitIndex - 1)
                NewFilename = NewFilename.Substring(0, NewFilename.Length - 3)
                NewFilename = outfolder & "\" & NewFilename & FileExtension

                ' Open the document.
                objDoc = Nothing
                Try
                    objDoc = objApp.Documents.Open(strFileNames(i))
                    objApp.DoIdle()

                Catch ex As Exception
                    PrintLine(LogFile, "Error opening document: " & strFileNames(i))
                    GoTo SkipToNext
                End Try

                ' Save the document using the new name.  The SaveAs method will
                ' save the file to as a specific type depending on the file extension.
                Try

                    If SaveAs3DPDF Then
                        objDoc.SaveAs(NewFilename, , True)
                    Else
                        objDoc.SaveAs(NewFilename)
                    End If
                    PrintLine(LogFile, "Successfully saved '" & strFileNames(i) & "' as '" & NewFilename & "'.")
                Catch ex As Exception
                    PrintLine(LogFile, "Error saving document '" & strFileNames(i) & "' to '" & NewFilename & "'.")
                    GoTo SkipToNext
                End Try

                ' Close the document without saving.
                If Not objDoc Is Nothing Then
                    objDoc.Close(False)
                    objDoc = Nothing
                    ' Give the application time to process the document closure
                    objApp.DoIdle()
                End If

            End If

SkipToNext:
        Next

RegularEnd:
        ' Close the log file.
        FileClose(LogFile)

        ' Dismiss the status form.
        txtStatus.Text = "Finished processing."

EndProc:
        If optConvertTo.Checked Then
            Try
                ' restore draft open setting to original value.
                objApp.Application.SetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSessionDraftOpenInactive, DftInactive)
            Catch ex As Exception

            End Try
        End If
        Visible = True
        cmdCancel.Text = "Exit"

        'T84437: Code: Step Adapter
        'Switch back to Interop STEP translator if flag bIsAdapterOn is False. False value of
        'bIsAdapterOn indicates that, previously Interop STEP translator was in use. - Kuldeep Khadilkar(0-08-2015)
        If (cboToType.Text = "STEP (*.stp)" Or cboFromType.Text = "STEP (*.stp)") And Not bIsAdapterOn = False Then
            objApp.Application.SetGlobalParameter(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalSTEPAdapterKey, bIsAdapterOn)
        End If

        'Since translation is completed so setting back to original state
        objApp.DisplayAlerts = True

        '******* Added because of .NET
        Try
            MessageFilter.Revoke()
        Catch ex As Exception
            PrintLine("Error revoking the message filter.")
        End Try
        '******* Added because of .NET

        Exit Sub
    End Sub

    ' Recursively travels through a directory structure saving all files into an array.
    Private Sub ReadFileNamesFromDirectory(ByVal FilePath As String, ByRef intFileNameCount As Integer, ByRef strFileNames() As String)
        Dim i As Integer
        Dim strDirList() As String = Nothing
        Dim intDirCount, intfilecount As Integer

        ' If there are any subdirectories in the current directory save their
        ' names in an array so they can be processed below.
        ' Set the counter.
        intDirCount = FileSystem.GetDirectories(FilePath).Count
        If intDirCount > 0 Then
            ' Allocate memory to store the directories.
            ReDim strDirList(intDirCount)

            ' Save the directory names into the array.
            For i = 0 To intDirCount - 1
                strDirList(i) = FileSystem.GetDirectories(FilePath).Item(i)
            Next
        End If

        ' If there are any files in the current directory save their names
        ' into the filename array.
        intfilecount = FileSystem.GetFiles(FilePath).Count
        If intfilecount > 0 Then
            ' Allocate more space in the filename array to store the filenames.
            ReDim Preserve strFileNames(intFileNameCount + intfilecount)

            ' Save all the filenames of the files in the current directory
            ' into the filename array.
            For i = 0 To nFilters
                For Each foundFile As String In FileSystem.GetFiles(
                    FilePath,
                    SearchOption.SearchTopLevelOnly, Filters(i))
                    intFileNameCount = intFileNameCount + 1
                    strFileNames(intFileNameCount - 1) = foundFile
                Next
            Next
        End If

        ' Loop through subdirectories of the current directory and get their files.
        For i = 0 To intDirCount - 1
            ' Recursively call this subroutine.
            ReadFileNamesFromDirectory(strDirList(i), intFileNameCount, strFileNames)
        Next

    End Sub

    Private Sub frmBatch_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ' Initialize the default log file.
        FolderPath = System.Windows.Forms.Application.StartupPath()
        FolderBrowserDialog1.SelectedPath = FolderPath
        lblPath.Text = FolderPath
        'PR 7539666 :Output folder location is not updated based on input file location
        'Solution: Set Output folder location based on input file location.
        'Sumant Deshpande.          10/26/2015.
        txtLogfile.Text = FolderPath
        strDate = System.DateTime.Now.Hour.ToString + "_" + System.DateTime.Now.Minute.ToString + "_" + System.DateTime.Now.Second.ToString + " " + System.DateTime.Now.Month.ToString + "-" + System.DateTime.Now.Day.ToString + "-" + System.DateTime.Now.Year.ToString

        'txtLogfile.Text = FolderPath

        LastLogFilename = FolderPath & "\Batch" & strDate & ".log"

        ' Initialize the combo boxes with their contents.
        cboFromType.Items.Clear()
        cboFromType.Items.Add("Parasolid (*.x_t)")
        cboFromType.Items.Add("JT (*.jt)")
        cboFromType.Items.Add("NX Files (*.prt)")
        cboFromType.Items.Add("ACIS (*.sat)")
        cboFromType.Items.Add("AutoCAD (*.dwg)")
        cboFromType.Items.Add("AutoCAD (*.dxf)")
        cboFromType.Items.Add("Catia V4 (*.model)")
        cboFromType.Items.Add("Catia V5 Asm (*.catproduct)")
        cboFromType.Items.Add("Catia V5 Part (*.catpart)")
        cboFromType.Items.Add("IGES (*.igs)")
        'cboFromType.Items.Add("Microstation (*.dgn)")
        cboFromType.Items.Add("ProE Asm (*.asm.*)")
        cboFromType.Items.Add("ProE Part (*.prt.*)")
        cboFromType.Items.Add("SDRC (*.xpk,plmxpk)")
        cboFromType.Items.Add("Solid Works Asm (*.sldasm)")
        cboFromType.Items.Add("Solid Works Part (*.sldprt)")
        cboFromType.Items.Add("Inventor Asm (*.iam)")
        cboFromType.Items.Add("Inventor Part (*.ipt)")
        cboFromType.Items.Add("STEP (*.stp)")
        cboFromType.Items.Add("IFC (*.IFC)")
        cboFromType.Items.Add("OBJ (*.obj)")
        'cboFromType.Items.Add("FBX (*.fbx)")
        'FBX import is not supported in solid edge.
        cboFromType.Items.Add("STL File (*.stl)")
        cboFromType.Items.Add("XML (*.plmxml)")
        cboFromType.Text = "Parasolid (*.x_t)"

        cboToType.Items.Clear()
        cboToType.Items.Add("AutoCAD (*.dxf)")
        cboToType.Items.Add("AutoCAD (*.dwg)")
        'cboToType.Items.Add("Microstation (*.dgn)")
        cboToType.Items.Add("IGES (*.igs)")
        cboToType.Items.Add("STEP (*.stp)")
        cboToType.Items.Add("IFC (*.IFC)")
        cboToType.Items.Add("OBJ (*.obj)")
        cboToType.Items.Add("FBX (*.fbx)")
        cboToType.Items.Add("Parasolid (*.x_t)")
        cboToType.Items.Add("JT (*.jt)")
        cboToType.Items.Add("STL File (*.stl)")
        cboToType.Items.Add("Catia V4 (*.model)")
        cboToType.Items.Add("Catia V5 Part (*.catpart)")
        cboToType.Items.Add("XML (*.plmxml)")
        cboToType.Items.Add("ACIS (*.sat)")
        cboToType.Items.Add("XGL (*.xgl)")
        cboToType.Items.Add("Adobe (*.pdf)")
        cboToType.Items.Add("3D Adobe PDF (*.pdf)")
        cboToType.Items.Add("iPad Viewer (*.sev)")
        cboToType.Items.Add("Keyshot (*.bip)")
        cboToType.Items.Add("Universal 3D (*.u3d)")
        cboToType.Items.Add("NX Bookmark (*.bkm)")
        cboToType.Text = "AutoCAD (*.dxf)"

    End Sub


    Private Sub optConvertFrom_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optConvertFrom.CheckedChanged
        If eventSender.Checked Then
            buttonSelectNewTemplate.Enabled = False

            Dim i As Integer
            Dim Asm, Dft, All, NotDft As Boolean
            ' Disable the select boxes.
            chkPart.Enabled = False
            chkAssembly.Enabled = False
            chkDraft.Enabled = False
            cboToType.Enabled = False
            ' enable needed gadgets
            grpTemplates.Enabled = True
            cboFromType.Enabled = True

            ' use to control enable/disable for the template options.
            All = True
            Asm = False
            Dft = False
            NotDft = False
            nFilters = 0
            SpecExt = False
            ' Set the correct search pattern based on the currently selected type.
            Select Case cboFromType.Text
                ' Variable template
                Case "Parasolid (*.x_t)"
                    nFilters = 1
                    Filters(0) = "*.x_t"
                    Filters(1) = "*.x_b"
                    NotDft = True
                Case "NX Files (*.prt)"
                    Filters(0) = "*.prt"
                    NotDft = True
                Case "IGES (*.igs)"
                    nFilters = 1
                    Filters(0) = "*.igs"
                    Filters(1) = "*.iges"
                    SpecExt = True
                Case "JT (*.jt)"
                    Filters(0) = "*.jt"
                    NotDft = True
                    SpecExt = True
                Case "STL File (*.stl)"
                    Filters(0) = "*.stl"
                    NotDft = True
                Case "Catia V4 (*.model)"
                    Filters(0) = "*.model"
                    NotDft = True
                    SpecExt = True
                Case "Catia V5 Part (*.catpart)"
                    Filters(0) = "*.catpart"
                    NotDft = True
                    SpecExt = True
                Case "STEP (*.stp)"
                    nFilters = 1
                    Filters(0) = "*.stp"
                    Filters(1) = "*.step"
                    NotDft = True
                    SpecExt = True
                Case "IFC (*.IFC)"
                    Filters(0) = "*.IFC"
                    NotDft = True
                    SpecExt = True
                Case "OBJ (*.obj)"
                    Filters(0) = "*.obj"
                    NotDft = True
                    SpecExt = True
                Case "FBX (*.fbx)"
                    Filters(0) = "*.fbx"
                    NotDft = True
                    SpecExt = True
                Case "XML (*.plmxml)"
                    Filters(0) = "*.plmxml"
                    NotDft = True
                    SpecExt = True
                Case "ACIS (*.sat)"
                    Filters(0) = "*.sat"
                    NotDft = True
                Case "SDRC (*.xpk,plmxpk)"
                    nFilters = 1
                    Filters(0) = "*.xpk"
                    Filters(1) = "*.plmxpk"
                    NotDft = True
                    SpecExt = True
                Case "Solid Works Part (*.sldprt)"
                    Filters(0) = "*.sldprt"
                    NotDft = True
                    SpecExt = True
                Case "ProE Part (*.prt.*)"
                    Filters(0) = "*.prt.*"
                    NotDft = True
                    SpecExt = True
                Case "Inventor Part (*.ipt)"
                    Filters(0) = "*.ipt*"
                    NotDft = True
                    SpecExt = True

                    ' Assembly only template
                Case "Catia V5 Asm (*.catproduct)"
                    Filters(0) = "*.catproduct"
                    Asm = True
                    SpecExt = True
                Case "Solid Works Asm (*.sldasm)"
                    Filters(0) = "*.sldasm"
                    Asm = True
                    SpecExt = True
                Case "ProE Asm (*.asm.*)"
                    Filters(0) = "*.asm.*"
                    Asm = True
                    SpecExt = True
                Case "Inventor Asm (*.iam)"
                    Filters(0) = "*.iam"
                    Asm = True
                    SpecExt = True

                    ' Draft only template
                Case "Microstation (*.dgn)"
                    Filters(0) = "*.dgn"
                    Dft = True
                Case "AutoCAD (*.dwg)"
                    Filters(0) = "*.dwg"
                    Dft = True
                Case "AutoCAD (*.dxf)"
                    Filters(0) = "*.dxf"
                    Dft = True
            End Select

            If Dft Then
                optDraft.Enabled = True
                optDraft.Checked = True
                optAssembly.Enabled = False
                optPart.Enabled = False
                optSheetmetal.Enabled = False
            ElseIf Asm Then
                optAssembly.Checked = True
                optDraft.Enabled = False
                optAssembly.Enabled = True
                optPart.Enabled = False
                optSheetmetal.Enabled = False
            ElseIf NotDft Then
                If optDraft.Checked Then optPart.Checked = True
                optAssembly.Enabled = True
                optPart.Enabled = True
                optSheetmetal.Enabled = True
                optDraft.Enabled = False
            Else
                optDraft.Enabled = True
                optAssembly.Enabled = True
                optPart.Enabled = True
                optSheetmetal.Enabled = True
            End If

            lstFiles.Items.Clear()
            For i = 0 To nFilters
                FileList(FolderPath, Filters(i))
            Next
            If optDraft.Checked = False And optAssembly.Checked = False And optDraft.Checked = False And optSheetmetal.Checked = False Then
                txtStatus.Text = "Select Template"
            End If
            'Filters(0) = Filter
        End If
    End Sub

    Private Sub optConvertTo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optConvertTo.CheckedChanged
        If eventSender.Checked Then
            buttonSelectNewTemplate.Enabled = False

            Dim FileTypes() As String = {"*.asm", "*.par", "*.psm", "*dft", "*.pwd"}
            ' enable / disable some gadgets
            grpTemplates.Enabled = False
            cboFromType.Enabled = False
            cboToType.Enabled = True

            ' Set the correct search pattern based on the currently selected type.
            Select Case cboToType.Text
                Case "JT (*.jt)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Catia V4 (*.model)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Catia V5 Part (*.catpart)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "IGES (*.igs)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = True
                Case "STEP (*.stp)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "IFC (*.IFC)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "OBJ (*.obj)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "FBX (*.fbx)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Adobe (*.pdf)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = True
                Case "3D Adobe PDF (*.pdf)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "ACIS (*.sat)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "XML (*.plmxml)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "NX Bookmark (*.bkm)"
                    chkPart.Enabled = False
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                    chkAssembly.Checked = True
                Case "XGL (*.xgl)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Parasolid (*.x_t)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "iPad Viewer (*.sev)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = True
                Case "Keyshot (*.bip)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Universal 3D (*.u3d)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "STL File (*.stl)"
                    chkPart.Enabled = True
                    chkAssembly.Enabled = True
                    chkDraft.Enabled = False
                Case "Microstation (*.dgn)"
                    chkPart.Enabled = False
                    chkAssembly.Enabled = False
                    chkDraft.Enabled = True
                    chkDraft.Checked = True
                Case "AutoCAD (*.dwg)"
                    chkPart.Enabled = False
                    chkAssembly.Enabled = False
                    chkDraft.Enabled = True
                    chkDraft.Checked = True
                Case "AutoCAD (*.dxf)"
                    chkPart.Enabled = False
                    chkAssembly.Enabled = False
                    chkDraft.Enabled = True
                    chkDraft.Checked = True
            End Select

            lstFiles.Items.Clear()
            nFilters = -1

            If chkPart.Checked And chkPart.Enabled Then
                FileList(FolderPath, "*.par")
                FileList(FolderPath, "*.psm")
                nFilters = nFilters + 2
                Filters(nFilters - 1) = "*.par"
                Filters(nFilters) = "*.psm"
            End If

            If chkAssembly.Checked And chkAssembly.Enabled Then
                nFilters = nFilters + 1
                Filters(nFilters) = "*.asm"
                FileList(FolderPath, "*.asm")
            End If

            If chkDraft.Checked And chkDraft.Enabled Then
                nFilters = 0
                Filters(0) = "*.dft"
                FileList(FolderPath, "*.dft")
            End If

        End If
    End Sub

    Private Sub optPrintDocuments_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPrintDocuments.CheckedChanged
        If eventSender.Checked Then
            ' Disable the check boxes.
            chkPart.Enabled = False
            chkAssembly.Enabled = False
            chkDraft.Enabled = False
            grpTemplates.Enabled = False
            cboFromType.Enabled = False
            cboToType.Enabled = False
            buttonSelectNewTemplate.Enabled = False
            ' Set the filter for Draft documents.

            lstFiles.Items.Clear()
            FileList(FolderPath, "*.dft")
            nFilters = 0
            Filters(0) = "*.dft"

        End If
    End Sub

    Private Sub optSelected_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSelected.CheckedChanged
        If eventSender.Checked Then
            UserSelectedMode = True
        End If
    End Sub

    Private Sub btnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolder.Click
        'Dim FolderPath As String
        Dim i As Integer
        FolderBrowserDialog1.ShowDialog()
        FolderPath = FolderBrowserDialog1.SelectedPath
        FileSystem.CurrentDirectory = FolderPath

        txtLogfile.Text = FolderPath

        lblPath.Text = "Folder Path: " & FolderPath
        lblPath.Update()
        lstFiles.Items.Clear()

        For i = 0 To nFilters
            FileList(FolderPath, Filters(i))
        Next

    End Sub
    Private Sub FileList(ByVal Path As String, ByVal Filter As String)
        Dim FileName As String
        Dim lenPath, len As Integer

        lenPath = Path.Length

        For Each foundFile As String In FileSystem.GetFiles(Path, SearchOption.SearchTopLevelOnly, Filter)
            len = foundFile.Length
            FileName = foundFile.Substring(lenPath + 1, len - lenPath - 1)
            lstFiles.Items.Add(FileName)
        Next
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        optSelected.Checked = True
        txtStatus.Text = ""
    End Sub

    Private Sub buttonSelectNewTemplate_Click(sender As Object, e As EventArgs) Handles buttonSelectNewTemplate.Click
        Dim ofd As New OpenFileDialog

        If (ofd.ShowDialog(Me) = DialogResult.OK) Then
            tbNewTemplatePath.Text = ofd.FileName
        End If
    End Sub

    Private Sub optReplaceBackground_CheckedChanged(sender As Object, e As EventArgs) Handles optReplaceBackground.CheckedChanged
        buttonSelectNewTemplate.Enabled = True
    End Sub

    Private Sub chkDraft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDraft.CheckedChanged
        If chkDraft.Checked Then
            chkLoadInactive.Enabled = True
        Else
            chkLoadInactive.Enabled = False
        End If
    End Sub


    'T95686 : Dev Deviation Task for Modify Batch.exe in the SE Custom folder.
    'Added new button "Output Folder" where the output files will get generated.
    'Sumant Deshpande		10/06/2015
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles outputfolder.Click
        'Dim FolderPath As String
        'Dim i As Integer
        FolderBrowserDialog1.ShowDialog()
        outfolder = FolderBrowserDialog1.SelectedPath
        FileSystem.CurrentDirectory = outfolder

        txtLogfile.Text = outfolder

        'lblPath.Text = "Folder Path: " & outfolder
        'lblPath.Update()
        'lstFiles.Items.Clear()

        'For i = 0 To nFilters
        ' FileList(FolderPath, Filters(i))
        'Next

    End Sub

    Private Sub txtLogfile_TextChanged(sender As Object, e As EventArgs) Handles txtLogfile.TextChanged
        'PR 7539431 : Output path copied manually is not considered while processing the file (D5626)
        'Considered manually copying output path while processing the file from Batch.exe
        outfolder = txtLogfile.Text
    End Sub
End Class